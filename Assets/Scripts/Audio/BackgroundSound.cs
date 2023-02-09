using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip        audioWin;
    public AudioClip        audioLose;
    private AudioManager    soundManager;
    private GameManager     gameManager;
    private AudioSource     audioSource;

    private void Awake() 
    {
        soundManager = AudioManager.Instance;
        gameManager  = GameManager.Instance;
        audioSource  = GetComponent<AudioSource>();
    }

    private void OnEnable() 
    {
        gameManager.OnEndGame.AddListener(OnEndGame);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEndGame(bool isWin)
    {
        if (isWin)
        {
            audioSource.clip = audioWin;
        }else
        {
            audioSource.clip = audioLose;
        }

        StartCoroutine(PlayAudioEndGame());
    }

    IEnumerator PlayAudioEndGame() {
        yield return new WaitForSeconds(0.1f);
        audioSource.Play();
    }

    private void OnDisable() 
    {
        gameManager.OnEndGame.RemoveListener(OnEndGame);
    }


}
