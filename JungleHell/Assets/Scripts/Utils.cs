using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    public static Vector3 CopyVector3(Vector3 src) 
    {
        return new Vector3(src.x, src.y, src.z);
    }

    public static bool IsHit(Vector3 targetPosition, Vector3 gunPosition, Vector3 gunDirection, float gunRange, float gunSpread) 
    {
        var tp = CopyVector3(targetPosition);
        var gp = CopyVector3(gunPosition);
        var gd = CopyVector3(gunDirection);
        tp.y = 0;
        gp.y = 0;
        gd.y = 0;
        var targetDir = tp - gp;
        if(targetDir.magnitude > gunRange)
            return false;
        //Debug.Log(targetPosition.ToString() +"," +gunPosition.ToString() + "," + gunDirection.ToString() + "," + gunSpread.ToString());
        Debug.Log(Vector3.Angle(targetDir, gd));
        //targetDir.y = 0;
        return Vector3.Angle(targetDir, gd) * 2f < gunSpread;
    }

    public static float AngleToRadians(float angle) 
    {
        return (Mathf.PI * angle) / 180f;
    }

    public static float GetFovWidth(float fovHeight, float fovAngle) 
    {
        fovAngle = Mathf.Min(fovAngle, 175f);
        var tan = Mathf.Abs(Mathf.Tan(fovAngle / 2f));
        return (fovHeight * tan) * 2f;
    }

    public static Mesh CreateTriangleMesh(float height, float width)
    {
        var vertices = new Vector3[3];
        var uv = new Vector2[3];
        var triangles = new int[3];

        var halfWidth = width / 2f;

        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(-halfWidth, height);
        vertices[2] = new Vector3(halfWidth, height);

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(-1, 1);
        uv[2] = new Vector2(1, 1);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;


        var mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        return mesh;
    }

    public static void AssignMesh(GameObject gameObject, Mesh mesh, Material material) 
    {
        //var gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        //gameObject.transform.localScale = new Vector3(30, 30, 1);

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = material;
    }

    //public static Mesh CreateRectangleMesh(Vector3 position, Vector3 direction, float width, float height, Material material)
    //{
    //    var vertices = new Vector3[4];
    //    var uv = new Vector2[4];
    //    var triangles = new int[6];

    //    vertices[0] = new Vector3(0,1);
    //    vertices[1] = new Vector3(1,1);
    //    vertices[2] = new Vector3(0,0);
    //    vertices[3] = new Vector3(1,0);

    //    uv[0] = new Vector2(0,1);
    //    uv[1] = new Vector2(1,1);
    //    uv[2] = new Vector2(0,1);
    //    uv[3] = new Vector2(1,0);

    //    triangles[0] = 0;
    //    triangles[1] = 1;
    //    triangles[2] = 2;
    //    triangles[3] = 2;
    //    triangles[4] = 1;
    //    triangles[5] = 3;

    //    var mesh = new Mesh();
    //    mesh.vertices = vertices;
    //    mesh.uv = uv;
    //    mesh.triangles = triangles;

    //    var gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
    //    gameObject.transform.localScale = new Vector3(30, 30, 1);

    //    gameObject.GetComponent<MeshFilter>().mesh = mesh;
    //    gameObject.GetComponent<MeshRenderer>().material = material;
    //}

}
