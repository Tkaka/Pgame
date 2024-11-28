/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_ScoreRewardWindow : GComponent
	{
		public GButton m_btnClose;
		public GComponent m_btnOneKeyGet;
		public GList m_mainList;
		public GTextField m_txtScore;

		public const string URL = "ui://3xs7lfyxehrw20";

		public static UI_ScoreRewardWindow CreateInstance()
		{
			return (UI_ScoreRewardWindow)UIPackage.CreateObject("UI_Arena","ScoreRewardWindow");
		}

		public UI_ScoreRewardWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_btnOneKeyGet = (GComponent)this.GetChildAt(2);
			m_mainList = (GList)this.GetChildAt(3);
			m_txtScore = (GTextField)this.GetChildAt(5);
		}
	}
}