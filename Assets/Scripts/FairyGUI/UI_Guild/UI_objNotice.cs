/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_objNotice : GComponent
	{
		public GTextField m_txtDes;
		public UI_btnBianJi m_btnBianji;

		public const string URL = "ui://oe7ras64105rb3g";

		public static UI_objNotice CreateInstance()
		{
			return (UI_objNotice)UIPackage.CreateObject("UI_Guild","objNotice");
		}

		public UI_objNotice()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(1);
			m_btnBianji = (UI_btnBianJi)this.GetChildAt(2);
		}
	}
}