using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootExplosion : MonoBehaviour
{
    [SerializeField] private Transform pfExplossion;
    //private float gunRange = 11f;
    //private float gunSpread = 36f;

    private void Start()
    {
        Player.Instance.OnShoot += CreateExplosion;
    }

    private void CreateExplosion(object sender, Player.OnShootEventArgs e)
    {
        if (e.hasAmmo) 
        {
            Debug.Log("Shoot");//e.gunEndPointPosition
            var explossionTransform = Instantiate(pfExplossion, e.gunEndPointPosition, Quaternion.identity);
            var shootDirection = e.shootPosition.normalized;
            explossionTransform.GetComponent<GunExplosion>().SetShootDirection(shootDirection, e.gunEndPointPosition);
        }
    }
}
