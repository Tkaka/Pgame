/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_BuyCount : GComponent
	{
		public GTextField m_txtNum;
		public GImage m_imgZhuanShi;
		public GImage m_btnBuy;

		public const string URL = "ui://3xs7lfyxgawd1l";

		public static UI_BuyCount CreateInstance()
		{
			return (UI_BuyCount)UIPackage.CreateObject("UI_Arena","BuyCount");
		}

		public UI_BuyCount()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtNum = (GTextField)this.GetChildAt(0);
			m_imgZhuanShi = (GImage)this.GetChildAt(1);
			m_btnBuy = (GImage)this.GetChildAt(2);
		}
	}
}