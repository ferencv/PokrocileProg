using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Running,
}

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private bool isWalking;
    private bool isDead;
    private bool isShooting;
    private bool isHit;
    private static int InitialAmmoCount = 5; 
    private int ammoCount = InitialAmmoCount;
    private DateTime lastShotTime = DateTime.Now.AddHours(-1); 
    private TimeSpan maxCadency = TimeSpan.FromSeconds(0.5);
    //[SerializeField]
    private float moveSpeed = 7.0f;
    //[SerializeField]
    private float rotateSpeed = 150.0f; // 150 stupnu za sekundu
    [SerializeField]
    private GameInput gameInput;
    [SerializeField]
    private GameObject gunEndPointPosition;
    public PlayerState playerState;
    private int levelEnterAmmoCount = InitialAmmoCount;
    private float gunRange = 11f;
    private float gunSpread = 40f;


    [SerializeField] private Transform pfPlayerHitExplossion;

    public event EventHandler OnStateChanged;
    public event EventHandler OnLevelFinishReached;
    public event EventHandler OnDie;
    //public event EventHandler OnAmmoGrab;

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs 
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 playerPosition;
        public bool hasAmmo;
        public float gunRange;
        public float gunSpread;
    }
    public event EventHandler<OnAmmoCountChangeEventArgs> OnAmmoCountChange;

    public class OnAmmoCountChangeEventArgs 
    {
        public int ammoCount;
    }

    private void Awake() 
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Player.Instance.gameObject.SetActive(true);
            Destroy(gameObject);
        }
        playerState = PlayerState.Idle;
        //gunEndPointPosition = transform.Find("GunEndPointPosition");
    }

    private void Start()
    {
        //levelEnterPosition = transform.position;
        //levelEnterForwardDirection = transform.forward;
        //OnAmmoCountChange?.Invoke(this, new OnAmmoCountChangeEventArgs() { ammoCount = ammoCount});
    }

    // Update is called once per frame
    private void Update()
    {
        if (Game.Instance.IsGamePaused())
            return;

        if (!isDead) 
        {
            if (isHit) 
            {
                isDead = true;
                return;
            }
            HandleMovement();
            HandleShooting();
        }
    }

    private void HandleMovement() 
    {
        var inputVector = gameInput.GetInputVector();

        if (inputVector.x != 0)
        {
            transform.Rotate(0f, inputVector.x * Time.deltaTime * rotateSpeed, 0f, Space.Self);
        }
        var moveDistance = moveSpeed * Time.deltaTime;
        var playerRadius = 0.4f;
        var playerHeight = 2f;
        var raycastHit = new RaycastHit();
        var anyCollision = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, transform.forward * inputVector.y, out raycastHit, moveDistance);
        if (anyCollision) 
        {
            Debug.Log("Player hit smth: " + raycastHit.collider.gameObject.name);
            if (raycastHit.collider.GetComponent<FinishLevel>() != null) 
            {
                Debug.Log("Player hit level finish");

                this.isWalking = false;
                levelEnterAmmoCount = ammoCount;
                OnLevelFinishReached?.Invoke(this, EventArgs.Empty);
                return;
            }
            var ammo = raycastHit.collider.GetComponent<Ammo>();
            if (ammo != null)
            {
                Debug.Log("Player hit level ammo");

                ammoCount++;
                ammo.GrabAmmo();
                OnAmmoCountChange?.Invoke(this, new OnAmmoCountChangeEventArgs() { ammoCount = ammoCount });
                return;
            }
        }

        bool canMove = !anyCollision;

        if (canMove)
        {
            transform.position += inputVector.y * transform.forward * moveDistance;
        }
        var isWalking = inputVector.y != 0;
        if (isWalking && !this.isWalking) 
        {
            this.isWalking = true;
            playerState = PlayerState.Running;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
        if (!isWalking && this.isWalking) 
        {
            this.isWalking = false;
            playerState = PlayerState.Idle;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandleShooting() 
    {
        isShooting = gameInput.IsShooting();
        if (isShooting)
        {
            var now = DateTime.Now;
            var span = now - lastShotTime;
            if (span < maxCadency) 
            {
                return;
            }
            lastShotTime = DateTime.Now;
            //Debug.Log(gunEndPointPosition.transform.position);
            var hasAmmo = false;
            if (ammoCount > 0) 
            {
                hasAmmo = true;
                ammoCount--;
                OnAmmoCountChange?.Invoke(this, new OnAmmoCountChangeEventArgs() { ammoCount = ammoCount });
            }
            OnShoot?.Invoke(this, new OnShootEventArgs()
            {
                hasAmmo = hasAmmo,
                playerPosition = transform.position,
                gunEndPointPosition = gunEndPointPosition.transform.position,
                shootPosition = transform.forward.normalized,
                gunRange = gunRange,
                gunSpread = gunSpread + 6,
            });
        }
    }

    private bool CanMove(Vector2 moveDirection) 
    {
        var moveDistance = moveSpeed * Time.deltaTime;
        var playerRadius = 0.7f;
        var playerHeight = 2f;

        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, transform.forward, moveDistance);
    }

    public bool IsWalking() 
    {
        return isWalking;
    }
    public bool IsShooting()
    {
        return isShooting;
    }
    public bool IsHit()
    {
        return isHit;
    }
    public bool IsDead()
    {
        return isDead;
    }

    public void SetIsHit() 
    {
        if (!isDead) 
        {
            isHit = true;
            isShooting = false;
            isWalking = false;
            CreateHitExplosion();
            SoundManager.Instance.PlayPlayerHitSound();
            OnDie?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SetIdleState()
    {
        playerState = PlayerState.Idle;
        isHit = false;
        isShooting = false;
        isWalking = false;
        isDead = false;
        ammoCount = levelEnterAmmoCount;
        OnAmmoCountChange?.Invoke(this, new OnAmmoCountChangeEventArgs() { ammoCount = ammoCount });
        //Player.Instance.transform.forward = levelEnterForwardDirection;
        //Player.Instance.transform.position = levelEnterPosition;
    }

    private void CreateHitExplosion()
    {
        Debug.Log("Player hit");//e.gunEndPointPosition
        var playerHitPosition = Utils.CopyVector3(transform.position);
        playerHitPosition.y = gunEndPointPosition.transform.position.y;
        var explossionTransform = Instantiate(pfPlayerHitExplossion, playerHitPosition, Quaternion.identity);
    }

    public int GetAmmoCount() { return ammoCount; }

    public void Init()
    {
        Player.Instance.gameObject.SetActive(false);
        levelEnterAmmoCount = InitialAmmoCount;
        SetIdleState();
    }
}
