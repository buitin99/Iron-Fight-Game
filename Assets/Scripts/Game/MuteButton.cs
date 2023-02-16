using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public bool isMute;
    private SettingData settingData;
    private Button      button;
    private AudioManager audioManager;

    private void Awake() 
    {
        audioManager = AudioManager.Instance;
        button       = GetComponent<Button>();

      
    }

    private void OnEnable() 
    {

        //16-02
        settingData = SettingData.LoadData();
        OnMute(settingData.mute);

        //

        audioManager.OnMute.AddListener(OnMute);

        button.onClick.AddListener(OnClick);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMute(bool mute)
    {
        if (mute)
        {
            gameObject.SetActive(!isMute);
        }
        else
        {
            gameObject.SetActive(isMute);
        }
    }

    private void OnClick()
    {
        audioManager.MuteGame(isMute);
    }


    //Using Destroy
    private void OnDestroy()
    {
        audioManager.OnMute.RemoveListener(OnMute);
    }
}
