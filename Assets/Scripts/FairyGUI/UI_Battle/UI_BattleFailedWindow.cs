/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleFailedWindow : GComponent
	{
		public GGraph m_bg;
		public GImage m_rightBg;
		public GImage m_hpbar;
		public GTextField m_turnTxt;
		public GButton m_jiubaBtn;
		public GButton m_qiangHuaBtn;
		public GButton m_shengPinBtn;

		public const string URL = "ui://028ppdzhjkpz6u";

		public static UI_BattleFailedWindow CreateInstance()
		{
			return (UI_BattleFailedWindow)UIPackage.CreateObject("UI_Battle","BattleFailedWindow");
		}

		public UI_BattleFailedWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GGraph)this.GetChildAt(0);
			m_rightBg = (GImage)this.GetChildAt(2);
			m_hpbar = (GImage)this.GetChildAt(6);
			m_turnTxt = (GTextField)this.GetChildAt(9);
			m_jiubaBtn = (GButton)this.GetChildAt(11);
			m_qiangHuaBtn = (GButton)this.GetChildAt(12);
			m_shengPinBtn = (GButton)this.GetChildAt(13);
		}
	}
}