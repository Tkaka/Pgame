/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_HF_ShuXingXianShiWindow : GComponent
	{
		public GGraph m_BeiJing;
		public GTextField m_name;
		public GList m_propertyList;
		public GLoader m_colorIcon;
		public GTextField m_dangqiandengji;
		public UI_HF_shuxing m_nextProperty;
		public GButton m_CloseBtn;

		public const string URL = "ui://yo5kunkilddym";

		public static UI_HF_ShuXingXianShiWindow CreateInstance()
		{
			return (UI_HF_ShuXingXianShiWindow)UIPackage.CreateObject("UI_HallFame","HF_ShuXingXianShiWindow");
		}

		public UI_HF_ShuXingXianShiWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GGraph)this.GetChildAt(0);
			m_name = (GTextField)this.GetChildAt(3);
			m_propertyList = (GList)this.GetChildAt(4);
			m_colorIcon = (GLoader)this.GetChildAt(5);
			m_dangqiandengji = (GTextField)this.GetChildAt(7);
			m_nextProperty = (UI_HF_shuxing)this.GetChildAt(10);
			m_CloseBtn = (GButton)this.GetChildAt(11);
		}
	}
}