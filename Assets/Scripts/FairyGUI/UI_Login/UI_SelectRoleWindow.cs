/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Login
{
	public partial class UI_SelectRoleWindow : GComponent
	{
		public Controller m_gender;
		public GGraph m_TiaoGuo;
		public GImage m_nan;
		public GButton m_chuangjianjuese;
		public GButton m_jinruyouxi;
		public GImage m_ChuangJian;
		public GGroup m_JinRuYouXi;
		public GTextInput m_roleName;
		public GComponent m_ChongZhiBtn;
		public GComponent m_OkBtn;
		public GButton m_saiZiBtn;
		public GGroup m_JueSeMing;
		public GTextField m_Name;
		public GGroup m_ShouCiHuanYing;
		public GGroup m_YouXi;
		public Transition m_Xianshi;

		public const string URL = "ui://hg19ijpap1l14";

		public static UI_SelectRoleWindow CreateInstance()
		{
			return (UI_SelectRoleWindow)UIPackage.CreateObject("UI_Login","SelectRoleWindow");
		}

		public UI_SelectRoleWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_gender = this.GetControllerAt(0);
			m_TiaoGuo = (GGraph)this.GetChildAt(0);
			m_nan = (GImage)this.GetChildAt(1);
			m_chuangjianjuese = (GButton)this.GetChildAt(2);
			m_jinruyouxi = (GButton)this.GetChildAt(3);
			m_ChuangJian = (GImage)this.GetChildAt(4);
			m_JinRuYouXi = (GGroup)this.GetChildAt(5);
			m_roleName = (GTextInput)this.GetChildAt(8);
			m_ChongZhiBtn = (GComponent)this.GetChildAt(10);
			m_OkBtn = (GComponent)this.GetChildAt(11);
			m_saiZiBtn = (GButton)this.GetChildAt(12);
			m_JueSeMing = (GGroup)this.GetChildAt(13);
			m_Name = (GTextField)this.GetChildAt(16);
			m_ShouCiHuanYing = (GGroup)this.GetChildAt(18);
			m_YouXi = (GGroup)this.GetChildAt(19);
			m_Xianshi = this.GetTransitionAt(0);
		}
	}
}