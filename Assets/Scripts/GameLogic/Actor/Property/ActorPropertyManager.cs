using System;
using System.Collections.Generic;


public class ActorPropertyManager
{
    private Actor mOwner;    
    // 实体属性
    private ActorProperty mProperty;
    // 实体属性当前值
    private ActorProperty mPropertyNow;

    public ActorPropertyManager(Actor owner)
    {
        mOwner = owner;
    }

    public void Initialize()
    {
        mProperty = new ActorProperty();
        for (int i = (int)PropertyType.Atk; i < (int)PropertyType.MaxPropertyType; i++)
            mProperty.setProperty((PropertyType)i, 0);
        mPropertyNow = new ActorProperty();
        ActorInitHelper.InitProperty(mOwner);
    }

    public void CopyBaseToNow()
    {
        if (mPropertyNow != null && mProperty != null)
            mPropertyNow.equals(mProperty);
    }

    /// <summary>
    /// 仅仅供编辑器调用 
    /// </summary>
    public void InitForEditor()
    {
        mProperty = new ActorProperty();
        for (int i = (int)PropertyType.Atk; i < (int)PropertyType.MaxPropertyType; i++)
            mProperty.setProperty((PropertyType)i, 0);
        mPropertyNow = new ActorProperty();
        mPropertyNow.equals(mProperty);
    }
    /*----------------------------------------------------*/
    /// <summary>
    /// 仅仅初始化属性时调用 
    /// </summary>
    /// <param name="property"></param>
    /// <param name="val"></param>
    public void setBaseProperty(PropertyType property, LNumber val)
    {
        mProperty.setProperty(property, val);
    }

    public LNumber getBaseProperty(PropertyType type)
    {
        return mProperty.getProperty(type);
    }

    /*----------------------------------------------------*/

    public void setProperty(PropertyType property, LNumber val)
    {
        mPropertyNow.setProperty(property, val);
    }

    public LNumber getProperty(PropertyType type)
    {
        return mPropertyNow.getProperty(type);
    }

    /*----------------------------------------------------*/

    public void changeProperty(PropertyType type, LNumber val)
    {
        //if (type == PropertyType.Hp)
        //    val = val.Ceiling;      //保证血量永远只有整数部分

        LNumber after = getProperty(type) + val;
        if (type == PropertyType.Hp || type == PropertyType.Mp)
        {
            if (after > getBaseProperty(type))
                after = getBaseProperty(type);
            else if (after <= 0)
                after = 0;
            mPropertyNow.setProperty(type, after);
            return;
        }

        mPropertyNow.changeProperty(type, val);
    }

    public void clear()
    {
        mProperty.clear();
        mPropertyNow.clear();
    }

    public void reBuildProperty()
    {
        mPropertyNow.equals(mProperty);
    }

    public Dictionary<int, LNumber> GetBasePropertyDic()
    {
        return mProperty.GetPropertyDic();
    }

}

