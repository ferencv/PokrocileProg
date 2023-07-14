using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private bool isDestroyed = false;
    private Vector3 shootDirection;
    private Vector3 initialPosition;
    private float maxRange = 11f;

    public void SetShootDirection(Vector3 shootDirection, Vector3 initialPosition) 
    {
        this.initialPosition = initialPosition;
        this.shootDirection = shootDirection;
        //float angle = Mathf.Atan2(shootDirection.z, shootDirection.x) * Mathf.Rad2Deg;
        //if (angle < 0)
        //{
        //    angle += 360f;
        //}
        //Debug.Log("uhel: " +angle);
        //transform.eulerAngles = new Vector3(0, angle, 0);
        var newDir = new Vector3(shootDirection.y, shootDirection.z, shootDirection.x);
        transform.forward = shootDirection;
        //Destroy(gameObject, 0.1f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDestroyed) 
        {
            return;
        }
        float moveSpeed = 30f;
        transform.position += shootDirection * Time.deltaTime * moveSpeed;
        var range = (transform.position - initialPosition).magnitude;
        //Debug.Log(range);
        if (range > maxRange) 
        {
            Destroy(gameObject);
            isDestroyed = true;
        }
    }

    private void OnTriggerEnter(Collider collision) 
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy != null) 
        {
            Debug.Log("Enemy hit");
            enemy.Kill();
        }
    }
}
