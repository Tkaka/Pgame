/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_buZhenColumn : GComponent
	{
		public GList m_zhenRongList;
		public GButton m_adoptZhenRong;
		public GButton m_keyShangZhen;
		public GGroup m_columnGroup;

		public const string URL = "ui://42sthz3tkrhbk5";

		public static UI_buZhenColumn CreateInstance()
		{
			return (UI_buZhenColumn)UIPackage.CreateObject("UI_Common","buZhenColumn");
		}

		public UI_buZhenColumn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_zhenRongList = (GList)this.GetChildAt(1);
			m_adoptZhenRong = (GButton)this.GetChildAt(2);
			m_keyShangZhen = (GButton)this.GetChildAt(3);
			m_columnGroup = (GGroup)this.GetChildAt(5);
		}
	}
}