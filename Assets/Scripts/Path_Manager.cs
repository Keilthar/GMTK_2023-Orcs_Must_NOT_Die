using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Path_Manager : MonoBehaviour
{
#region Singleton
    public static Path_Manager Singleton;
    private void Awake()
    {
        if (Singleton!= null)
        {
            Debug.Log("Duplicated " + this.name);
            return;
        }
        else Singleton= this;
    }
#endregion
    SplineContainer path;

    void Start()
    {
        path = transform.GetComponent<SplineContainer>();
    }

    public Vector3 Path_GetPosition(float Distance)
    {
        Vector3 path_Position;
        path_Position = path.Spline.GetPointAtLinearDistance(Distance/path.Spline.GetLength(), 0, out float point);
        path_Position += transform.position;

        return path_Position;
    }

}
