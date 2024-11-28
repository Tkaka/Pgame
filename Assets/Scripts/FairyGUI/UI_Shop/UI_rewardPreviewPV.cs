/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_rewardPreviewPV : GComponent
	{
		public Controller m_ctrl;
		public GButton m_btnClose;
		public UI_objItem m_btnItem;
		public UI_objEquip m_btnEquip;
		public GList m_mainList;

		public const string URL = "ui://w9mypx6jlxvz1y";

		public static UI_rewardPreviewPV CreateInstance()
		{
			return (UI_rewardPreviewPV)UIPackage.CreateObject("UI_Shop","rewardPreviewPV");
		}

		public UI_rewardPreviewPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ctrl = this.GetControllerAt(0);
			m_btnClose = (GButton)this.GetChildAt(4);
			m_btnItem = (UI_objItem)this.GetChildAt(5);
			m_btnEquip = (UI_objEquip)this.GetChildAt(7);
			m_mainList = (GList)this.GetChildAt(9);
		}
	}
}