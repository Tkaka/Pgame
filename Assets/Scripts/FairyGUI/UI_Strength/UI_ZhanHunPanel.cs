/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_ZhanHunPanel : GComponent
	{
		public UI_zhanHunItem m_zhanHunItem1;
		public UI_zhanHunItem m_zhanHunItem2;
		public UI_zhanHunItem m_zhanHunItem3;
		public UI_zhanHunItem m_zhanHunItem4;
		public GButton m_strengthBtn;
		public GTextField m_nameLabel;
		public GTextField m_lvLabel;
		public GTextField m_descriptLabel;
		public GLoader m_zhanHunIconLoader;
		public GTextField m_iconLvLabel;
		public GGroup m_fullLevelGroup;
		public GButton m_unlockBtn;

		public const string URL = "ui://qnd9tp35swzn21";

		public static UI_ZhanHunPanel CreateInstance()
		{
			return (UI_ZhanHunPanel)UIPackage.CreateObject("UI_Strength","ZhanHunPanel");
		}

		public UI_ZhanHunPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_zhanHunItem1 = (UI_zhanHunItem)this.GetChildAt(6);
			m_zhanHunItem2 = (UI_zhanHunItem)this.GetChildAt(7);
			m_zhanHunItem3 = (UI_zhanHunItem)this.GetChildAt(8);
			m_zhanHunItem4 = (UI_zhanHunItem)this.GetChildAt(9);
			m_strengthBtn = (GButton)this.GetChildAt(10);
			m_nameLabel = (GTextField)this.GetChildAt(11);
			m_lvLabel = (GTextField)this.GetChildAt(12);
			m_descriptLabel = (GTextField)this.GetChildAt(13);
			m_zhanHunIconLoader = (GLoader)this.GetChildAt(16);
			m_iconLvLabel = (GTextField)this.GetChildAt(18);
			m_fullLevelGroup = (GGroup)this.GetChildAt(21);
			m_unlockBtn = (GButton)this.GetChildAt(22);
		}
	}
}