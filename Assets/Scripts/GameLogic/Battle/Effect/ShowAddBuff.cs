
using Data.Beans;
using UnityEngine;

public class ShowAddBuff : ShowBase
{

    public int buffId;

    public override void Show()
    {
        Actor actor = ActorManager.Singleton.Get(actorId);
        if (actor != null)
        {
            //预制件表现
            t_buffBean bean = ConfigBean.GetBean<t_buffBean, int>(buffId);
            if (!string.IsNullOrEmpty(bean.t_prefab))
            {
                //将buff保存到actor指定的buff上
                actor.BuffMgr.BuffEftCtrl.AddBuffEft(bean);
            }

            //UI表现
            if (!string.IsNullOrEmpty(bean.t_icon))
            {
                if (actor.TransformExt != null)
                {
                    new BuffTip(actor, bean.t_icon);
                }
            }

            //头像UI表现
            if (bean.t_is_ctrl == 1)
            {
                PropertyType propertyType = PropertyType.MaxPropertyType;
                if (System.Enum.IsDefined(typeof(PropertyType), bean.t_property_id))
                    propertyType = (PropertyType)bean.t_property_id;
                else
                    Logger.err("标记buff效果属性参数错误：" + bean.t_property_id);

                //
                actor.ViewPropertyMgr.SetProperty(propertyType, actor.getProperty(propertyType));

                if (actor.isCampOf(ActorCamp.CampFriend))
                {
                    //如果是 眩晕 麻痹 冰冻 沉默处理头像
                    if (propertyType == PropertyType.IsDizziness ||
                        propertyType == PropertyType.IsNumbness ||
                        propertyType == PropertyType.IsIce ||
                        propertyType == PropertyType.IsSilence)
                    {
                        actor.headBar.ResetStatus();
                    }
                }
                
                //动作表现
                if (actor.ViewPropertyMgr.GetProperty(PropertyType.IsDizziness) > 0 && actor.isStateOf(ActorState.idle))
                    actor.GetActionManager().PlayCommonAnimation(AniName.xuanyun.ToString());
            }

        }
    }


    public override string Print()
    {
        Actor actor = ActorManager.Singleton.Get(actorId);
        if (actor != null)
        {
            return actor.Name + "should add buff " + buffId;
        }
        return "";
    }


}
