/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_headListItem : GComponent
	{
		public GTextField m_tip;
		public GImage m_bg;
		public GList m_headIconList;

		public const string URL = "ui://jdfufi06kho74w";

		public static UI_headListItem CreateInstance()
		{
			return (UI_headListItem)UIPackage.CreateObject("UI_MainCity","headListItem");
		}

		public UI_headListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tip = (GTextField)this.GetChildAt(1);
			m_bg = (GImage)this.GetChildAt(2);
			m_headIconList = (GList)this.GetChildAt(3);
		}
	}
}