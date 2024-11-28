/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_DebugItem : GButton
	{
		public GButton m_expandButton;
		public GGraph m_indent;

		public const string URL = "ui://028ppdzhpyqvbe";

		public static UI_DebugItem CreateInstance()
		{
			return (UI_DebugItem)UIPackage.CreateObject("UI_Battle","DebugItem");
		}

		public UI_DebugItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_expandButton = (GButton)this.GetChildAt(0);
			m_indent = (GGraph)this.GetChildAt(2);
		}
	}
}