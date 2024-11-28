/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Loading
{
	public partial class UI_UpdateLoadingWindow : GComponent
	{
		public GImage m_loadingBar;
		public GGroup m_preBar;
		public GTextField m_txtTip;

		public const string URL = "ui://ewurfl6flj0x7";

		public static UI_UpdateLoadingWindow CreateInstance()
		{
			return (UI_UpdateLoadingWindow)UIPackage.CreateObject("UI_Loading","UpdateLoadingWindow");
		}

		public UI_UpdateLoadingWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_loadingBar = (GImage)this.GetChildAt(1);
			m_preBar = (GGroup)this.GetChildAt(2);
			m_txtTip = (GTextField)this.GetChildAt(3);
		}
	}
}