/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_HG_Group : GComponent
	{
		public UI_SH_HG_baqiangJianKuang_Item m_one;
		public UI_SH_HG_baqiangJianKuang_Item m_two;

		public const string URL = "ui://yjvxfmwon7xzs";

		public static UI_SH_HG_Group CreateInstance()
		{
			return (UI_SH_HG_Group)UIPackage.CreateObject("UI_StriveHegemong","SH_HG_Group");
		}

		public UI_SH_HG_Group()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_one = (UI_SH_HG_baqiangJianKuang_Item)this.GetChildAt(0);
			m_two = (UI_SH_HG_baqiangJianKuang_Item)this.GetChildAt(1);
		}
	}
}