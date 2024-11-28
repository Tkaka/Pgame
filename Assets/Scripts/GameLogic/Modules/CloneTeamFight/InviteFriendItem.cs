using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_CloneTeamFight;

public class InviteFriendItem : UI_inviteFriendItem {

    public new static InviteFriendItem CreateInstance()
    {
        return UI_inviteFriendItem.CreateInstance() as InviteFriendItem;
    }
}
