/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_SaoDangJieSuanWindow : GComponent
	{
		public UI_bestItemInfo m_bestItem;
		public GButton m_QueDing;
		public GButton m_Close;
		public GList m_MainList;
		public GButton m_btnContinue;
		public GButton m_btnFastQueDing;
		public GGroup m_btnFastSaoDang;

		public const string URL = "ui://34cd5b4hp1eau";

		public static UI_SaoDangJieSuanWindow CreateInstance()
		{
			return (UI_SaoDangJieSuanWindow)UIPackage.CreateObject("UI_SaoDangJieSuan","SaoDangJieSuanWindow");
		}

		public UI_SaoDangJieSuanWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bestItem = (UI_bestItemInfo)this.GetChildAt(0);
			m_QueDing = (GButton)this.GetChildAt(4);
			m_Close = (GButton)this.GetChildAt(5);
			m_MainList = (GList)this.GetChildAt(7);
			m_btnContinue = (GButton)this.GetChildAt(8);
			m_btnFastQueDing = (GButton)this.GetChildAt(9);
			m_btnFastSaoDang = (GGroup)this.GetChildAt(10);
		}
	}
}