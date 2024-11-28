/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_XunLianJia
{
	public partial class UI_XunLianJiaWindow : GComponent
	{
		public GGraph m_modelPos;
		public GButton m_backBtn;
		public GList m_btnList;
		public UI_mingRenTanBtn m_mingRenTanBtn;
		public UI_tongXianGuanBtn m_tongXiangGuanBtn;
		public UI_ChengJiuBtn m_ChengJiuBtn;
		public UI_shenQiBtn m_shenQiBtn;
		public UI_mingRenTanBtn m_btnTianFu;

		public const string URL = "ui://27xc27aks03s0";

		public static UI_XunLianJiaWindow CreateInstance()
		{
			return (UI_XunLianJiaWindow)UIPackage.CreateObject("UI_XunLianJia","XunLianJiaWindow");
		}

		public UI_XunLianJiaWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_modelPos = (GGraph)this.GetChildAt(2);
			m_backBtn = (GButton)this.GetChildAt(4);
			m_btnList = (GList)this.GetChildAt(5);
			m_mingRenTanBtn = (UI_mingRenTanBtn)this.GetChildAt(6);
			m_tongXiangGuanBtn = (UI_tongXianGuanBtn)this.GetChildAt(7);
			m_ChengJiuBtn = (UI_ChengJiuBtn)this.GetChildAt(8);
			m_shenQiBtn = (UI_shenQiBtn)this.GetChildAt(9);
			m_btnTianFu = (UI_mingRenTanBtn)this.GetChildAt(10);
		}
	}
}