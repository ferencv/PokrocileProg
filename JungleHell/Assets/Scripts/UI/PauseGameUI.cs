using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : MonoBehaviour
{

    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button mainMenuButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(ContinueClick);
        mainMenuButton.onClick.AddListener(MainMenuClick);
    }

    private void Start()
    {
        Game.Instance.OnGamePaused += OnGamePaused;
        Game.Instance.OnGameUnPaused += OnGameUnPaused;
        Hide();
    }

    private void OnDestroy()
    {
        Game.Instance.OnGamePaused -= OnGamePaused;
        Game.Instance.OnGameUnPaused -= OnGameUnPaused;
    }

    private void OnGamePaused(object sender, EventArgs e) 
    {
        Show();
    }

    private void OnGameUnPaused(object sender, EventArgs e) 
    {
        Hide();
    }

    private void ContinueClick() 
    {
        Game.Instance.UnPauseGame();
    }
    private void MainMenuClick()
    {
        Loader.Load("InitialScene");
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
