using System.Collections.Generic;
using UnityEngine;

public class WayPointMgr : MonoBehaviour
{
    public static WayPointMgr Singleton;

    [System.Serializable]
    public class ActorPoint
    {
        public Transform born;
        public List<Transform> wayPoints = new List<Transform>();
    }
    
    public List<ActorPoint> points = new List<ActorPoint>();
    public ActorPoint playerPoint;

    private void Awake()
    {
        Singleton = this;
    }
}