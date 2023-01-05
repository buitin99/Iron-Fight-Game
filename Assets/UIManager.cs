using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{  
    private Animator animator;
    private int alert;
    private int non;
    private ScoreManager scoreManager;
    private void Awake() 
    {
        animator = GetComponent<Animator>();
        scoreManager = FindObjectOfType<ScoreManager>();
        alert = Animator.StringToHash("Next");
        non = Animator.StringToHash("Non");
    }

    private void OnEnable() 
    {
        scoreManager.OnWaveDone.AddListener(AlertPlayer);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NonAlert()
    {   
        animator.SetTrigger(non);
    }

    private void AlertPlayer()
    {
        animator.SetTrigger(alert);
    }

    private void OnDisable() 
    {
        scoreManager.OnWaveDone.RemoveListener(AlertPlayer);
    }
}
