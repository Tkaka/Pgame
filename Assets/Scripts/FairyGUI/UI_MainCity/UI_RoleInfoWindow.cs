/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_RoleInfoWindow : GComponent
	{
		public GGraph m_mask;
		public GComponent m_headIcon;
		public GTextField m_nameLabel;
		public UI_modifyName m_modifyNameBtn;
		public UI_modifyHeadIcon m_modifyHeadIconBtn;
		public GTextField m_levelLabel;
		public GProgressBar m_expProgress;
		public GTextField m_expProgressTip;
		public GTextField m_achievementName;
		public GTextField m_numberLabel;
		public GTextField m_guildName;
		public GTextField m_fightPowerLabel;
		public GTextField m_lvLimitLabel;
		public GTextField m_tiLiTimeLabel;
		public GButton m_closeBtn;
		public GTextField m_fullLevelTip;

		public const string URL = "ui://jdfufi06kho74r";

		public static UI_RoleInfoWindow CreateInstance()
		{
			return (UI_RoleInfoWindow)UIPackage.CreateObject("UI_MainCity","RoleInfoWindow");
		}

		public UI_RoleInfoWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_headIcon = (GComponent)this.GetChildAt(3);
			m_nameLabel = (GTextField)this.GetChildAt(5);
			m_modifyNameBtn = (UI_modifyName)this.GetChildAt(6);
			m_modifyHeadIconBtn = (UI_modifyHeadIcon)this.GetChildAt(7);
			m_levelLabel = (GTextField)this.GetChildAt(9);
			m_expProgress = (GProgressBar)this.GetChildAt(11);
			m_expProgressTip = (GTextField)this.GetChildAt(12);
			m_achievementName = (GTextField)this.GetChildAt(14);
			m_numberLabel = (GTextField)this.GetChildAt(16);
			m_guildName = (GTextField)this.GetChildAt(18);
			m_fightPowerLabel = (GTextField)this.GetChildAt(20);
			m_lvLimitLabel = (GTextField)this.GetChildAt(22);
			m_tiLiTimeLabel = (GTextField)this.GetChildAt(24);
			m_closeBtn = (GButton)this.GetChildAt(25);
			m_fullLevelTip = (GTextField)this.GetChildAt(26);
		}
	}
}