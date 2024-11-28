/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_objBadge : GComponent
	{
		public GLoader m_imgBadge;

		public const string URL = "ui://oe7ras64f1jg38";

		public static UI_objBadge CreateInstance()
		{
			return (UI_objBadge)UIPackage.CreateObject("UI_Guild","objBadge");
		}

		public UI_objBadge()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBadge = (GLoader)this.GetChildAt(1);
		}
	}
}