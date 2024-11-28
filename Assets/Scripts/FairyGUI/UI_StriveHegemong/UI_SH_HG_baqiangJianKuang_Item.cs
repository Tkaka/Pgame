/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_HG_baqiangJianKuang_Item : GComponent
	{
		public GLoader m_beijing;
		public GLoader m_touxiang;
		public GLoader m_jieguo;
		public GTextField m_ZhanLi;
		public GTextField m_name;

		public const string URL = "ui://yjvxfmwon7xzr";

		public static UI_SH_HG_baqiangJianKuang_Item CreateInstance()
		{
			return (UI_SH_HG_baqiangJianKuang_Item)UIPackage.CreateObject("UI_StriveHegemong","SH_HG_baqiangJianKuang_Item");
		}

		public UI_SH_HG_baqiangJianKuang_Item()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_beijing = (GLoader)this.GetChildAt(1);
			m_touxiang = (GLoader)this.GetChildAt(2);
			m_jieguo = (GLoader)this.GetChildAt(3);
			m_ZhanLi = (GTextField)this.GetChildAt(4);
			m_name = (GTextField)this.GetChildAt(5);
		}
	}
}