using UI_Battle;
using Data.Beans;
using UnityEngine;
using FairyGUI;

public class BossBriefWindow : BaseWindow
{
    private UI_BossBriefWindow window;

    public override void OnOpen()
    {
        base.OnOpen();
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_BossBriefWindow>();
        window.m_bg.onClick.Add(OnCloseBtn);
        ShowModels();
        ShowDesc();
    }

    private void ShowDesc()
    {
        int bossId = (int)Info.param;
        if (bossId > 0)
        {
            t_monster_boosBean bossBean = ConfigBean.GetBean<t_monster_boosBean, int>(bossId);
            if (bossBean != null)
            {
                window.m_zizhiTxt.text = bossBean.t_zizhi + "";
                window.m_dingweiTxt.text = bossBean.t_dingwei;
                window.m_bossName.text = bossBean.t_name;
                window.m_desc.text = bossBean.t_desc;
                int skillId = UIUtils.GetMasterSkillID(bossBean.t_id);
                t_skillBean skillBean = ConfigBean.GetBean<t_skillBean, int>(skillId);
                if (skillBean != null)
                {
                    window.m_skillDesc.text = skillBean.t_desc;
                }
            }
        }
    }

    private ActorUI actor;
    public void ShowModels()
    {
        int bossId = (int)Info.param;
        if (bossId > 0)
        {
            t_monster_boosBean bossBean = ConfigBean.GetBean<t_monster_boosBean, int>(bossId);
            if (bossBean != null)
            {
                GoWrapper wrapper = new GoWrapper();
                CacheWrapper(window.m_modelHolder);
                window.m_modelHolder.SetNativeObject(wrapper);
                ActorUI actor = NewActorUI(bossBean.t_id, ActorType.Boss, wrapper);

                float scale = 200f;
                var sb = ConfigBean.GetBean<t_model_scaleBean, string>(bossBean.t_prefab);
                if (sb != null) scale = sb.t_boss_biref / 100.0f;

                actor.SetTransform(new Vector3(0, 0, 1000), scale, new Vector3(0, 180, 0));
            }
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
        GED.ED.dispatchEvent(EventID.OnBossBriefWindowClose);
        if (actor != null)
        {
            actor.destoryMe();
        }
    }

}
