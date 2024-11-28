/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_HG_Figure : GComponent
	{
		public GLoader m_pinjie;
		public GLoader m_touxiang;
		public GLoader m_zhankuang;
		public GComponent m_xingji;
		public GTextField m_dengji;

		public const string URL = "ui://yjvxfmwon7xzl";

		public static UI_SH_HG_Figure CreateInstance()
		{
			return (UI_SH_HG_Figure)UIPackage.CreateObject("UI_StriveHegemong","SH_HG_Figure");
		}

		public UI_SH_HG_Figure()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_pinjie = (GLoader)this.GetChildAt(1);
			m_touxiang = (GLoader)this.GetChildAt(2);
			m_zhankuang = (GLoader)this.GetChildAt(3);
			m_xingji = (GComponent)this.GetChildAt(4);
			m_dengji = (GTextField)this.GetChildAt(5);
		}
	}
}