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
    private int                         chuongLaserHash;
    private int                         chuongRocketHash;

    [SerializeField]
    private float                       speed = 10f;
    private float                       fallingVelocity;
    private float                       gravity = - 9.81f;
    private bool                        activeTimerToReset;
    private float                       default_Combo_Timer = 0.4f;
    private float                       current_Combo_Timer;
    private ComboState                  current_Combo_State;
    private float                       chuongTimer = 3f;

    private Vector3                      direction;
    public Rig                          handRig;
    public ParticleSystem               chuongVFX;
    public GameObject                   chuongObject;
    public GameObject                   rightHand, leftHand, rightLeg, leftLeg;
    public AudioClip                    knockoutAudioClip;
    public AudioClip                    deadAudioClip;
    private UIManager ui;
    public LayerMask alertLayer;
    // public GameObject                   missilePrefab;

    // public GameObject                   missilePos;

    private GameManager gameManager;

    [Range(0,1)] public float volumeScale;
    private bool turnRight, turnLeft;
    private float health = 100f;
    private bool  isDead, isLaser ,isCanMove;
    public bool isStartGame;
    private SoundManager soundManager;
    private PlayerDamageable playerDamageable;

    private void Awake() 
    {
        characterController = GetComponent<CharacterController>();
        playerInputActions  = new PlayerInputActions();
        velocityHash        = Animator.StringToHash("Velocity");
        chuongAHash         = Animator.StringToHash("ChuongA");
        chuongBHash         = Animator.StringToHash("ChuongB");
        punchAHash          = Animator.StringToHash("PunchA");
        punchBHash          = Animator.StringToHash("PunchB");
        punchABHash         = Animator.StringToHash("PunchAB");
        kickAHash           = Animator.StringToHash("KickA");
        kickBHash           = Animator.StringToHash("KickB");
        flipHash            = Animator.StringToHash("Flip");
        chuongLaserHash     = Animator.StringToHash("Chuong");
        chuongRocketHash    = Animator.StringToHash("Rocket");
        playerDamageable = GetComponent<PlayerDamageable>();

        ui = FindObjectOfType<UIManager>();
        playerDamageable.setInit(200, 0);

        soundManager = SoundManager.Instance;
        gameManager = GameManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        current_Combo_Timer = default_Combo_Timer;
        current_Combo_State = ComboState.NONE;
        isCanMove = true;
    }

    private void OnEnable() 
    {
        playerInputActions.Enable();


        playerInputActions.Player.Move.performed        += GetDirectionMove;
        playerInputActions.Player.Move.canceled         += GetDirectionMove;
        playerInputActions.Player.Fire.started          += PunchAnimation;
        playerInputActions.Player.Punch.started         += FlipAnimation;
        playerInputActions.Player.Chuong.started        += ChuongHandle;
        playerInputActions.Player.ChuongRocket.started  += ChuongRocket;

        gameManager.OnStartGame.AddListener(StartGame);
        gameManager.OnEndGame.AddListener(SetPositionHealthBar);
    }

    private void StartGame()
    {
        isStartGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log (isStartGame);
        // if (!isStartGame)
        //     return;
    
        if (!playerDamageable.isDead)
        {
            if (isCanMove & !isLaser)
            {
                Move();
                RotationLook();
                HandleGravity(); 

            }
        }
        HandleAnimation();
        ResetComboState();
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

    private void ChuongHandle(InputAction.CallbackContext ctx)
    {
        chuongVFX.Play();
        ChuongAnimation();
        chuongObject.SetActive(true);
    }

    private void ChuongRocket(InputAction.CallbackContext ctx)
    {
        // Instantiate(missilePrefab, missilePos.transform.position, missilePos.transform.rotation);
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
        isLaser = true;
        StartCoroutine(ChuongAnimationOff());
    }

    IEnumerator ChuongAnimationOff()
    {
        yield return new WaitForSeconds(chuongTimer);
        while(handRig.weight >= 0.2)
        {
            handRig.weight = handRig.weight = Mathf.Lerp(handRig.weight, -1f, 20f*Time.deltaTime);
        }
        isLaser = false;
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

    public void PlayerCanMove()
    {
        isCanMove = true;
    }

    public void PlayerCanNotMove()
    {
        isCanMove = false;
    }

    //Audio
    // private void PlayHitSound()
    // {
    //     soundManager.PlayOneShot(hitAudioClip, volumeScale);
    // }

    private void PlaySoundDead()
    {
        soundManager.PlayOneShot(deadAudioClip, volumeScale);
    }

    public void PlaySoundKnockDonw()
    {
        soundManager.PlayOneShot(knockoutAudioClip, volumeScale);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if ((alertLayer & (1 << other.gameObject.layer)) != 0)
        {  
            // ui.NonAlert();
        }
    }

    private void SetPositionHealthBar(bool isWin)
    {
        if (isWin)
        {
            playerDamageable.PositionHealthBar();
        }
    }

    private void OnDisable() 
    {
        playerInputActions.Player.Move.performed        -= GetDirectionMove;
        playerInputActions.Player.Move.canceled         -= GetDirectionMove;
        playerInputActions.Player.Punch.started         -= FlipAnimation;
        playerInputActions.Player.Fire.started          -= PunchAnimation;
        playerInputActions.Player.Chuong.started        -= ChuongHandle;
        playerInputActions.Player.ChuongRocket.started  -= ChuongRocket;

        gameManager.OnStartGame.RemoveListener(StartGame);
        gameManager.OnEndGame.RemoveListener(SetPositionHealthBar);

    }
}
