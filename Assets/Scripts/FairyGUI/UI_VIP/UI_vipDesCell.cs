/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_VIP
{
	public partial class UI_vipDesCell : GComponent
	{
		public GTextField m_txtDes;

		public const string URL = "ui://7pvd5vi4qaa2d";

		public static UI_vipDesCell CreateInstance()
		{
			return (UI_vipDesCell)UIPackage.CreateObject("UI_VIP","vipDesCell");
		}

		public UI_vipDesCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(1);
		}
	}
}