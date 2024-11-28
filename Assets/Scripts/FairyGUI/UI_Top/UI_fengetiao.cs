/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Top
{
	public partial class UI_fengetiao : GComponent
	{
		public GTextField m_paiming;
		public GTextField m_juesemign;
		public GTextField m_type1;
		public GTextField m_type2;

		public const string URL = "ui://y4tkaqbbjdpb9";

		public static UI_fengetiao CreateInstance()
		{
			return (UI_fengetiao)UIPackage.CreateObject("UI_Top","fengetiao");
		}

		public UI_fengetiao()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_paiming = (GTextField)this.GetChildAt(1);
			m_juesemign = (GTextField)this.GetChildAt(2);
			m_type1 = (GTextField)this.GetChildAt(3);
			m_type2 = (GTextField)this.GetChildAt(4);
		}
	}
}