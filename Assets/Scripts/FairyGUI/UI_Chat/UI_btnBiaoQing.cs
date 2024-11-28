/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_btnBiaoQing : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://51gazvjd7igtw";

		public static UI_btnBiaoQing CreateInstance()
		{
			return (UI_btnBiaoQing)UIPackage.CreateObject("UI_Chat","btnBiaoQing");
		}

		public UI_btnBiaoQing()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}