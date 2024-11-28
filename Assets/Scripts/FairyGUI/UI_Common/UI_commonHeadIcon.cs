/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_commonHeadIcon : GComponent
	{
		public GLoader m_headIcon;
		public GGraph m_toucher;

		public const string URL = "ui://42sthz3tkho7xnv";

		public static UI_commonHeadIcon CreateInstance()
		{
			return (UI_commonHeadIcon)UIPackage.CreateObject("UI_Common","commonHeadIcon");
		}

		public UI_commonHeadIcon()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_headIcon = (GLoader)this.GetChildAt(1);
			m_toucher = (GGraph)this.GetChildAt(2);
		}
	}
}