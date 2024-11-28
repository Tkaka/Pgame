/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_btnSystem : GButton
	{
		public GTextField m_txtDes;

		public const string URL = "ui://51gazvjd7igt11";

		public static UI_btnSystem CreateInstance()
		{
			return (UI_btnSystem)UIPackage.CreateObject("UI_Chat","btnSystem");
		}

		public UI_btnSystem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(2);
		}
	}
}