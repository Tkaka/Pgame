using System.Collections.Generic;

/// <summary>
/// buff列表
/// </summary>
public class BuffMgr
{
    /// <summary>
    /// 是否麻痹
    /// </summary>
    public bool IsNumbness;

    /// <summary>
    /// 是否眩晕
    /// </summary>
    public bool IsDizziness;

    /// <summary>
    /// 免疫控制优先级
    /// </summary>
    public int ImmuneCtrlPriority;

    /// <summary>
    /// 免疫debuff优先级
    /// </summary>
    public int ImmuneDebuffPriority;

    public List<BaseBuff> BuffList { private set; get; }

    //buff特效控制器 
    public BuffEftCtrl BuffEftCtrl { private set; get; }

    private Actor owner;

    public BuffMgr(Actor owner)
    {
        this.owner = owner;
        BuffList = new List<BaseBuff>();
        BuffEftCtrl = new BuffEftCtrl(owner);
    }

    public int OnTurnStart()
    {
        int count = 0;

        for (int i = BuffList.Count - 1; i >= 0; i--)
        {
            //需要有时间表现的buff(暂时定为只有属性buff需要表现)
            if (BuffList[i].BuffType == BuffType.HotDotBuff)
            {
                count++;
            }
            BuffList[i].OnTurnStart();
        }
        return count;
    }

    public void Add(BaseBuff buff)
    {
        if (owner == null || buff == null)
            return;

        //有此状态，优先级N以下的控制状态都免疫
        if (buff.BuffBean.t_is_ctrl == 1)
        {
            LNumber immuneCtrlPriority = owner.getProperty(PropertyType.ImmuneCtrlPriority);
            if (immuneCtrlPriority >= buff.BuffBean.t_priority)
            {
                Logger.log(buff.TemplateId + " 被免疫控制 ");
                return;
            }
        }

        //有此状态，优先级N以下Debuff都免疫
        if (buff.BuffBean.t_type == (int)SUB_ADD.Sub)
        {
            LNumber immuneDebuffPriority = owner.getProperty(PropertyType.ImmuneDebuffPriority);
            if (immuneDebuffPriority >= buff.BuffBean.t_priority)
            {
                Logger.log(buff.TemplateId + " 被免疫debuff");
                return;
            }
        }

        bool needAdd = false;
        if (BuffList.Count <= 0)
        {
            needAdd = true;
        }
        else
        {
            for (int i = BuffList.Count - 1; i >= 0; i--)
            {
                BaseBuff temp = BuffList[i];
                //如果没有叠加标识
                if (temp.GetReplaceMark() == buff.GetReplaceMark())
                {
                    if (buff.GetReplaceType() == (int)BuffReplaceType.ReplaceByPriority)
                    {
                        if (buff.BuffBean.t_priority > temp.BuffBean.t_priority)
                        {
                            //BuffList.RemoveAt(i);
                            Remove(temp, false);
                            needAdd = true;
                            Logger.log(temp.TemplateId + "buff被优先级高的替换：" + buff.TemplateId);
                        }
                    }
                    else if (buff.GetReplaceType() == (int)BuffReplaceType.Refresh)
                    {
                        //BuffList.RemoveAt(i);
                        Remove(temp, false);
                        needAdd = true;
                        Logger.log(temp.TemplateId + "buff被新的buff刷新：" + buff.TemplateId);
                    }
                    else if (buff.GetReplaceType() == (int)BuffReplaceType.Overlay)
                    {
                        needAdd = true;
                    }
                }
                else
                {
                    needAdd = true;
                }
            }
        }

        if (needAdd)
        {
            Logger.log(owner.getTemplateId() + owner.Name + "__buff added__" + buff.TemplateId);
            buff.OnAdd(owner);
            //此次技能生效的buff不添加到buff list
            //if (buff.ImpactTime != BuffImpactTime.CurSkill)
            BuffList.Add(buff);
        }

    }

    public void Remove(BaseBuff buff, bool fromTurnStart)
    {
        if (buff != null)
        {
            BuffList.Remove(buff);
            buff.OnRemove(fromTurnStart);
        }
    }

    private void Remove(int index, bool fromTurnStart)
    {
        BaseBuff buff = BuffList[index];
        if (buff != null)
        {
            BuffList.RemoveAt(index);
            buff.OnRemove(fromTurnStart);
        }
    }

    /// <summary>
    /// 是否存在某个buffid
    /// </summary>
    /// <param name="buffId"></param>
    /// <returns></returns>
    public bool Exist(int buffId)
    {
        for (int i = 0; i < BuffList.Count; i++)
        {
            if (BuffList[i].TemplateId == buffId)
                return true;
        }
        return false;
    }

    public BaseBuff Get(int buffId)
    {
        for (int i = 0; i < BuffList.Count; i++)
        {
            if (BuffList[i].TemplateId == buffId)
                return BuffList[i];
        }
        return null;
    }

    /// <summary>
    /// 驱散(优先级N以下的buff)
    /// </summary>
    public void Dispel(int priority, DispelType dispelType, bool fromTurnStart)
    {
        for (int i = BuffList.Count - 1; i >= 0; i--)
        {
            BaseBuff temp = BuffList[i];
            if (dispelType != DispelType.All)
            {
                if (dispelType == DispelType.Add && temp.SubOrAdd != SUB_ADD.Add)
                    continue;
                else if (dispelType == DispelType.Sub && temp.SubOrAdd != SUB_ADD.Sub)
                    continue;
                else if (dispelType == DispelType.Ctrl && temp.BuffBean.t_is_ctrl != 1)
                    continue;
            }
            if (BuffList[i].BuffBean.t_priority < priority)
            {
                Remove(i, fromTurnStart);
            }
        }
    }

    /// <summary>
    /// 复活 
    /// </summary>
    public bool Reborn()
    {
        Logger.log("复活处理哦");
        RebornBuff rbuff = null;
        for (int i = BuffList.Count - 1; i >= 0; i--)
        {
            BaseBuff temp = BuffList[i];
            if (temp is RebornBuff)
            {
                rbuff = (RebornBuff)temp;
                break;
            }
        }
        
        if (rbuff != null)
        {
            //清除1000一下的buff
            Dispel(1000, DispelType.All, true);
            rbuff.DoReborn();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 是否会复活
    /// </summary>
    /// <returns></returns>
    public bool WillReborn()
    {
        Logger.log("复活处理哦");
        RebornBuff rbuff = null;
        for (int i = BuffList.Count - 1; i >= 0; i--)
        {
            BaseBuff temp = BuffList[i];
            if (temp is RebornBuff)
            {
                rbuff = (RebornBuff)temp;
                break;
            }
        }
        if (rbuff != null)
            return rbuff.WillReborn();
        return false;
    }

    public void Clear()
    {
        BuffList.Clear();
        BuffEftCtrl.Clear();
    }

}
