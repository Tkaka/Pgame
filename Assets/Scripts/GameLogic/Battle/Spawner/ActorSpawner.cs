using UnityEngine;

public class ActorSpawner : BaseBehaviour
{
    [HideInInspector]
    public int Id;            //站位

    public int actorId;      //表ID

    public ActorType Type = ActorType.Monster;

    [HideInInspector, System.NonSerialized]
    public long roleId;

    [HideInInspector, System.NonSerialized]
    public ActorCamp camp = ActorCamp.CampEnemy;
}