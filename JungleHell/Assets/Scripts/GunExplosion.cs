using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunExplosion : MonoBehaviour
{
    private Vector3 shootDirection;
    private float speed = 10f;

    public void SetShootDirection(Vector3 shootDirection, Vector3 initialPosition)
    {
        this.shootDirection = shootDirection.normalized;
        var newDir = new Vector3(shootDirection.y, shootDirection.z, shootDirection.x);
        transform.forward = shootDirection;
        Destroy(gameObject, 0.1f);
    }

    private void Update() 
    {
        if (shootDirection != null) 
        {
            transform.position += (shootDirection * speed * Time.deltaTime);
        }
    }
}
