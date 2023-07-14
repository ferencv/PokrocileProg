using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private Button mainMenuButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(MainMenuClick);
    }
    private void MainMenuClick()
    {
        Loader.Load("InitialScene");
    }

    // Start is called before the first frame update
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
        if (Game.Instance.IsGameOver())
        {
            SoundManager.Instance.PlayGameOverSound();
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
