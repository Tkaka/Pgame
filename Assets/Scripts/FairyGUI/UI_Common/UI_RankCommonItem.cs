/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_rankCommonItem : GComponent
	{
		public GTextField m_ziji;
		public GLoader m_paiming_Icon;
		public GTextField m_paiming_number;
		public GTextField m_name;
		public GTextField m_chenghao;
		public GTextField m_left;
		public GTextField m_right;
		public UI_petItem m_Rankpet;
		public GGraph m_ChongWuXiangQingBtn;
		public GGroup m_Pet;
		public GGroup m_YiShangBang;
		public GTextField m_WiShangBang;
		public GGraph m_toucher;

		public const string URL = "ui://42sthz3thf5jxr7";

		public static UI_rankCommonItem CreateInstance()
		{
			return (UI_rankCommonItem)UIPackage.CreateObject("UI_Common","rankCommonItem");
		}

		public UI_rankCommonItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ziji = (GTextField)this.GetChildAt(1);
			m_paiming_Icon = (GLoader)this.GetChildAt(2);
			m_paiming_number = (GTextField)this.GetChildAt(3);
			m_name = (GTextField)this.GetChildAt(4);
			m_chenghao = (GTextField)this.GetChildAt(5);
			m_left = (GTextField)this.GetChildAt(6);
			m_right = (GTextField)this.GetChildAt(7);
			m_Rankpet = (UI_petItem)this.GetChildAt(8);
			m_ChongWuXiangQingBtn = (GGraph)this.GetChildAt(9);
			m_Pet = (GGroup)this.GetChildAt(10);
			m_YiShangBang = (GGroup)this.GetChildAt(11);
			m_WiShangBang = (GTextField)this.GetChildAt(12);
			m_toucher = (GGraph)this.GetChildAt(13);
		}
	}
}