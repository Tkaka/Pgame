/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_DaoJiShi : GComponent
	{
		public GTextField m_weikaisai_name;
		public GTextField m_daojishi;
		public GGroup m_WeiKaiSai;

		public const string URL = "ui://yjvxfmwoe8xk1r";

		public static UI_SH_DaoJiShi CreateInstance()
		{
			return (UI_SH_DaoJiShi)UIPackage.CreateObject("UI_StriveHegemong","SH_DaoJiShi");
		}

		public UI_SH_DaoJiShi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_weikaisai_name = (GTextField)this.GetChildAt(1);
			m_daojishi = (GTextField)this.GetChildAt(2);
			m_WeiKaiSai = (GGroup)this.GetChildAt(3);
		}
	}
}