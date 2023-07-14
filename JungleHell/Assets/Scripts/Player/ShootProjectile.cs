using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;
    [SerializeField] private Player player;
    [SerializeField] private Material projectileTracerMaterial;
    [SerializeField] private Sprite shootFlashSprite;
    //private float gunRange = 11f;
    //private float gunSpread = 36f;

    private void Awake() 
    {
        //player.OnShoot += ShootProjectile_OnShoot;
    }

    private void ShootProjectile_OnShoot(object sender, Player.OnShootEventArgs e) 
    {
        var bulletsCount = 20;
        var splashAngle = 30f;
        var splashAngleDelta = splashAngle / (float)bulletsCount;
        //for (var i = 0; i < bulletsCount; i++) 
        {
            var bulletTransform = Instantiate(pfBullet, e.gunEndPointPosition, Quaternion.identity);
            var shootDirection = e.shootPosition.normalized;
            bulletTransform.GetComponent<Bullet>().SetShootDirection(shootDirection, e.gunEndPointPosition);
        }
    }

    //private void CreateProjectileTracer(Vector3 gunPosition, Vector3 gunDirection) 
    //{
    //    var tmpMaterial = new Material(projectileTracerMaterial);
    //    tmpMaterial.SetTextureScale("_MainTex", new Vector3(1,1, gunRange  / 256f)); 
    //    var mesh = Utils.CreateRectangleMesh(gunPosition, gunDirection, 6f, gunRange, tmpMaterial);

    //    int frame = 0;
    //    float framerate = 0.016f;
    //    float time = framerate;
    //    mesh.SetUVCoords();
    //}

    //private void CreateShootFlash(Vector3 position) 
    //{
    //    //var sprite = 
    //}

    

}
