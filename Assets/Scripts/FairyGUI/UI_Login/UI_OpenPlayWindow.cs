/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Login
{
	public partial class UI_OpenPlayWindow : GComponent
	{
		public GGraph m_TiaoGuoBtn;

		public const string URL = "ui://hg19ijpajww61b";

		public static UI_OpenPlayWindow CreateInstance()
		{
			return (UI_OpenPlayWindow)UIPackage.CreateObject("UI_Login","OpenPlayWindow");
		}

		public UI_OpenPlayWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_TiaoGuoBtn = (GGraph)this.GetChildAt(0);
		}
	}
}