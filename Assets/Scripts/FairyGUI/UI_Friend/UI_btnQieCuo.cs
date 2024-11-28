/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Friend
{
	public partial class UI_btnQieCuo : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://tvm1q5ekqkni21";

		public static UI_btnQieCuo CreateInstance()
		{
			return (UI_btnQieCuo)UIPackage.CreateObject("UI_Friend","btnQieCuo");
		}

		public UI_btnQieCuo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}