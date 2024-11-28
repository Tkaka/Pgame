/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BossBriefWindow : GComponent
	{
		public GGraph m_bg;
		public GGraph m_modelHolder;
		public GTextField m_dingweiTxt;
		public GTextField m_skillName;
		public GTextField m_skillDesc;
		public GTextField m_bossName;
		public GTextField m_zizhiTxt;
		public GTextField m_desc;

		public const string URL = "ui://028ppdzhjh5a6m";

		public static UI_BossBriefWindow CreateInstance()
		{
			return (UI_BossBriefWindow)UIPackage.CreateObject("UI_Battle","BossBriefWindow");
		}

		public UI_BossBriefWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GGraph)this.GetChildAt(0);
			m_modelHolder = (GGraph)this.GetChildAt(5);
			m_dingweiTxt = (GTextField)this.GetChildAt(7);
			m_skillName = (GTextField)this.GetChildAt(9);
			m_skillDesc = (GTextField)this.GetChildAt(10);
			m_bossName = (GTextField)this.GetChildAt(11);
			m_zizhiTxt = (GTextField)this.GetChildAt(13);
			m_desc = (GTextField)this.GetChildAt(15);
		}
	}
}