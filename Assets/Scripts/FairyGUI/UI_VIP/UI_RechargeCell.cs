/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_VIP
{
	public partial class UI_RechargeCell : GButton
	{
		public GGroup m_objRecommond;
		public GTextField m_txtPrice;
		public GLoader m_imgIcon;
		public GTextField m_txtExtraDiamond;
		public GTextField m_txtDiamond;

		public const string URL = "ui://7pvd5vi49wdbk";

		public static UI_RechargeCell CreateInstance()
		{
			return (UI_RechargeCell)UIPackage.CreateObject("UI_VIP","RechargeCell");
		}

		public UI_RechargeCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_objRecommond = (GGroup)this.GetChildAt(3);
			m_txtPrice = (GTextField)this.GetChildAt(4);
			m_imgIcon = (GLoader)this.GetChildAt(5);
			m_txtExtraDiamond = (GTextField)this.GetChildAt(6);
			m_txtDiamond = (GTextField)this.GetChildAt(8);
		}
	}
}