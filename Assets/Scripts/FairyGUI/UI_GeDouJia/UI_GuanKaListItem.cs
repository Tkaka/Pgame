/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_GuanKaListItem : GComponent
	{
		public GComponent m_GK_Type;
		public UI_GuanKaListBtn m_GK_QianWang;
		public GTextField m_GK_Name;
		public GList m_DiaoLuoList;
		public GTextField m_ZhangJie;
		public GGroup m_GuanKaLaiYuan;

		public const string URL = "ui://4asqm7awfps849";

		public static UI_GuanKaListItem CreateInstance()
		{
			return (UI_GuanKaListItem)UIPackage.CreateObject("UI_GeDouJia","GuanKaListItem");
		}

		public UI_GuanKaListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_GK_Type = (GComponent)this.GetChildAt(1);
			m_GK_QianWang = (UI_GuanKaListBtn)this.GetChildAt(2);
			m_GK_Name = (GTextField)this.GetChildAt(3);
			m_DiaoLuoList = (GList)this.GetChildAt(4);
			m_ZhangJie = (GTextField)this.GetChildAt(5);
			m_GuanKaLaiYuan = (GGroup)this.GetChildAt(6);
		}
	}
}