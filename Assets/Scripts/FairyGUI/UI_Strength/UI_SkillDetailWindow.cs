/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_SkillDetailWindow : GComponent
	{
		public GGraph m_mask;
		public GImage m_bg;
		public GImage m_kuang;
		public GImage m_MiaoShuDiPian;
		public GTextField m_MiaoShu;
		public GLoader m_TouXiang;
		public GTextField m_nameLabel;
		public GTextField m_typeLabel;
		public GTextField m_lvLabel;
		public GGroup m_unlock;
		public GGroup m_lock;
		public GTextField m_XiaoGuo;
		public GButton m_closeBtn;
		public GImage m_XiaoJiNeng_JiaoBiao;
		public GImage m_JueJi_JiaoBiao;
		public GTextField m_JieNeng_type;

		public const string URL = "ui://qnd9tp35swzn1z";

		public static UI_SkillDetailWindow CreateInstance()
		{
			return (UI_SkillDetailWindow)UIPackage.CreateObject("UI_Strength","SkillDetailWindow");
		}

		public UI_SkillDetailWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_bg = (GImage)this.GetChildAt(1);
			m_kuang = (GImage)this.GetChildAt(2);
			m_MiaoShuDiPian = (GImage)this.GetChildAt(3);
			m_MiaoShu = (GTextField)this.GetChildAt(4);
			m_TouXiang = (GLoader)this.GetChildAt(5);
			m_nameLabel = (GTextField)this.GetChildAt(6);
			m_typeLabel = (GTextField)this.GetChildAt(8);
			m_lvLabel = (GTextField)this.GetChildAt(10);
			m_unlock = (GGroup)this.GetChildAt(11);
			m_lock = (GGroup)this.GetChildAt(14);
			m_XiaoGuo = (GTextField)this.GetChildAt(15);
			m_closeBtn = (GButton)this.GetChildAt(16);
			m_XiaoJiNeng_JiaoBiao = (GImage)this.GetChildAt(17);
			m_JueJi_JiaoBiao = (GImage)this.GetChildAt(18);
			m_JieNeng_type = (GTextField)this.GetChildAt(19);
		}
	}
}