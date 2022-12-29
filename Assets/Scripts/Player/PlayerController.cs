using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public enum ComboState 
{
    NONE,
    PUNCHA,
    PUNCHB,
    PUNCHAB,
    KICKA,
    KICKB,
    CHUONGA,
    CHUONGB,
    FLIP
}
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    private Animator                    animator;
    private Vector2                     inputMove;
    private CharacterController         characterController;
    private InputAction.CallbackContext ctx;
    private int                         velocityHash;
    private int                         chuongAHash;
    private int                         chuongBHash;
    private int                         punchAHash;
    private int                         punchBHash;
    private int                         punchABHash;
    private int                         kickAHash;
    private int                         kickBHash;
    private int                         flipHash;
    private int                         deadHash;
    private int                         hitHash;
    private int                         knockDownHash;
    private int                         standUpHash;
    private int                         chuongLaserHash;
    private int                         chuongRocketHash;
    private float                       standUpTimer = 2f;

    [SerializeField]
    private float                       speed = 10f;
    private float                       fallingVelocity;
    private float                       gravity = - 9.81f;
    private bool                        activeTimerToReset;
    private float                       default_Combo_Timer = 0.4f;
    private float                       current_Combo_Timer;
    private ComboState                  current_Combo_State;
    private bool                        chuongFlag;
    private float                       chuongTimer = 3f;
    private Missile                     missile;

    public Vector3                      direction;
    public Rig                          handRig;
    public ParticleSystem               chuongVFX;
    public GameObject                   chuongObject;
    public LayerMask                    enemyLayerMask;
    public LayerMask                    enemyAttackLayer;
    public LayerMask                    enemyGun;
    public GameObject                   iniRocketPos;
    public GameObject                   rocket; 
    public GameObject                   rightHand, leftHand, rightLeg, leftLeg;
    public AudioClip    hitAudioClip, knockoutAudioClip, deadAudioClip;
    [Range(0,1)] public float volumeScale;
    private bool turnRight, turnLeft;
    private float health = 100f;
    private bool  isDead, isKnockDown;
    private SoundManager soundManager;

    private void Awake() 
    {
        characterController = GetComponent<CharacterController>();
        playerInputActions  = new PlayerInputActions();
        missile             = GetComponentInChildren<Missile>();
        velocityHash        = Animator.StringToHash("Velocity");
        chuongAHash         = Animator.StringToHash("ChuongA");
        chuongBHash         = Animator.StringToHash("ChuongB");
        punchAHash          = Animator.StringToHash("PunchA");
        punchBHash          = Animator.StringToHash("PunchB");
        punchABHash         = Animator.StringToHash("PunchAB");
        kickAHash           = Animator.StringToHash("KickA");
        kickBHash           = Animator.StringToHash("KickB");
        flipHash            = Animator.StringToHash("Flip");
        deadHash            = Animator.StringToHash("Dead");
        chuongLaserHash     = Animator.StringToHash("Chuong");
        hitHash             = Animator.StringToHash("Hit");
        knockDownHash       = Animator.StringToHash("KnockDown");
        standUpHash       = Animator.StringToHash("StandUp");
        chuongRocketHash    = Animator.StringToHash("Rocket");

        soundManager = SoundManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        current_Combo_Timer = default_Combo_Timer;
        current_Combo_State = ComboState.NONE;
    }

    private void OnEnable() 
    {
        playerInputActions.Enable();
        playerInputActions.Player.Move.performed += GetDirectionMove;
        playerInputActions.Player.Move.canceled  += GetDirectionMove;
        playerInputActions.Player.Fire.started   += PunchAnimation;
        playerInputActions.Player.Punch.started  += FlipAnimation;
        playerInputActions.Player.Chuong.started += ChuongHandle;
        playerInputActions.Player.ChuongLaserBame.started += ChuongLaserAnimation;
        playerInputActions.Player.ChuongRocket.started += ChuongRocket;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            HandleAnimation();
            ResetComboState();
            RotationLook();
            HandleGravity(); 
        }
      
    }

    private void RotationLook()
    {
        if(direction.x < 0) {
            turnLeft =  true;
            turnRight =  false;
        } else if(direction.x > 0) {
            turnRight = true;
            turnLeft =  false;
        }

        if(turnRight) {
            Quaternion rot = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, rot, 25 * Time.deltaTime);
            if(Vector3.Angle(transform.forward, Vector3.right) <= 0) {
                turnRight = false;
            }
        }

        if(turnLeft) {
            Quaternion rot = Quaternion.LookRotation(Vector3.left);
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, rot, 25 * Time.deltaTime);
            if(Vector3.Angle(transform.forward, Vector3.left) <= 0) {
                turnLeft = false;
            }
        }
    }

    private void Move()
    {
        Vector3 motionMove = direction*speed*Time.deltaTime;
        Vector3 motionFall = Vector3.up*fallingVelocity*Time.deltaTime;
        characterController.Move(motionMove + motionFall);
    }

    private void GetDirectionMove(InputAction.CallbackContext ctx) {
        Vector2 dir = ctx.ReadValue<Vector2>();
        direction = new Vector3(dir.x, 0, dir.y);
    }

    private void HandleGravity()
    {
        if(characterController.isGrounded)
        {
            fallingVelocity = gravity/10;
        } else
        {
            fallingVelocity += gravity/10;
        }
    }

    private void HandleAnimation()
    {
        Vector3 horizontalVelocity = new Vector3(characterController.velocity.x, 0 , characterController.velocity.z);
        float Velocity = horizontalVelocity.magnitude/speed;
        if (Velocity > 0)
        {
            animator.SetFloat(velocityHash, Velocity);
        } else 
        {
            float v = animator.GetFloat(velocityHash);
            v = v> 0.10f? Mathf.Lerp(v, 0, 20f*Time.deltaTime) : 0;
            animator.SetFloat(velocityHash, v);
        }
    }

    private void PunchAnimation(InputAction.CallbackContext ctx)
    {
        // if (current_Combo_State == ComboState.PUNCHAB || current_Combo_State == ComboState.KICKA || current_Combo_State == ComboState.KICKB)
        if (current_Combo_State == ComboState.KICKB)
            return;

        current_Combo_State++;
        activeTimerToReset = true;
        current_Combo_Timer = default_Combo_Timer;

        if (current_Combo_State == ComboState.PUNCHA)
        {
            animator.SetTrigger(punchAHash);
        }

        if (current_Combo_State == ComboState.PUNCHB)
        {
            animator.SetTrigger(punchBHash);
        }

        if (current_Combo_State == ComboState.PUNCHAB)
        {
            animator.SetTrigger(punchABHash);
        }
        
        if (current_Combo_State == ComboState.KICKA)
        {
            animator.SetTrigger(kickAHash);
        }

        if (current_Combo_State == ComboState.KICKA)
        {
            animator.SetTrigger(kickAHash);
        }

        if (current_Combo_State == ComboState.KICKB)
        {
            animator.SetTrigger(kickBHash);
        }
    }

    private void FlipAnimation(InputAction.CallbackContext ctx)
    {
        current_Combo_State = ComboState.FLIP;
        animator.SetTrigger(flipHash);
    }

    private void ResetComboState()
    {
        if(activeTimerToReset)
        {
            current_Combo_Timer -= Time.deltaTime;
            if (current_Combo_Timer <= 0f)
            {
                current_Combo_State = ComboState.NONE;

                activeTimerToReset = false;
                current_Combo_Timer = default_Combo_Timer;
            }
        }
    }

    public void PlayerDead()
    {
        animator.SetTrigger(deadHash);
    }

    private void ChuongHandle(InputAction.CallbackContext ctx)
    {
        chuongVFX.Play();
        ChuongAnimation();
        chuongObject.SetActive(true);
    }

    private void ChuongLaserAnimation(InputAction.CallbackContext ctx)
    {
        animator.SetTrigger(chuongLaserHash);
    }

    public void Hited()
    {
        animator.SetTrigger(hitHash);
    }

    public void KnockDown()
    {
        animator.SetTrigger(knockDownHash);
        isKnockDown = true;
    }

    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(standUpTimer);
        animator.SetTrigger(standUpHash);
        isKnockDown = false;

    }

    private void ChuongRocket(InputAction.CallbackContext ctx)
    {
        // Instantiate(rocket, iniRocketPos.transform.position, rocket.transform.rotation);
    }

    private void StandUp()
    {
        StartCoroutine(StandUpAfterTime());
    }

    private void Dead()
    {
        animator.SetTrigger(deadHash);
    }

    protected virtual void CameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(10f, .2f);
    }

    public void ChuongAnimation()
    {
        while(handRig.weight <= 0.9)
        {
            handRig.weight = handRig.weight = Mathf.Lerp(handRig.weight, 1f, 20f*Time.deltaTime);
        }
        playerInputActions.Player.Fire.started   -= PunchAnimation;
        StartCoroutine(ChuongAnimationOff());
    }

    IEnumerator ChuongAnimationOff()
    {
        yield return new WaitForSeconds(chuongTimer);
        while(handRig.weight >= 0.2)
        {
            handRig.weight = handRig.weight = Mathf.Lerp(handRig.weight, -1f, 20f*Time.deltaTime);
        }
        playerInputActions.Player.Fire.started   += PunchAnimation;
        chuongObject.SetActive(false);
    }

    public void SetLayerPlayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    public void SetLayerDefault()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (gameObject.layer != LayerMask.NameToLayer("Default"))
        {
            if ((enemyAttackLayer & (1 << other.gameObject.layer)) != 0)
            {
                if (health > 0)
                {
                    if (Random.Range(0, 3) >= 2)
                    {
                        KnockDown();
                        health -= 10;
                    }
                    else
                    {
                        Hited();
                        PlayHitSound();
                        health -= 5;
                    }
                }
                else if (health <= 0)
                {
                    Dead();
                    PlaySoundDead();
                    isDead = true;
                }

            }
        }
            
    }

    private void OnTriggerStay(Collider other) 
    {
        if ((enemyLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            // animator.SetTrigger(hitHash);
            // Debug.Log(other);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if ((enemyGun & (1 << other.gameObject.layer)) != 0)
        {
            animator.SetTrigger(hitHash);
        }
    }

    //Animation
    // rightHand, leftHand, rightLeg, leftLeg;
    public void RightHandAttackTrue()
    {
        rightHand.SetActive(true);
    }

    public void RightHandAttackFalse()
    {
        rightHand.SetActive(false);
    }

    public void LeftHandAttackTrue()
    {
        leftHand.SetActive(true);
    }

    public void LeftHandAttackFalse()
    {
        leftHand.SetActive(false);
    }

    public void RightLegAttackTrue()
    {
        rightLeg.SetActive(true);
    }

    public void RightLegAttackFalse()
    {
        rightLeg.SetActive(false);
    }

    public void LeftLegAttackTrue()
    {
        leftLeg.SetActive(true);
    }

    public void LeftLegAttackFalse()
    {
        leftLeg.SetActive(false);
    }

    //Audio
    private void PlayHitSound()
    {
        soundManager.PlayOneShot(hitAudioClip, volumeScale);
    }

    private void PlaySoundDead()
    {
        soundManager.PlayOneShot(deadAudioClip, volumeScale);
    }

    public void PlaySoundKnockDonw()
    {
        soundManager.PlayOneShot(knockoutAudioClip, volumeScale);
    }

    private void OnDisable() 
    {
        playerInputActions.Player.Move.performed -= GetDirectionMove;
        playerInputActions.Player.Move.canceled  -= GetDirectionMove;
        playerInputActions.Player.Punch.started  -= FlipAnimation;
        playerInputActions.Player.Fire.started   -= PunchAnimation;
        playerInputActions.Player.Chuong.started -= ChuongHandle;
        playerInputActions.Player.ChuongLaserBame.started -= ChuongLaserAnimation;
    }
}
