/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_BM_ListItem : GComponent
	{
		public GLoader m_pinjie;
		public GLoader m_touxiang;
		public GTextField m_dengji;
		public GComponent m_xingji;
		public GGraph m_xuanzhong;

		public const string URL = "ui://yjvxfmwoidnd1c";

		public static UI_SH_BM_ListItem CreateInstance()
		{
			return (UI_SH_BM_ListItem)UIPackage.CreateObject("UI_StriveHegemong","SH_BM_ListItem");
		}

		public UI_SH_BM_ListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_pinjie = (GLoader)this.GetChildAt(0);
			m_touxiang = (GLoader)this.GetChildAt(1);
			m_dengji = (GTextField)this.GetChildAt(2);
			m_xingji = (GComponent)this.GetChildAt(3);
			m_xuanzhong = (GGraph)this.GetChildAt(4);
		}
	}
}