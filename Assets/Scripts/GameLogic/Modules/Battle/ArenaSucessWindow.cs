using Data.Beans;
using Message.Bag;
using Message.Dungeon;
using Message.Fight;
using UI_Battle;
using Message.Challenge;
using System.Collections.Generic;
using System;
using Message.Pet;
using UI_Common;



//竞技场挑战胜利窗口
public class ArenaSucessWindow : BaseWindow
{
    private UI_ArenaSucessWindow m_window;
    private ResFightResultInfo m_msg;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ArenaSucessWindow>();
        m_window.m_btnExit.onClick.Add(CloseBtn);
        m_window.m_btnSeeAgain.onClick.Add(_OnSeeAgainClick);
        m_msg = Info.param as ResFightResultInfo;
        if (m_msg == null)
        {
            Close();
            return;
        }

        _ShowWinnerInfo();
        _ShowLoserInfo();
    }


    //显示胜利者信息
    private void _ShowWinnerInfo()
    {
        ArenaResult info = m_msg.result as ArenaResult;
        if (info == null)
            return;

        m_window.m_txtWinName.text = info.winName;
        m_window.m_txtWinRank.text = info.winRank + "";
        m_window.m_txtUpRank.text = info.upRank + "";
        m_window.m_txtUpRank.visible = info.upRank > 0;
        m_window.m_winPetList.RemoveChildren(0, -1, true);
        for (int i = 0; i < info.winPetInfos.Count; i++)
        {
            EquipedPetInfo petInfo = info.winPetInfos[i];
            UI_BattlePetIcon cell = UI_BattlePetIcon.CreateInstance();
            _ShowPetIconInfo(petInfo, cell);
            m_window.m_winPetList.AddChild(cell);
        }
    }

    private void _ShowPetIconInfo(EquipedPetInfo petInfo, UI_BattlePetIcon cell)
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petInfo.id);

        if (petBean == null)
            return;

        // 图片
        if (petBean != null)
            UIGloader.SetUrl(cell.m_headLoader, UIUtils.GetIconPath(petBean, petInfo.star));
        if (petInfo != null)
            UIGloader.SetUrl(cell.m_borderLoder, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petInfo.color)));

        StarList starList = new StarList((UI_StarList)cell.m_starList);
        starList.SetStar(petInfo.star);

        cell.m_lvTxt.text = petInfo.level + "";

    }

    //失败者信息
    private void _ShowLoserInfo()
    {
        ArenaResult info = m_msg.result as ArenaResult;
        if (info == null)
            return;

        m_window.m_txtLoserName.text = info.loserName;
        m_window.m_txtLoserRank.text = info.loserRank + "";

        m_window.m_loserPetList.RemoveChildren(0, -1, true);
        for (int i = 0; i < info.loserPetInfos.Count; i++)
        {
            EquipedPetInfo petInfo = info.loserPetInfos[i];
            UI_BattlePetIcon cell = UI_BattlePetIcon.CreateInstance();
            _ShowPetIconInfo(petInfo, cell);
            m_window.m_loserPetList.AddChild(cell);
        }
    }


    private void _OnSeeAgainClick()
    {
        //ReplayService.Singleton.SetPlayAgainFlag(true);
        //BattleService.Singleton.QuitBattle();
        ReplayService.Singleton.StartPlay();

        //SceneLoader.Singleton.nextState = GameState.BattleReplay;
        //SceneLoader.Singleton.sceneName = GSceneName.MaiCity;


        //GameManager.Singleton.changeState(GameState.Loading, param);
    }

    public void CloseBtn()
    {
        Close();
        BattleService.Singleton.QuitBattle();
    }

    protected override void OnClose()
    {
        base.OnClose();
 
    }
}