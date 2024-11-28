/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_JinHuaPanel : GComponent
	{
		public GButton m_fragBtn;
		public GButton m_addBtn;
		public GProgressBar m_progressBar;
		public GTextField m_progressTxt;
		public GButton m_jinhuaBtn;
		public GTextField m_goldTxt;
		public GTextField m_fightTxt;
		public GTextField m_atkTxt;
		public GTextField m_defTxt;
		public GTextField m_hpTxt;
		public GLoader m_star1;
		public GLoader m_star2;
		public GLoader m_star3;
		public GLoader m_star4;
		public GLoader m_star5;
		public GLoader m_star6;
		public GLoader m_star7;

		public const string URL = "ui://qnd9tp35w5481m";

		public static UI_JinHuaPanel CreateInstance()
		{
			return (UI_JinHuaPanel)UIPackage.CreateObject("UI_Strength","JinHuaPanel");
		}

		public UI_JinHuaPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_fragBtn = (GButton)this.GetChildAt(2);
			m_addBtn = (GButton)this.GetChildAt(4);
			m_progressBar = (GProgressBar)this.GetChildAt(5);
			m_progressTxt = (GTextField)this.GetChildAt(6);
			m_jinhuaBtn = (GButton)this.GetChildAt(7);
			m_goldTxt = (GTextField)this.GetChildAt(10);
			m_fightTxt = (GTextField)this.GetChildAt(19);
			m_atkTxt = (GTextField)this.GetChildAt(20);
			m_defTxt = (GTextField)this.GetChildAt(21);
			m_hpTxt = (GTextField)this.GetChildAt(22);
			m_star1 = (GLoader)this.GetChildAt(25);
			m_star2 = (GLoader)this.GetChildAt(26);
			m_star3 = (GLoader)this.GetChildAt(27);
			m_star4 = (GLoader)this.GetChildAt(28);
			m_star5 = (GLoader)this.GetChildAt(29);
			m_star6 = (GLoader)this.GetChildAt(30);
			m_star7 = (GLoader)this.GetChildAt(31);
		}
	}
}