using System.Collections.Generic;
using UnityEngine;

public class PathParam
{
    public List<Vector3> path = new List<Vector3>();

    public Vector3 dir;

    public float dur;

    public Vector3? GetLastPos()
    {
        if (path != null && path.Count > 0)
        {
            return path[path.Count - 1];
        }
        return null;
    }

}