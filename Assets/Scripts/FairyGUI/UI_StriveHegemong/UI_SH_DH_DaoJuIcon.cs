/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_DH_DaoJuIcon : GComponent
	{
		public GLoader m_beijing;
		public GLoader m_touxiang;
		public GImage m_type;
		public GTextField m_number;

		public const string URL = "ui://yjvxfmwojdrg11";

		public static UI_SH_DH_DaoJuIcon CreateInstance()
		{
			return (UI_SH_DH_DaoJuIcon)UIPackage.CreateObject("UI_StriveHegemong","SH_DH_DaoJuIcon");
		}

		public UI_SH_DH_DaoJuIcon()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_beijing = (GLoader)this.GetChildAt(1);
			m_touxiang = (GLoader)this.GetChildAt(2);
			m_type = (GImage)this.GetChildAt(3);
			m_number = (GTextField)this.GetChildAt(4);
		}
	}
}