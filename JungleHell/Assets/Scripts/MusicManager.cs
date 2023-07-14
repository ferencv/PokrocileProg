using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("audioSource found");
    }

    // Start is called before the first frame update
    void Start()
    {
        //Game.Instance.on
        //audioSource.Play();
    }

    private void OnGameStateChanged()
    {
        
    }
}
