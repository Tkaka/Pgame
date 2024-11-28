/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_HuoDeChongWuWindow : GComponent
	{
		public GLoader m_ZhanShiTuPian;
		public GGraph m_MoXing;
		public GTextField m_YiHuoDe;
		public GGraph m_CloseBtn;

		public const string URL = "ui://42sthz3teo85kb";

		public static UI_HuoDeChongWuWindow CreateInstance()
		{
			return (UI_HuoDeChongWuWindow)UIPackage.CreateObject("UI_Common","HuoDeChongWuWindow");
		}

		public UI_HuoDeChongWuWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ZhanShiTuPian = (GLoader)this.GetChildAt(1);
			m_MoXing = (GGraph)this.GetChildAt(2);
			m_YiHuoDe = (GTextField)this.GetChildAt(3);
			m_CloseBtn = (GGraph)this.GetChildAt(4);
		}
	}
}