using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_CloneTeamFight;

public class CloneInviteFriendWindow : BaseWindow {

    UI_CloneInviteFriendWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_CloneInviteFriendWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);
    }
}
