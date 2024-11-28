/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_ColorUpWindow : GComponent
	{
		public GGraph m_;
		public GLoader m_HF_haoganduIcon;
		public GList m_ItemList;
		public GTextField m_miaoshu;
		public GButton m_HF_CloseBtn;
		public GButton m_HF_queding;

		public const string URL = "ui://yo5kunkic4uwi";

		public static UI_ColorUpWindow CreateInstance()
		{
			return (UI_ColorUpWindow)UIPackage.CreateObject("UI_HallFame","ColorUpWindow");
		}

		public UI_ColorUpWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ = (GGraph)this.GetChildAt(0);
			m_HF_haoganduIcon = (GLoader)this.GetChildAt(3);
			m_ItemList = (GList)this.GetChildAt(4);
			m_miaoshu = (GTextField)this.GetChildAt(5);
			m_HF_CloseBtn = (GButton)this.GetChildAt(6);
			m_HF_queding = (GButton)this.GetChildAt(7);
		}
	}
}