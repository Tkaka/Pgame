/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_ContinuousChallengeWindow : GComponent
	{
		public GButton m_btnClose;
		public GTextField m_txtNum;
		public GGroup m_price;
		public GComponent m_btnChallenge5;
		public GComponent m_btnOk;
		public GLoader m_imgMyIcon;
		public GLoader m_imgEnemyIcon;
		public GTextField m_txtMyName;
		public GTextField m_txtMyFightPower;
		public GTextField m_txtEnemyName;
		public GTextField m_txtEnemyFightPower;
		public GList m_mainList;

		public const string URL = "ui://3xs7lfyxehrw26";

		public static UI_ContinuousChallengeWindow CreateInstance()
		{
			return (UI_ContinuousChallengeWindow)UIPackage.CreateObject("UI_Arena","ContinuousChallengeWindow");
		}

		public UI_ContinuousChallengeWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(1);
			m_txtNum = (GTextField)this.GetChildAt(3);
			m_price = (GGroup)this.GetChildAt(4);
			m_btnChallenge5 = (GComponent)this.GetChildAt(5);
			m_btnOk = (GComponent)this.GetChildAt(6);
			m_imgMyIcon = (GLoader)this.GetChildAt(10);
			m_imgEnemyIcon = (GLoader)this.GetChildAt(11);
			m_txtMyName = (GTextField)this.GetChildAt(12);
			m_txtMyFightPower = (GTextField)this.GetChildAt(13);
			m_txtEnemyName = (GTextField)this.GetChildAt(15);
			m_txtEnemyFightPower = (GTextField)this.GetChildAt(16);
			m_mainList = (GList)this.GetChildAt(18);
		}
	}
}