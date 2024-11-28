/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Friend
{
	public partial class UI_btnLaHei : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://tvm1q5ekqkni20";

		public static UI_btnLaHei CreateInstance()
		{
			return (UI_btnLaHei)UIPackage.CreateObject("UI_Friend","btnLaHei");
		}

		public UI_btnLaHei()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}