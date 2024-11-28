using UI_Battle;
using Data.Beans;
using System.Collections;
using UnityEngine;

public class BattleDialogWindow : BaseWindow
{

    private UI_BattleDialogWindow window;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_BattleDialogWindow>();
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        if (bean != null)
        {
            if (SpawnerHelper.Singleton.CurWave == 0)
            {
                if (!string.IsNullOrEmpty(bean.t_wave_dialog1))
                {
                    CoroutineManager.Singleton.startCoroutine(ShowDialog(bean.t_wave_dialog1));
                }
            }
            else if (SpawnerHelper.Singleton.CurWave == 1)
            {
                if (!string.IsNullOrEmpty(bean.t_wave_dialog2))
                {
                    CoroutineManager.Singleton.startCoroutine(ShowDialog(bean.t_wave_dialog2));
                }
            }
            else if (SpawnerHelper.Singleton.CurWave == 2)
            {
                if (!string.IsNullOrEmpty(bean.t_wave_dialog2))
                {
                    CoroutineManager.Singleton.startCoroutine(ShowDialog(bean.t_wave_dialog3));
                }
            }
        }
    }

    public IEnumerator ShowDialog(string dialogIdStr)
    {
        int[] dialogIdArr = GTools.splitStringToIntArray(dialogIdStr);
        for (int i = 0; i < dialogIdArr.Length; i++)
        {
            t_dialogBean dialogBean = ConfigBean.GetBean<t_dialogBean, int>(dialogIdArr[i]);
            if (dialogBean.t_left_right == 1)
                ShowLeft(dialogBean.npc_name, dialogBean.npc_content);
            else if (dialogBean.t_left_right == 2)
                ShowRight(dialogBean.npc_name, dialogBean.npc_content);
            yield return new WaitForSeconds(2.0f);
        }
        Close();
    }

    public void ShowLeft(string name, string content)
    {
        window.m_leftName.visible = true;
        window.m_leftPanel.visible = true;
        window.m_rightName.visible = false;
        window.m_rightPanel.visible = false;
        window.m_leftName.text = name;
        window.m_dialogTxt.text = content;
    }

    public void ShowRight(string name, string content)
    {
        window.m_leftName.visible = false;
        window.m_leftPanel.visible = false;
        window.m_rightName.visible = true;
        window.m_rightPanel.visible = true;
        window.m_rightName.text = name;
        window.m_dialogTxt.text = content;
    }

    protected override void OnClose()
    {
        GED.ED.dispatchEvent(EventID.OnBattleDialogWindowClose);
        base.OnClose();
    }

}