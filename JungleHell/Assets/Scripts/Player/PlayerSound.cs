using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSound : MonoBehaviour
{
    [SerializeField]
    private Player player;
    private AudioSource audioSource;

    public float start = 0.2f;
    public float end = 0.7f;

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = MakeSubclip(audioSource.clip, start, end);
        //audioSource.time = audioSource.clip.length * 0.3f;
    }

    private void Start() 
    {
        player.OnStateChanged += OnPlayerStateChanged;
    }

    private void OnPlayerStateChanged(object sender, EventArgs e) 
    {
        var playSound = player.playerState == PlayerState.Running;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            //audioSource.Pause();
        }
    }

    private AudioClip MakeSubclip(AudioClip clip, float start, float stop)
    {
        /* Create a new audio clip */
        int frequency = clip.frequency;
        float timeLength = stop - start;
        int samplesLength = (int)(frequency * timeLength);
        AudioClip newClip = AudioClip.Create(clip.name + "-sub", samplesLength, 1, frequency, false);

        /* Create a temporary buffer for the samples */
        float[] data = new float[samplesLength];
        /* Get the data from the original clip */
        clip.GetData(data, (int)(frequency * start));
        /* Transfer the data to the new clip */
        newClip.SetData(data, 0);

        /* Return the sub clip */
        return newClip;
    }
}
