/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_OneKeyPlaceWnd : GComponent
	{
		public GList m_mainList;
		public GButton m_btnClose;

		public const string URL = "ui://vexa0xryqcm6s";

		public static UI_OneKeyPlaceWnd CreateInstance()
		{
			return (UI_OneKeyPlaceWnd)UIPackage.CreateObject("UI_AoYi","OneKeyPlaceWnd");
		}

		public UI_OneKeyPlaceWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mainList = (GList)this.GetChildAt(5);
			m_btnClose = (GButton)this.GetChildAt(6);
		}
	}
}