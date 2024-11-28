/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Friend
{
	public partial class UI_objVip : GComponent
	{
		public GTextField m_txtVipLevel;

		public const string URL = "ui://tvm1q5ekqknin";

		public static UI_objVip CreateInstance()
		{
			return (UI_objVip)UIPackage.CreateObject("UI_Friend","objVip");
		}

		public UI_objVip()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtVipLevel = (GTextField)this.GetChildAt(1);
		}
	}
}