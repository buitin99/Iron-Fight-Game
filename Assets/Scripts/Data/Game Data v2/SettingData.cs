using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingData
{
    public bool mute;
    public SettingData()
    {
        mute = false;
    }

    public static SettingData LoadData()
    {
        return GameDataManagers<SettingData>.LoadData();
    }

    public void SaveData()
    {
        GameDataManagers<SettingData>.SaveData(this);
    }
}
