/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BuZhen
{
	public partial class UI_YiShangZhenBtn : GComponent
	{
		public Controller m_button;
		public GLoader m_PingJieKuang;
		public GLoader m_TouXiangKuang;
		public GComponent m_PinJieDian;
		public GComponent m_starList;
		public GGroup m_ShangZhenTuBiao;

		public const string URL = "ui://z0csav43mwyx1s";

		public static UI_YiShangZhenBtn CreateInstance()
		{
			return (UI_YiShangZhenBtn)UIPackage.CreateObject("UI_BuZhen","YiShangZhenBtn");
		}

		public UI_YiShangZhenBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_button = this.GetControllerAt(0);
			m_PingJieKuang = (GLoader)this.GetChildAt(0);
			m_TouXiangKuang = (GLoader)this.GetChildAt(1);
			m_PinJieDian = (GComponent)this.GetChildAt(2);
			m_starList = (GComponent)this.GetChildAt(3);
			m_ShangZhenTuBiao = (GGroup)this.GetChildAt(6);
		}
	}
}