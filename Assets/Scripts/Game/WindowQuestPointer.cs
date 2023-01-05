using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class WindowQuestPointer : MonoBehaviour
{

    private Vector3 targetPos;
    private RectTransform pointerRectTransform;
    private void Awake() 
    {
        targetPos = new Vector3(200, 45);
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPos = targetPos;
        Vector3 fromPos = Camera.main.transform.position;
        fromPos.z = 0f;
        Vector3 dir = (toPos - fromPos).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
