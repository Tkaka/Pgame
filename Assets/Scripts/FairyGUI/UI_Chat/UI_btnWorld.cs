/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_btnWorld : GButton
	{
		public GTextField m_txtDes;

		public const string URL = "ui://51gazvjd7igt14";

		public static UI_btnWorld CreateInstance()
		{
			return (UI_btnWorld)UIPackage.CreateObject("UI_Chat","btnWorld");
		}

		public UI_btnWorld()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(2);
		}
	}
}