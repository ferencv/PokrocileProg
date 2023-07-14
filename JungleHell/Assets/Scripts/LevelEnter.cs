using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Player.Instance != null) 
        {
            Player.Instance.transform.position = transform.position;
            Player.Instance.transform.forward = transform.forward;
        }
    }
}
