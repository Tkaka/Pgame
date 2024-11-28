/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Achievement
{
	public partial class UI_AM_MainWindow : GComponent
	{
		public Controller m_AchievementType;
		public GImage m_BeiJing;
		public GTextField m_AM_Title;
		public GTextField m_AM_Integral;
		public GTextField m_AM_Precede;
		public GButton m_AM_RuleBtn;
		public GList m_AM_List;
		public UI_AM_MaoXianBtn m_AM_maoxian;
		public UI_AM_HuoDongBtn m_AM_huodong;
		public GButton m_AM_RankingBtn;
		public UI_AM_YangChengBtn m_AM_Yangcheng;
		public GImage m_AM_MX_HongDian;
		public GGroup m_AM_XZ_maoxian;
		public GImage m_AM_YC_HongDian;
		public GGroup m_AM_XZ_yangcheng;
		public GImage m_AM_HD_HongDian;
		public GGroup m_AM_XZ_huodong;
		public GLoader m_TouXiang;
		public GButton m_ColseBtn;

		public const string URL = "ui://xpd8f6j0e2gr0";

		public static UI_AM_MainWindow CreateInstance()
		{
			return (UI_AM_MainWindow)UIPackage.CreateObject("UI_Achievement","AM_MainWindow");
		}

		public UI_AM_MainWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_AchievementType = this.GetControllerAt(0);
			m_BeiJing = (GImage)this.GetChildAt(0);
			m_AM_Title = (GTextField)this.GetChildAt(3);
			m_AM_Integral = (GTextField)this.GetChildAt(5);
			m_AM_Precede = (GTextField)this.GetChildAt(7);
			m_AM_RuleBtn = (GButton)this.GetChildAt(8);
			m_AM_List = (GList)this.GetChildAt(9);
			m_AM_maoxian = (UI_AM_MaoXianBtn)this.GetChildAt(10);
			m_AM_huodong = (UI_AM_HuoDongBtn)this.GetChildAt(11);
			m_AM_RankingBtn = (GButton)this.GetChildAt(12);
			m_AM_Yangcheng = (UI_AM_YangChengBtn)this.GetChildAt(13);
			m_AM_MX_HongDian = (GImage)this.GetChildAt(16);
			m_AM_XZ_maoxian = (GGroup)this.GetChildAt(17);
			m_AM_YC_HongDian = (GImage)this.GetChildAt(20);
			m_AM_XZ_yangcheng = (GGroup)this.GetChildAt(21);
			m_AM_HD_HongDian = (GImage)this.GetChildAt(24);
			m_AM_XZ_huodong = (GGroup)this.GetChildAt(25);
			m_TouXiang = (GLoader)this.GetChildAt(26);
			m_ColseBtn = (GButton)this.GetChildAt(27);
		}
	}
}