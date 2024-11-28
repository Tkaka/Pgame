/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_goodsCell : GComponent
	{
		public GTextField m_txtName;
		public GButton m_imgIcon;
		public GLoader m_imgComsume;
		public GTextField m_txtDiscount;
		public GGroup m_objDiscount;
		public GTextField m_txtNum;
		public GComponent m_imgSellOut;

		public const string URL = "ui://w9mypx6jyzqv7";

		public static UI_goodsCell CreateInstance()
		{
			return (UI_goodsCell)UIPackage.CreateObject("UI_Shop","goodsCell");
		}

		public UI_goodsCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtName = (GTextField)this.GetChildAt(2);
			m_imgIcon = (GButton)this.GetChildAt(3);
			m_imgComsume = (GLoader)this.GetChildAt(4);
			m_txtDiscount = (GTextField)this.GetChildAt(6);
			m_objDiscount = (GGroup)this.GetChildAt(7);
			m_txtNum = (GTextField)this.GetChildAt(8);
			m_imgSellOut = (GComponent)this.GetChildAt(9);
		}
	}
}