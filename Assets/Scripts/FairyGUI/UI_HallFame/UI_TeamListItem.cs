/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_TeamListItem : GComponent
	{
		public GLoader m_BeiJing;
		public GLoader m_TouXiang;
		public GImage m_HongDian;
		public GTextField m_number;

		public const string URL = "ui://yo5kunkiux5q2";

		public static UI_TeamListItem CreateInstance()
		{
			return (UI_TeamListItem)UIPackage.CreateObject("UI_HallFame","TeamListItem");
		}

		public UI_TeamListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GLoader)this.GetChildAt(0);
			m_TouXiang = (GLoader)this.GetChildAt(1);
			m_HongDian = (GImage)this.GetChildAt(2);
			m_number = (GTextField)this.GetChildAt(3);
		}
	}
}