/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_DrawResultWnd : GComponent
	{
		public GList m_rewardList;
		public GTextField m_txtTitle;
		public UI_btnOpenAgain1 m_btnOpen1;
		public UI_btnOpenAgain10 m_btnOpen10;
		public UI_btnOk m_btnOk;
		public GLoader m_imgComsume;
		public GTextField m_txtNum;
		public GGroup m_coinGroup;
		public GGroup m_objGroup;

		public const string URL = "ui://vexa0xrynxtq1r";

		public static UI_DrawResultWnd CreateInstance()
		{
			return (UI_DrawResultWnd)UIPackage.CreateObject("UI_AoYi","DrawResultWnd");
		}

		public UI_DrawResultWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rewardList = (GList)this.GetChildAt(1);
			m_txtTitle = (GTextField)this.GetChildAt(2);
			m_btnOpen1 = (UI_btnOpenAgain1)this.GetChildAt(3);
			m_btnOpen10 = (UI_btnOpenAgain10)this.GetChildAt(4);
			m_btnOk = (UI_btnOk)this.GetChildAt(5);
			m_imgComsume = (GLoader)this.GetChildAt(7);
			m_txtNum = (GTextField)this.GetChildAt(8);
			m_coinGroup = (GGroup)this.GetChildAt(9);
			m_objGroup = (GGroup)this.GetChildAt(10);
		}
	}
}