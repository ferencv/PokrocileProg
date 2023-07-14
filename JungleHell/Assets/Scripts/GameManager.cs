using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private enum GameState 
    {
        Init,
        Playing,
        PlayerKilled,
        GameOver,
        GameFinished,
    }

    [SerializeField] private Transform pfShell;
    [SerializeField] private Transform pfHeart;
    [SerializeField] private Transform heartContainer;
    [SerializeField] private Transform shellContainer;
    public static Game Instance { get; private set; }
    public event EventHandler OnStateChanged;

    private int level = 1;
    private int maxLevel = 7;
    private int playerLifes = 3;
    private float deadPlayerYMove = -3f;

    private List<Transform> hearts; 
    private List<Transform> shells;

    private DateTime playerDiedTime;
    private TimeSpan afterPlayerDiedTimeSpan = TimeSpan.FromSeconds(2);
    private GameState gameState;
    private bool isGamePaused = false;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;


    private DateTime GetFutureTime() 
    {
        return DateTime.Now.AddYears(10);
    }

    private void SetState(GameState state) 
    {
        Debug.Log("Menime stav: " + state);
        gameState = state;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            SetState(GameState.Init);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        playerDiedTime = GetFutureTime();
        //SceneManager.
        //SceneManager.LoadScene($"Level{level}", LoadSceneMode.Additive);
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += OnPauseAction;
        shells = new List<Transform>() { };

        if (Player.Instance != null) 
        {
            Player.Instance.OnDie += PlayerDied;
            Player.Instance.OnLevelFinishReached += PlayerCompletedLevel;
            Player.Instance.OnAmmoCountChange += PlayerAmmoCountChanged;

            PlayerAmmoCountChanged(Player.Instance, new Player.OnAmmoCountChangeEventArgs() { ammoCount = Player.Instance.GetAmmoCount() });
        }

        hearts = new List<Transform>() { };
        for (int i = 0; i < playerLifes; i++) 
        {
            var heart = Instantiate(pfHeart, heartContainer);
            heart.transform.localPosition = new Vector3(0, 0, -3f * (float)i);
            hearts.Add(heart);
            heart.gameObject.SetActive(true);
        }
    }

    private void OnPauseAction(object sender, EventArgs e) 
    {
        TogglePauseGame();    
    }

    private void Update()
    {
        var now = DateTime.Now;
        if (playerDiedTime < now && (now - playerDiedTime) >= afterPlayerDiedTimeSpan) 
        {
            playerDiedTime = GetFutureTime();
            if (playerLifes > 0)
            {
                var playerPosition = Player.Instance.transform.position;
                Player.Instance.transform.position = new Vector3(playerPosition.x, playerPosition.y + deadPlayerYMove, playerPosition.z);
                Player.Instance.SetIdleState();
                SetState(GameState.PlayerKilled);
                //SceneManager.LoadScene($"RepeatLevel");
            }
            else
            {
                SetState(GameState.GameOver);
                //SceneManager.LoadScene($"GameOver");
            }
        }
    }

    public bool IsWaitingForStart() { return gameState == GameState.Init; }
    public bool IsGamePlaying() { return gameState == GameState.Playing; }
    public bool IsPlayerKilled() { return gameState == GameState.PlayerKilled; }
    public bool IsGameOver() { return gameState == GameState.GameOver; }
    public bool IsGameFinished() { return gameState == GameState.GameFinished; }
    public bool IsGamePaused() { return isGamePaused; }
    
    private void PlayerDied(object sender, EventArgs e)
    {
        if (playerLifes > 0) 
        {
            playerLifes--;
            hearts[playerLifes].gameObject.SetActive(false);
        }
        playerDiedTime = DateTime.Now;
    }

    private void PlayerCompletedLevel(object sender, EventArgs e) 
    {
        if (level < maxLevel)
        {
            level++;
            //SceneManager.UnloadScene($"Level{level++}");
            //SceneManager.LoadScene($"Level{level}", LoadSceneMode.Additive);
            SceneManager.LoadScene($"Level{level}");
            //Player.Instance.SetLevelEnterTransform();
        }
        else 
        {
            //SceneManager.UnloadScene($"Level{level}");
            //SceneManager.LoadScene($"GameFinished", LoadSceneMode.Additive);
            SetState(GameState.GameFinished);
            //SceneManager.LoadScene($"GameFinished");
        }
    }

    public string GetCurrentSceneName() 
    {
        return $"Level{level}";
    }

    private void PlayerAmmoCountChanged(object sender, Player.OnAmmoCountChangeEventArgs e)
    {
        if (shells.Count == e.ammoCount)
            return;
        if (shells.Count < e.ammoCount)
        {
            for (var i = shells.Count; i < e.ammoCount; i++) 
            {
                var shell = Instantiate(pfShell, shellContainer);
                shells.Add(shell);
                shell.transform.localPosition = new Vector3(0, 0, -2f * (float)i);
                shell.gameObject.SetActive(true);
            }
        }
        else
        {
            if (shells.Count > 0) 
            {
                var shell = shells[shells.Count - 1];
                shells.Remove(shell);
                Destroy(shell.gameObject);
            }
        }
    }

    private void TogglePauseGame() 
    {
        Debug.Log("Pause pressed");
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else 
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public void UnPauseGame() 
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        OnGameUnPaused?.Invoke(this, EventArgs.Empty);
    }

    public void InitGame()
    {
        isGamePaused = false;
        playerLifes = 3;
        level = 1;
        for (int i = 0; i < playerLifes; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
        Player.Instance.Init();
    }
}
