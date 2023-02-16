using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource audioSource;
    private SettingData settingData;
    public UnityEvent<bool> OnMute;

    protected override void Awake()
    {
        base.Awake();
        settingData = SettingData.LoadData();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() 
    {
        audioSource.mute = settingData.mute;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayOneShot(AudioClip audioClip, float volumeScale = 1)
    {
        audioSource.PlayOneShot(audioClip, volumeScale);
    }

    public void MuteGame(bool mute)
    {
        //16-02
        settingData.mute = mute;
        settingData.SaveData();
        audioSource.mute = mute;
        OnMute?.Invoke(mute);
    }
}
