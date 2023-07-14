using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovRenderer : MonoBehaviour
{

    //[SerializeField]
    private float fovAngle = 40f;
    private float fovHeight = 11f;
    //[SerializeField]
    //private GameObject fovGameObject;
    [SerializeField]
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        var width = Utils.GetFovWidth(fovHeight, Utils.AngleToRadians(fovAngle));
        //Debug.Log(width);
        var mesh = Utils.CreateTriangleMesh(fovHeight, width);
        Utils.AssignMesh(gameObject, mesh, material);
    }

}
