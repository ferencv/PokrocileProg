using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameUI : MonoBehaviour
{

    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button exitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayClick);
        exitButton.onClick.AddListener(ExitClick);
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        if (Game.Instance != null) 
        {
            Game.Instance.InitGame();
        }
    }

    private void PlayClick() 
    {
        Loader.Load("Level1");
    }
    private void ExitClick()
    {
        Application.Quit();
    }

    //// Start is called before the first frame update
    //private void Start()
    //{
    //    Game.Instance.OnStateChanged += OnGameStateChanged;
    //    Hide();
    //}

    //private void OnGameStateChanged(object sender, EventArgs e)
    //{
    //    if (Game.Instance.IsWaitingForStart())
    //    {
    //        Show();
    //    }
    //    else 
    //    {
    //        Hide();
    //    }
    //}


    //private void Show() 
    //{
    //    gameObject.SetActive(true);
    //}
    //private void Hide()
    //{
    //    gameObject.SetActive(false);
    //}
}
