/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_SupperSaoDangWindow : GComponent
	{
		public GGraph m_mask;
		public UI_supperSaoDangPV m_popupView;

		public const string URL = "ui://34cd5b4hrpuw1l";

		public static UI_SupperSaoDangWindow CreateInstance()
		{
			return (UI_SupperSaoDangWindow)UIPackage.CreateObject("UI_SaoDangJieSuan","SupperSaoDangWindow");
		}

		public UI_SupperSaoDangWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_supperSaoDangPV)this.GetChildAt(1);
		}
	}
}