/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnBianJi : GComponent
	{
		public GTextField m_txtDes;

		public const string URL = "ui://oe7ras64105rb3h";

		public static UI_btnBianJi CreateInstance()
		{
			return (UI_btnBianJi)UIPackage.CreateObject("UI_Guild","btnBianJi");
		}

		public UI_btnBianJi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(1);
		}
	}
}