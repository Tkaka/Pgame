/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_WorldMap
{
	public partial class UI_WorldMapWindow : GComponent
	{
		public GList m_scrollList;
		public GButton m_backBtn;
		public GGroup m_topLeft;

		public const string URL = "ui://k1lxoe22ls568";

		public static UI_WorldMapWindow CreateInstance()
		{
			return (UI_WorldMapWindow)UIPackage.CreateObject("UI_WorldMap","WorldMapWindow");
		}

		public UI_WorldMapWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_scrollList = (GList)this.GetChildAt(0);
			m_backBtn = (GButton)this.GetChildAt(11);
			m_topLeft = (GGroup)this.GetChildAt(17);
		}
	}
}