/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_headIcon : GComponent
	{
		public GLoader m_imgBg;
		public GLoader m_imgIcon;

		public const string URL = "ui://oe7ras64qbwu1u";

		public static UI_headIcon CreateInstance()
		{
			return (UI_headIcon)UIPackage.CreateObject("UI_Guild","headIcon");
		}

		public UI_headIcon()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GLoader)this.GetChildAt(0);
			m_imgIcon = (GLoader)this.GetChildAt(1);
		}
	}
}