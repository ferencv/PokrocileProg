using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ammo : MonoBehaviour
{
    public void GrabAmmo() 
    {
        SoundManager.Instance.PlayPlayerGrabAmmoSound();
        Debug.Log("Destroying ammo");
        Destroy(gameObject);
    } 

    private void DisposeAmmo(object sender, EventArgs e)
    {
        //if (!active) 
        //{
        //    return;
        //}
        //active = false;
        //Player.Instance.OnAmmoGrab -= DisposeAmmo;
    }
}
