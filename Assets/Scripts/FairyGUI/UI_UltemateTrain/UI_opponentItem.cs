/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_opponentItem : GComponent
	{
		public GTextField m_nameLabel;
		public GTextField m_fightPowerLabel;
		public GLoader m_iconLoader;
		public GTextField m_integralLabel;
		public GTextField m_starTimesLabel;
		public GLoader m_difficultyLoader;
		public GList m_petList;
		public GGroup m_petListGroup;
		public GGraph m_toucher;
		public GButton m_challengeBtn;
		public GImage m_lockIcon;

		public const string URL = "ui://1wdkrtiuw0hu19";

		public static UI_opponentItem CreateInstance()
		{
			return (UI_opponentItem)UIPackage.CreateObject("UI_UltemateTrain","opponentItem");
		}

		public UI_opponentItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_nameLabel = (GTextField)this.GetChildAt(1);
			m_fightPowerLabel = (GTextField)this.GetChildAt(2);
			m_iconLoader = (GLoader)this.GetChildAt(3);
			m_integralLabel = (GTextField)this.GetChildAt(7);
			m_starTimesLabel = (GTextField)this.GetChildAt(10);
			m_difficultyLoader = (GLoader)this.GetChildAt(11);
			m_petList = (GList)this.GetChildAt(13);
			m_petListGroup = (GGroup)this.GetChildAt(14);
			m_toucher = (GGraph)this.GetChildAt(15);
			m_challengeBtn = (GButton)this.GetChildAt(16);
			m_lockIcon = (GImage)this.GetChildAt(17);
		}
	}
}