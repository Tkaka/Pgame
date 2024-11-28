using UnityEngine;


public enum Sex
{
    Male = 0,              //男性
    Female = 1,          //女性
    Middlesex = 2,      //中性
}

/// <summary>
/// 实体基类
/// </summary>
public abstract class ActorBase
{
    //实体名字
    protected string mName;
    // 实体唯一Id
    protected long mId;
    // 实体模版Id
    protected int mTemplateId;
    // 实体类型
    protected ActorType mType;
    // 实体性别
    public Sex Sex { get; protected set; }
    //实体种族
    //public ActorRace Race { get; protected set; }
    //实体魂大类
    public SoulType SoulType { get; set; }
    //实体成长类型
    public PetType GrowType { get; protected set; }
    // 实体主显示对象
    protected GameObject mShowObj;
    // 阵营
    protected ActorCamp mCamp;
    // 实体位置
    protected Transform mTransform;
    // Unity3D beaviour
    protected ActorBehavior mBehavior;
    public ActorBehavior monoBehavior
    {
        get { return mBehavior; }
    }

    public GameObject ShowObj 
    {
        get
        {
            return mShowObj;
        }
        set
        {
            mShowObj = value;
            mBehavior = mShowObj.GetComponent<ActorBehavior>();
            if (mBehavior == null)
                mBehavior = mShowObj.AddComponent<ActorBehavior>();
            mBehavior.actorId = mId;
        }
    }

    public string Name
    {
        get
        {
            return mName;
        }
    }

    public virtual void setName(string name= "Actor")
    {
        mName = name;
    }

    public virtual void ChangeShowObj(GameObject obj)
    {
        mTransform = null;
        mShowObj = null;
        mTransform = obj.transform;
        mShowObj = obj;
    }


    public ActorBase(int templateId, ActorType actorType, ActorCamp camp, long actorId = 0)
    {
        mTemplateId = templateId;
        mType = actorType;
        mId = (actorId == 0 ? IdAssginer.getId(IdAssginer.IdType.ActorId) : actorId);
        mCamp = camp;
        setName();
    }


    /// <summary>
    /// 设置角色类型
    /// </summary>
    protected abstract void SetActorTypes();

    public Transform TransformExt
    {
        get
        {
            if (mShowObj == null)
                return null;
            if (mTransform == null)
                mTransform = mShowObj.GetComponent<Transform>();
            return mTransform;
        }
        set
        {
            mTransform = value;
        }
    }

    /// <summary>
    /// 返回实体类型
    /// </summary>
    /// <returns></returns>
    public ActorType getActorType()
    {
        return mType;
    }

    /// <summary>
    /// 是否是某种类型 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool isActorType(ActorType type)
    {
        return mType == type;
    }

    public bool IsGrowType(PetType type)
    {
        return GrowType == type;
    }

    public bool IsSex(Sex type)
    {
        return Sex == type;
    }

    /// <summary>
    /// 模版id
    /// </summary>
    /// <returns></returns>
    public int getTemplateId()
    {
        return mTemplateId;
    }

    /// <summary>
    /// 实体id
    /// </summary>
    /// <returns></returns>
    public long getActorId()
    {
        return mId;
    }

    /// <summary>
    /// 返回阵营类型
    /// </summary>
    /// <returns></returns>
    public ActorCamp getCamp()
    {
        return mCamp;
    }

    /// <summary>
    /// 设置阵营
    /// </summary>
    /// <param name="camp"></param>
    public void setCamp(ActorCamp camp)
    {
        mCamp = camp;
    }
    

    /// <summary>
    /// 是否为某个阵营
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public bool isCampOf(ActorCamp camp)
    {
        if (camp == ActorCamp.CampAll)
            return true;
        return (mCamp == camp);
    }

    // 实体被创建时调用
    public abstract bool initialize(ActorParam instanceData);

    // 释放
    public abstract void destoryMe();

    // fixed Update
    //public abstract void fixedUpdate();

    // 更新
    public abstract void update();

    // update后每帧调用
    //public abstract void lateUpdate();

    // 触发器进入时调用一次
    public abstract void onTriggerEnter(Collider other);

    // 触发器持续触发调用（每帧调用）
    public abstract void onTriggerStay(Collider other);

    // 触发器退出时调用
    public abstract void onTriggerExit(Collider other);

    public void setObjName(string name)
    {
        ShowObj.name = name;
    }

}

