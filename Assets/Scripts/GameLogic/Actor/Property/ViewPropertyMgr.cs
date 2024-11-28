
public class ViewPropertyMgr
{

    private Actor mOwner;

    // 实体属性
    private ActorProperty mProperty;
    // 实体属性当前值
    private ActorProperty mPropertyNow;


    public ViewPropertyMgr(Actor actor)
    {
        mOwner = actor;
    }

    public void Initialize()
    {
        mProperty = new ActorProperty();
        mPropertyNow = new ActorProperty();
    
        mProperty.setProperty(PropertyType.Hp, mOwner.getBaseProperty(PropertyType.Hp));
        mProperty.setProperty(PropertyType.Mp, mOwner.getBaseProperty(PropertyType.Mp));
 

        //控制类表现
        mProperty.setProperty(PropertyType.IsNumbness, 0);
        mProperty.setProperty(PropertyType.IsDizziness, 0);
        mProperty.setProperty(PropertyType.IsIce, 0);
        mProperty.setProperty(PropertyType.IsSilence, 0);

        mPropertyNow.equals(mProperty);
        mPropertyNow.setProperty(PropertyType.Mp, mOwner.getProperty(PropertyType.Mp));
        mPropertyNow.setProperty(PropertyType.Hp, mOwner.getProperty(PropertyType.Hp));
    }

    public void SetProperty(PropertyType type, LNumber val, bool showBar=true)
    {
        if (type == PropertyType.Hp || type == PropertyType.Mp)
        {
            if (val > GetBaseProperty(type))
                val = GetBaseProperty(type);
            else if (val <= 0)
                val = 0;
        }
        if (type == PropertyType.Hp && showBar)
            mOwner.CdMgr.addCoolDown("hpbar", 2);
        mPropertyNow.setProperty(type, val);
    }


    public void ChangeProperty(PropertyType type, LNumber val)
    {
        LNumber after = GetProperty(type) + val;
        if (type == PropertyType.Hp || type == PropertyType.Mp)
        {
            if (after > GetBaseProperty(type))
                after = GetBaseProperty(type);
            else if (after <= 0)
                after = 0;
            if (type == PropertyType.Hp)
                mOwner.CdMgr.addCoolDown("hpbar", 2);
            //if (type == PropertyType.Mp)
            //    Logger.err(mOwner.Name + "表现加怒气" + val);
            mPropertyNow.setProperty(type, after);
            return;
        }
        mPropertyNow.changeProperty(type, val);
    }

    public LNumber GetProperty(PropertyType type)
    {
        return mPropertyNow.getProperty(type);
    }

    public LNumber GetBaseProperty(PropertyType type)
    {
        return mProperty.getProperty(type);
    }

}

