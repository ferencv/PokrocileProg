using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitExplosion : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 0.1f);
    }
}
