using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationDelegete : CharacterAnimationDelegete
{
    public GameObject leftHandAttackPoint, rightHandAttackPoint, rightLegAttackPoint;
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
        //Not to do 
    }

    public override void LeftLegKickOff()
    {
        //Not to do 
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
}
