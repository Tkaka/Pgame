/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_jiNengItem : GComponent
	{
		public GLoader m_BeiJing;
		public GLoader m_TouXiang;
		public GGraph m_Shengji;
		public UI_SkillUpBtn m_addBtn;
		public GTextField m_goldLabel;
		public GTextField m_nameLabel;
		public GTextField m_lockTipLabel;
		public GImage m_jinbitubiao;
		public GGraph m_skillAtrributeTipPos;
		public GImage m_JiNengKuang;
		public GTextField m_skillLevelLabel;
		public GImage m_suo;
		public GImage m_XiaoJiNeng_JiaoBiao;
		public GImage m_JueJi_JiaoBiao;
		public GTextField m_JieNeng_type;
		public Transition m_shuzhi;
		public Transition m_JiaHao;

		public const string URL = "ui://qnd9tp35swzn1y";

		public static UI_jiNengItem CreateInstance()
		{
			return (UI_jiNengItem)UIPackage.CreateObject("UI_Strength","jiNengItem");
		}

		public UI_jiNengItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GLoader)this.GetChildAt(1);
			m_TouXiang = (GLoader)this.GetChildAt(2);
			m_Shengji = (GGraph)this.GetChildAt(3);
			m_addBtn = (UI_SkillUpBtn)this.GetChildAt(4);
			m_goldLabel = (GTextField)this.GetChildAt(5);
			m_nameLabel = (GTextField)this.GetChildAt(6);
			m_lockTipLabel = (GTextField)this.GetChildAt(7);
			m_jinbitubiao = (GImage)this.GetChildAt(8);
			m_skillAtrributeTipPos = (GGraph)this.GetChildAt(9);
			m_JiNengKuang = (GImage)this.GetChildAt(10);
			m_skillLevelLabel = (GTextField)this.GetChildAt(11);
			m_suo = (GImage)this.GetChildAt(12);
			m_XiaoJiNeng_JiaoBiao = (GImage)this.GetChildAt(13);
			m_JueJi_JiaoBiao = (GImage)this.GetChildAt(14);
			m_JieNeng_type = (GTextField)this.GetChildAt(15);
			m_shuzhi = this.GetTransitionAt(0);
			m_JiaHao = this.GetTransitionAt(1);
		}
	}
}