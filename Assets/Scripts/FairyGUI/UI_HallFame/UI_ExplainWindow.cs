/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_ExplainWindow : GComponent
	{
		public GList m_GuiZeList;
		public GButton m_CloseBtn;

		public const string URL = "ui://yo5kunkic4uwj";

		public static UI_ExplainWindow CreateInstance()
		{
			return (UI_ExplainWindow)UIPackage.CreateObject("UI_HallFame","ExplainWindow");
		}

		public UI_ExplainWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_GuiZeList = (GList)this.GetChildAt(4);
			m_CloseBtn = (GButton)this.GetChildAt(5);
		}
	}
}