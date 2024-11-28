/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_JiangPingListItem : GComponent
	{
		public GLoader m_BeiJing;
		public GTextField m_Name;
		public GLoader m_TouXiang;
		public GTextField m_number;
		public GLoader m_SuiPian;
		public GComponent m_StartList;
		public GGroup m_all;

		public const string URL = "ui://zy7t2yegw9w21d";

		public static UI_JiangPingListItem CreateInstance()
		{
			return (UI_JiangPingListItem)UIPackage.CreateObject("UI_DrawCard","JiangPingListItem");
		}

		public UI_JiangPingListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GLoader)this.GetChildAt(0);
			m_Name = (GTextField)this.GetChildAt(1);
			m_TouXiang = (GLoader)this.GetChildAt(2);
			m_number = (GTextField)this.GetChildAt(3);
			m_SuiPian = (GLoader)this.GetChildAt(4);
			m_StartList = (GComponent)this.GetChildAt(5);
			m_all = (GGroup)this.GetChildAt(6);
		}
	}
}