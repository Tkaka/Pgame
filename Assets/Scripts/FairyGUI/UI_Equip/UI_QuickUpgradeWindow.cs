/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_QuickUpgradeWindow : GComponent
	{
		public GGraph m_mask;
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public GComponent m_starList;
		public GTextField m_lvLabel;
		public GTextField m_nameLabel;
		public GButton m_reduceBtn;
		public GButton m_addBtn;
		public GTextField m_targetPingJie;
		public GList m_quickItemList;
		public GTextField m_goldNum;
		public GLoader m_coinLoader;
		public GTextField m_coinNumLabel;
		public GButton m_confirmBtn;
		public GButton m_cancelBtn;
		public GTextField m_targetLvLabel;

		public const string URL = "ui://8u3gv94nd5g9y";

		public static UI_QuickUpgradeWindow CreateInstance()
		{
			return (UI_QuickUpgradeWindow)UIPackage.CreateObject("UI_Equip","QuickUpgradeWindow");
		}

		public UI_QuickUpgradeWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_boardLoader = (GLoader)this.GetChildAt(8);
			m_iconLoader = (GLoader)this.GetChildAt(9);
			m_starList = (GComponent)this.GetChildAt(10);
			m_lvLabel = (GTextField)this.GetChildAt(11);
			m_nameLabel = (GTextField)this.GetChildAt(12);
			m_reduceBtn = (GButton)this.GetChildAt(14);
			m_addBtn = (GButton)this.GetChildAt(15);
			m_targetPingJie = (GTextField)this.GetChildAt(17);
			m_quickItemList = (GList)this.GetChildAt(18);
			m_goldNum = (GTextField)this.GetChildAt(21);
			m_coinLoader = (GLoader)this.GetChildAt(23);
			m_coinNumLabel = (GTextField)this.GetChildAt(24);
			m_confirmBtn = (GButton)this.GetChildAt(25);
			m_cancelBtn = (GButton)this.GetChildAt(26);
			m_targetLvLabel = (GTextField)this.GetChildAt(28);
		}
	}
}