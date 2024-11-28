/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TongXiangGuan
{
	public partial class UI_BuyTongXiangWindow : GComponent
	{
		public Controller m_materialCtrl;
		public GList m_btnList;
		public GTextField m_tongXiangNameLabel;
		public GList m_goodsList;
		public GTextField m_curUseTipLabel;
		public GButton m_backBtn;

		public const string URL = "ui://ansp6fm5lni73";

		public static UI_BuyTongXiangWindow CreateInstance()
		{
			return (UI_BuyTongXiangWindow)UIPackage.CreateObject("UI_TongXiangGuan","BuyTongXiangWindow");
		}

		public UI_BuyTongXiangWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_materialCtrl = this.GetControllerAt(0);
			m_btnList = (GList)this.GetChildAt(2);
			m_tongXiangNameLabel = (GTextField)this.GetChildAt(4);
			m_goodsList = (GList)this.GetChildAt(5);
			m_curUseTipLabel = (GTextField)this.GetChildAt(7);
			m_backBtn = (GButton)this.GetChildAt(8);
		}
	}
}