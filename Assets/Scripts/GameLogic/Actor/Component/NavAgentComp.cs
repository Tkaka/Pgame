using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class NavAgentComp : BaseComponent, IMoveAble
{
    // NavAgent
    protected NavMeshAgent mAgent;

    // 速率
    private float mVelocity = 2.5f;

    public void Init(NavMeshAgent agent, float velocity)
    {
        this.mAgent = agent;
        this.mVelocity = velocity;
    }

    public void Move()
    {
        //mAgent.Move();
    }

    public void Move(Vector3 dir)
    {
        
    }

    public void MoveTo(Vector3 targetPos)
    {
        if (mAgent != null)
        {
            mAgent.SetDestination(targetPos);
        }
    }

    private static NavMeshPath path = new NavMeshPath();
    public bool IsWalkable(Vector3 resPos)
    {
        if (mAgent.pathStatus == NavMeshPathStatus.PathInvalid)
            return false;
        mAgent.CalculatePath(resPos, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    public void SetVelocity(float v)
    {
        mVelocity = v;
    }

}
