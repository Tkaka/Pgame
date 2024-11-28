/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_AYiIconCell : GComponent
	{
		public GLoader m_imgIcon;

		public const string URL = "ui://vexa0xrycpnr8";

		public static UI_AYiIconCell CreateInstance()
		{
			return (UI_AYiIconCell)UIPackage.CreateObject("UI_AoYi","AYiIconCell");
		}

		public UI_AYiIconCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgIcon = (GLoader)this.GetChildAt(0);
		}
	}
}