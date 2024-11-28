using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_TongXiangGuan;
using FairyGUI;
using Message.Pet;

public class TongXiangMaterialBtn : UI_tongXiangMaterialBtn {
    /// <summary>
    /// 解锁的lv
    /// </summary>
    public int unlockLv;
    public int material;

    public new static TongXiangMaterialBtn CreateInstance()
    {
        return (TongXiangMaterialBtn)UIPackage.CreateObject("UI_TongXiangGuan", "tongXiangMaterialBtn");
    }

    public void Init()
    {
        InitView();
    }

    private void InitView()
    {
        bool isUnlock = IsUnlock();
        m_lockGroup.visible = !isUnlock;
        m_materialNameLabel.visible = isUnlock;
        m_btn.selected = false;

        m_lockLabel.text = string.Format("{0}级解锁", unlockLv);

        m_materialNameLabel.text = UIUtils.GetTongXiangMaterialName((TongXiangMaterial)material);
    }

    private bool IsUnlock()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.level >= unlockLv;
    }

    public void ClickItem(bool isSelected)
    {
        m_btn.selected = isSelected ;
    }
}
