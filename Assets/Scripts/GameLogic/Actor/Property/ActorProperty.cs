using System.Collections.Generic;

public class ActorProperty
{

    private Dictionary<int, LNumber> mProperties = null;

    public ActorProperty()
    {
        mProperties = new Dictionary<int, LNumber>();
    }


    public bool ExistProperty(PropertyType type)
    {
        int iType = (int)type;
        return mProperties.ContainsKey(iType);
    }

    /// <summary>
    /// 返回属性值
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public LNumber getProperty(PropertyType type, bool ignoreError=false)
    {
        int iType = (int)type;
        if (mProperties.ContainsKey(iType))
        {
            return mProperties[iType];
        }
        else
        {
            if(!ignoreError)
                Logger.err("不存在的属性类型：[" + type + "]");
        }
        return 0;
    }

    /// <summary>
    /// 设置属性值
    /// </summary>
    /// <param name="type"></param>
    /// <param name="val"></param>
    public void setProperty(PropertyType type, LNumber val)
    {
        int iType = (int)type;
        if (mProperties.ContainsKey(iType))
            mProperties[iType] = val;
        else
            mProperties.Add(iType, val);
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void clear()
    {
        mProperties.Clear();
    }

    public void changeProperty(PropertyType property, LNumber delta)
    {
        LNumber old = getProperty(property);
        LNumber after = old + delta;
        setProperty(property, after);
    }

    public void equals(ActorProperty actorProperty)
    {
        foreach (var kv in actorProperty.mProperties)
        {
            if (mProperties.ContainsKey(kv.Key))
                mProperties[kv.Key] = kv.Value;
            else
                mProperties.Add(kv.Key, kv.Value);
        }
    }

    public Dictionary<int, LNumber> GetPropertyDic()
    {
        return mProperties;
    }
}

