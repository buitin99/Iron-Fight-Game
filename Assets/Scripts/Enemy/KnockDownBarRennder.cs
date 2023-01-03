using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
#if UNITY_EDITOR
[CanEditMultipleObjects]
#endif

public class KnockDownBarRennder
{
    public GameObject  knockDownBar;
    public float       offset;
    private Camera     camera;
    private GameObject _knockDownBar;
    private Slider     sliderKnockDownBar;

    public void CreateKnockDownBar(Transform parent, float minKnockDown)
    {
        camera = Camera.main;
        _knockDownBar = GameObject.Instantiate(knockDownBar);
        _knockDownBar.transform.SetParent(parent, false);
        _knockDownBar.transform.position = parent.position + Vector3.up*offset;
        sliderKnockDownBar = _knockDownBar.GetComponentInChildren<Slider>();
        sliderKnockDownBar.maxValue = 100;
        sliderKnockDownBar.value = minKnockDown;
    }
    
    public void UpdateKnockDownBarRotation()
    {
        Vector3 dirCam = camera.transform.position - _knockDownBar.transform.position;
        dirCam.x = 0;
        _knockDownBar.transform.rotation = Quaternion.LookRotation(dirCam.normalized);
    }

    public void UpdateKnockDownBarValue(float knockbar)
    {
        sliderKnockDownBar.value = knockbar;
    }

    public void DestroyKnockDownBar()
    {
        _knockDownBar.SetActive(false);
    }
}
