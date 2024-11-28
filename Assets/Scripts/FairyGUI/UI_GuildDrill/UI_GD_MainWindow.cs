/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_MainWindow : GComponent
	{
		public Controller m_type;
		public GButton m_CloseBtn;
		public GButton m_shetuanyindiBtn;
		public GButton m_wodeyindiBtn;
		public UI_GD_WodeXunLian m_XunLianWeiList;
		public GList m_jiasurizhiList;
		public UI_GD_SuiJiYiCiBtn m_suijiyici;
		public UI_GD_SuiJiQuanBuBtn m_suijiquanbu;
		public GList m_jiasuList;
		public GTextField m_jiasucishu;
		public GTextField m_beidongjiashucishu;
		public GGroup m_shetuanyindi;
		public GTextField m_gundong;
		public GGraph m_lastBtn;
		public GGroup m_last;
		public GGraph m_nextBtn;
		public GGroup m_next;
		public Transition m_WenZiTiShi;
		public Transition m_AnNiu;

		public const string URL = "ui://wwlsouxzk46r0";

		public static UI_GD_MainWindow CreateInstance()
		{
			return (UI_GD_MainWindow)UIPackage.CreateObject("UI_GuildDrill","GD_MainWindow");
		}

		public UI_GD_MainWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_type = this.GetControllerAt(0);
			m_CloseBtn = (GButton)this.GetChildAt(1);
			m_shetuanyindiBtn = (GButton)this.GetChildAt(2);
			m_wodeyindiBtn = (GButton)this.GetChildAt(3);
			m_XunLianWeiList = (UI_GD_WodeXunLian)this.GetChildAt(5);
			m_jiasurizhiList = (GList)this.GetChildAt(9);
			m_suijiyici = (UI_GD_SuiJiYiCiBtn)this.GetChildAt(10);
			m_suijiquanbu = (UI_GD_SuiJiQuanBuBtn)this.GetChildAt(11);
			m_jiasuList = (GList)this.GetChildAt(12);
			m_jiasucishu = (GTextField)this.GetChildAt(13);
			m_beidongjiashucishu = (GTextField)this.GetChildAt(14);
			m_shetuanyindi = (GGroup)this.GetChildAt(15);
			m_gundong = (GTextField)this.GetChildAt(16);
			m_lastBtn = (GGraph)this.GetChildAt(17);
			m_last = (GGroup)this.GetChildAt(19);
			m_nextBtn = (GGraph)this.GetChildAt(20);
			m_next = (GGroup)this.GetChildAt(22);
			m_WenZiTiShi = this.GetTransitionAt(0);
			m_AnNiu = this.GetTransitionAt(1);
		}
	}
}