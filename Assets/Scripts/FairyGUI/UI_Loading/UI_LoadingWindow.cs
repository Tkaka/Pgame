/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Loading
{
	public partial class UI_LoadingWindow : GComponent
	{
		public GImage m_loadingBar;

		public const string URL = "ui://ewurfl6fq5kb1";

		public static UI_LoadingWindow CreateInstance()
		{
			return (UI_LoadingWindow)UIPackage.CreateObject("UI_Loading","LoadingWindow");
		}

		public UI_LoadingWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_loadingBar = (GImage)this.GetChildAt(1);
		}
	}
}