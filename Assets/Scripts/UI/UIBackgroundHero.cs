using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBackgroundHero : MonoBehaviour
{
    public TMP_Text headertxt;
    public TMP_Text unlocktxt;
    public List<GameObject> skillBtn;
    public GameObject popUpSkill;
    private string[] Skill = {"Skil 0", "Skill 1", "Skill 2"};
    //Video Player
    private UnityEngine.Video.VideoPlayer videoPlayer;

    private void Awake() 
    {
        videoPlayer = FindObjectOfType<UnityEngine.Video.VideoPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickButtonSkill(int id)
    {
        ShowPopUp();
        videoPlayer = GetComponentInChildren<UnityEngine.Video.VideoPlayer>();
        headertxt.text = Skill[id];
        ShowVideoSkill(id);
    }
    private void ShowVideoSkill(int id)
    {
        string url = "C:/Users/OS/Tin_Project/Iron Fight Game/Assets/Video/" + id + ".mp4";
        videoPlayer.url = url;
    }

    public void ShowPopUp()
    {
        popUpSkill.SetActive(true);
    }

    public void ClosePopUp()
    {
        popUpSkill.SetActive(false);
    }
}
