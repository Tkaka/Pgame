/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_ArenaSucessWindow : GComponent
	{
		public GTextField m_objTitle;
		public GTextField m_txtWinName;
		public GTextField m_txtWinRank;
		public GList m_winPetList;
		public GTextField m_txtUpRank;
		public GTextField m_objTitle_2;
		public GTextField m_txtLoserName;
		public GTextField m_txtLoserRank;
		public GList m_loserPetList;
		public GButton m_btnSeeAgain;
		public GButton m_btnExit;

		public const string URL = "ui://028ppdzhcs08g5";

		public static UI_ArenaSucessWindow CreateInstance()
		{
			return (UI_ArenaSucessWindow)UIPackage.CreateObject("UI_Battle","ArenaSucessWindow");
		}

		public UI_ArenaSucessWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_objTitle = (GTextField)this.GetChildAt(2);
			m_txtWinName = (GTextField)this.GetChildAt(4);
			m_txtWinRank = (GTextField)this.GetChildAt(5);
			m_winPetList = (GList)this.GetChildAt(8);
			m_txtUpRank = (GTextField)this.GetChildAt(10);
			m_objTitle_2 = (GTextField)this.GetChildAt(12);
			m_txtLoserName = (GTextField)this.GetChildAt(14);
			m_txtLoserRank = (GTextField)this.GetChildAt(15);
			m_loserPetList = (GList)this.GetChildAt(18);
			m_btnSeeAgain = (GButton)this.GetChildAt(19);
			m_btnExit = (GButton)this.GetChildAt(20);
		}
	}
}