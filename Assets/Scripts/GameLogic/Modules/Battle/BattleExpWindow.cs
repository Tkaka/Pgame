using Message.Dungeon;
using UI_Battle;
using Message.Fight;

public class BattleExpWindow : BaseWindow
{

    private UI_BattleExpWindow window;

    public override void OnOpen()
    {
        base.OnOpen();
        //OpenChild<BattleVictoryWindow>(Info);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        window = getUiWindow<UI_BattleExpWindow>();
        //window.m_bgRight.x = 0;
        //window.m_bgRight.x = window.m_bgLeft.x + window.m_bgLeft.width;
        window.m_bgRight.x += 1f;
        window.onClick.Add(OnBgClick);
        ShowPets();
    }

    private void ShowPets()
    {
        MissionResult res = BattleService.Singleton.FightRes;
        if (res != null && res.petInfos != null)
        {
            for(int i=0; i<res.petInfos.Count; i++)
            {
                BattleExpItem expItem = BattleExpItem.CreateInstance();
                expItem.petId = res.petInfos[i].petId;
                expItem.addExp = res.petExp;
                expItem.RefreshView();
                window.m_list.AddChild(expItem);
            }
        }
    }

    private void OnBgClick()
    {
        Close();
        BattleWindow.Singleton.OpenChild<BattleAwardWindow>(WinInfo.Create(false, Info.parentName, false));
    }

}
