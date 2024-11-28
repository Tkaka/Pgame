/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_FastSaoDangBtn : GComponent
	{
		public GButton m_btnFastQueDing;
		public GButton m_btnContinue;

		public const string URL = "ui://34cd5b4hiwuz14";

		public static UI_FastSaoDangBtn CreateInstance()
		{
			return (UI_FastSaoDangBtn)UIPackage.CreateObject("UI_SaoDangJieSuan","FastSaoDangBtn");
		}

		public UI_FastSaoDangBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnFastQueDing = (GButton)this.GetChildAt(0);
			m_btnContinue = (GButton)this.GetChildAt(1);
		}
	}
}