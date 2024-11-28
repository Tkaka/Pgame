using UI_Chat;
using FairyGUI;
using UnityEngine;
using System;
using Data.Beans;
using Message.Chat;
using Message.Role;
using DG.Tweening;
using System.Collections.Generic;

public class ChatEmojiWnd : BaseWindow
{
    private UI_ChatEmojiWnd m_window;
    private Action<string> m_callBack;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ChatEmojiWnd>();
        m_window.m_btnClose.onClick.Add(_OnCloseWnd);
        m_window.m_imgBg.onClick.Add(_OnCloseWnd);
        OneParam<Action<string>> param = Info.param as OneParam<Action<string>>;
        if (param != null)
        {
            m_callBack = param.value;
        }

        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        _ShowEmoji();

        //m_window.position = new Vector3(m_window.position.x, m_window.position.y + m_window.m_wndGroup.height, m_window.position.z);
        //m_window.TweenMoveY(-m_window.m_wndGroup.height, 0.3f);
    }


    private void _ShowEmoji()
    {
        m_window.m_emojiList.RemoveChildren(0, -1, true);
        List<t_emojiBean> beans = ConfigBean.GetBeanList<t_emojiBean>();
        for (int i = 0; i < beans.Count; i++)
        {
            t_emojiBean bean = beans[i];
            UI_emojiCell cell = UI_emojiCell.CreateInstance();
            UIGloader.SetUrl(cell.m_imgLoad, bean.t_emoji_icon);
            //cell.m_imgLoad.url = bean.t_emoji_icon;
            m_window.m_emojiList.AddChild(cell);

            cell.onClick.Add(() => {
                if (m_callBack != null)
                {
                    m_callBack(bean.t_emoji_name);
                }
            });
        }
    }


    private void _OnCloseWnd()
    {
        m_window.TweenMoveY(m_window.m_wndGroup.height, 0.3f).OnComplete(Close); 
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}