using UnityEngine;

public class PlayerAnimationDelegete : CharacterAnimationDelegete
{
    public GameObject leftHandAttackPoint, rightHandAttackPoint, leftLegAttackPoint, rightLegAttackPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void LeftHandPunchOn()
    {
        leftHandAttackPoint.SetActive(true);
    }
    public override void LeftHandPunchOff()
    {
        if(leftHandAttackPoint.activeInHierarchy)
        {
            leftHandAttackPoint.SetActive(false);
        }
    }
    public override void LeftLegKickOn()
    {
        leftLegAttackPoint.SetActive(true);
    }

    public override void LeftLegKickOff()
    {
        if(leftLegAttackPoint.activeInHierarchy)
        {
            leftLegAttackPoint.SetActive(false);
        }
    }
    public override void RightHandPunchOn()
    {
        rightHandAttackPoint.SetActive(true);
    }

    public override void RightHandPunchOff()
    {
        if(rightHandAttackPoint.activeInHierarchy)
        {
            rightHandAttackPoint.SetActive(false);
        }
    }
    public override void RightLegKickOn()
    {
        rightLegAttackPoint.SetActive(true);
    }

    public override void RightLegKickOff()
    {
        if(rightLegAttackPoint.activeInHierarchy)
        {
            rightLegAttackPoint.SetActive(false);
        }
    }

    void TagLeftArm()
    {
        leftHandAttackPoint.tag = "LeftArm";
    }

    void UnTagLeftArm()
    {
        leftHandAttackPoint.tag = "Untagged";
    }

    void TagLeftLeg()
    {
        leftLegAttackPoint.tag = "LeftLeg";
    }

    void UnTagLeftLeg()
    {
        leftLegAttackPoint.tag = "Untagged";
    }

    
}
