
using Data.Beans;

public class ShowRemoveBuff : ShowBase
{
    public int buffId;

    /// <summary>
    /// 移除buff特效 和 UI表现
    /// </summary>
    public override void Show()
    {
        Actor actor = ActorManager.Singleton.Get(actorId);
        if (actor != null)
        {
            //预制件表现
            t_buffBean bean = ConfigBean.GetBean<t_buffBean, int>(buffId);
            if (!string.IsNullOrEmpty(bean.t_prefab))
            {
                actor.BuffMgr.BuffEftCtrl.RemoveBuffEft(bean);
            }
            //移除UI表现


            //头像UI表现
            if (bean.t_is_ctrl == 1)
            {
                PropertyType propertyType = PropertyType.MaxPropertyType;
                if (System.Enum.IsDefined(typeof(PropertyType), bean.t_property_id))
                    propertyType = (PropertyType)bean.t_property_id;
                else
                    Logger.err("标记buff效果属性参数错误：" + bean.t_property_id);

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
                if (actor.ViewPropertyMgr.GetProperty(PropertyType.IsDizziness) <= 0 && actor.isStateOf(ActorState.idle))
                    actor.GetActionManager().PlayCommonAnimation(ActorState.idle);
            }

        }
    }

}