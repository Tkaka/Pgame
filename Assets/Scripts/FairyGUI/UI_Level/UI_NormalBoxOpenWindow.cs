/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_NormalBoxOpenWindow : GComponent
	{
		public GGraph m_mask;
		public UI_normalBoxOpenPV m_popupView;

		public const string URL = "ui://z04ymz0e97kms";

		public static UI_NormalBoxOpenWindow CreateInstance()
		{
			return (UI_NormalBoxOpenWindow)UIPackage.CreateObject("UI_Level","NormalBoxOpenWindow");
		}

		public UI_NormalBoxOpenWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_normalBoxOpenPV)this.GetChildAt(1);
		}
	}
}