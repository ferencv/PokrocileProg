using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKilledUI : MonoBehaviour
{
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button mainMenuButton;

    private void Awake()
    {
        retryButton.onClick.AddListener(RetryClick);
        mainMenuButton.onClick.AddListener(MainMenuClick);
    }

    private void RetryClick()
    {
        //Player.Instance.SetIdleState();
        Loader.Load(Game.Instance.GetCurrentSceneName());
    }
    private void MainMenuClick()
    {
        Loader.Load("InitialScene");
    }

    private void Start()
    {
        Game.Instance.OnStateChanged += OnGameStateChanged;
        Hide();
    }


    private void OnDestroy()
    {
        Game.Instance.OnStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(object sender, EventArgs e)
    {
        if (Game.Instance.IsPlayerKilled())
        {
            Show();
        }
        else 
        {
            Hide();
        }
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
