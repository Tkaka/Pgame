/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_supperGoodsCell : GComponent
	{
		public GTextField m_txtName;
		public GButton m_imgIcon;
		public GLoader m_imgComsume;
		public GImage m_imgSupper;
		public GTextField m_txtNum;
		public GTextField m_txtTime;
		public GTextField m_txtCount;
		public GComponent m_imgSellOut;
		public GTextField m_txtDiscount;
		public GGroup m_objDiscount;

		public const string URL = "ui://w9mypx6jasm2b";

		public static UI_supperGoodsCell CreateInstance()
		{
			return (UI_supperGoodsCell)UIPackage.CreateObject("UI_Shop","supperGoodsCell");
		}

		public UI_supperGoodsCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtName = (GTextField)this.GetChildAt(1);
			m_imgIcon = (GButton)this.GetChildAt(2);
			m_imgComsume = (GLoader)this.GetChildAt(3);
			m_imgSupper = (GImage)this.GetChildAt(4);
			m_txtNum = (GTextField)this.GetChildAt(6);
			m_txtTime = (GTextField)this.GetChildAt(7);
			m_txtCount = (GTextField)this.GetChildAt(8);
			m_imgSellOut = (GComponent)this.GetChildAt(11);
			m_txtDiscount = (GTextField)this.GetChildAt(13);
			m_objDiscount = (GGroup)this.GetChildAt(14);
		}
	}
}