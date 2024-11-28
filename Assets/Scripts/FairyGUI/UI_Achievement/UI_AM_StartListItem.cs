/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Achievement
{
	public partial class UI_AM_StartListItem : GComponent
	{
		public GImage m_NoConclude;
		public GImage m_conclude;

		public const string URL = "ui://xpd8f6j0enteg";

		public static UI_AM_StartListItem CreateInstance()
		{
			return (UI_AM_StartListItem)UIPackage.CreateObject("UI_Achievement","AM_StartListItem");
		}

		public UI_AM_StartListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_NoConclude = (GImage)this.GetChildAt(0);
			m_conclude = (GImage)this.GetChildAt(1);
		}
	}
}