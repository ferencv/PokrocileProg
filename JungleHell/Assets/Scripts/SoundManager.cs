using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField]
    private AudioClipsSO audioClipsSO;

    private void Awake() 
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        if (Player.Instance != null) 
        {
            Player.Instance.OnShoot += PlayerOnShoot;
        }
    }

    private void OnDestroy()
    {
        Debug.Log("SoundManager Player.Instance.OnShoot -= PlayerOnShoot");
        Player.Instance.OnShoot -= PlayerOnShoot;
    }

    private void PlayerOnShoot(object sender, Player.OnShootEventArgs e)
    {
        if (e.hasAmmo)
        {
            PlaySound(audioClipsSO.playerShoot, Camera.main.transform.position, 1f); // Player.Instance.transform.position
        }
        else 
        {
            PlaySound(audioClipsSO.playerShootEmpty, Camera.main.transform.position, 1f); // Player.Instance.transform.position
        }
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) 
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    public void PlayPlayerSingleFootstepSound()
    {
        PlaySound(audioClipsSO.playerSingleFootstep, Camera.main.transform.position, 0.5f);
    }


    public void PlayPlayerFootstepsSound() 
    {
        PlaySound(audioClipsSO.playerFootstep, Camera.main.transform.position, 0.7f);
    }

    public void PlayPlayerHitSound()
    {
        PlaySound(audioClipsSO.playerHit, Camera.main.transform.position, 0.1f);
    }

    public void PlayPlayerGrabAmmoSound()
    {
        PlaySound(audioClipsSO.playerGrabAmmo, Camera.main.transform.position, 0.5f);
    }

    public void PlayEnemyAttackSound()
    {
        PlaySound(audioClipsSO.enemyAttack, Camera.main.transform.position, 0.7f);
    }
    public void PlayEnemyFootstepsSound()
    {
        PlaySound(audioClipsSO.enemyFootstep, Camera.main.transform.position, 0.2f);
    }    
    public void PlayEnemyHitSound()
    {
        PlaySound(audioClipsSO.enemyHit, Camera.main.transform.position, 0.1f);
    }

    public void PlayApeEnemyAttackSound()
    {
        PlaySound(audioClipsSO.apeEnemyAttack, Camera.main.transform.position, 0.7f);
    }
    public void PlayApeEnemyFootstepsSound()
    {
        PlaySound(audioClipsSO.apeEnemyFootstep, Camera.main.transform.position, 0.2f);
    }
    public void PlayApeEnemyHitSound()
    {
        var random = (int)Mathf.Round(Random.Range(0.5f, 4.49f));
        Debug.Log(random);
        switch (random) 
        {
            case 1: PlaySound(audioClipsSO.apeEnemyHit, Camera.main.transform.position, 0.5f); return;
            case 2: PlaySound(audioClipsSO.apeEnemyHit2, Camera.main.transform.position, 0.5f); return;
            case 3: PlaySound(audioClipsSO.apeEnemyHit3, Camera.main.transform.position, 0.5f); return;
            case 4: PlaySound(audioClipsSO.apeEnemyHit4, Camera.main.transform.position, 0.5f); return;
        }
        PlaySound(audioClipsSO.apeEnemyHit, Camera.main.transform.position, 0.5f);
    }

    public void PlayGameFinishedSound()
    {
        PlaySound(audioClipsSO.gameFinishedSound, Camera.main.transform.position, 1f);
    }
    public void PlayGameOverSound()
    {
        PlaySound(audioClipsSO.gameOverSound, Camera.main.transform.position, 1f);
    }
}
