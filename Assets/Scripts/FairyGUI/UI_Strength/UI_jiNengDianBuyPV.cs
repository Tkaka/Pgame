/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_jiNengDianBuyPV : GComponent
	{
		public GButton m_Close;
		public GTextField m_TiShi1;
		public GButton m_GouMai;
		public GButton m_ChaKan;
		public GRichTextField m_TiShi2;
		public GTextField m_number;
		public GButton m_TiSheng;
		public GImage m_icon;

		public const string URL = "ui://qnd9tp35lxvz4m";

		public static UI_jiNengDianBuyPV CreateInstance()
		{
			return (UI_jiNengDianBuyPV)UIPackage.CreateObject("UI_Strength","jiNengDianBuyPV");
		}

		public UI_jiNengDianBuyPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Close = (GButton)this.GetChildAt(5);
			m_TiShi1 = (GTextField)this.GetChildAt(6);
			m_GouMai = (GButton)this.GetChildAt(7);
			m_ChaKan = (GButton)this.GetChildAt(8);
			m_TiShi2 = (GRichTextField)this.GetChildAt(9);
			m_number = (GTextField)this.GetChildAt(10);
			m_TiSheng = (GButton)this.GetChildAt(11);
			m_icon = (GImage)this.GetChildAt(12);
		}
	}
}