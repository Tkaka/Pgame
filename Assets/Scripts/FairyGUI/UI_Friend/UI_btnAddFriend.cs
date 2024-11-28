/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Friend
{
	public partial class UI_btnAddFriend : GComponent
	{
		public GTextField m_txtQuit;

		public const string URL = "ui://tvm1q5ekqkni22";

		public static UI_btnAddFriend CreateInstance()
		{
			return (UI_btnAddFriend)UIPackage.CreateObject("UI_Friend","btnAddFriend");
		}

		public UI_btnAddFriend()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtQuit = (GTextField)this.GetChildAt(1);
		}
	}
}