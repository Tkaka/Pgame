/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_shengPing : GComponent
	{
		public GList m_caiLiaoList;
		public UI_shengPingNormalBtn m_shengPBtn;
		public GTextField m_goldLabel;
		public GGroup m_gold;
		public GTextField m_needLvLabel;
		public GProgressBar m_materialJinDuTiao;
		public GTextField m_addAtkTipLabel;
		public GTextField m_addDefTipLabel;
		public GTextField m_addHpTipLabel;
		public GTextField m_progressTip;
		public Transition m_showShuXingTrans;

		public const string URL = "ui://qnd9tp35hccnz";

		public static UI_shengPing CreateInstance()
		{
			return (UI_shengPing)UIPackage.CreateObject("UI_Strength","shengPing");
		}

		public UI_shengPing()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_caiLiaoList = (GList)this.GetChildAt(1);
			m_shengPBtn = (UI_shengPingNormalBtn)this.GetChildAt(2);
			m_goldLabel = (GTextField)this.GetChildAt(5);
			m_gold = (GGroup)this.GetChildAt(6);
			m_needLvLabel = (GTextField)this.GetChildAt(8);
			m_materialJinDuTiao = (GProgressBar)this.GetChildAt(9);
			m_addAtkTipLabel = (GTextField)this.GetChildAt(10);
			m_addDefTipLabel = (GTextField)this.GetChildAt(11);
			m_addHpTipLabel = (GTextField)this.GetChildAt(12);
			m_progressTip = (GTextField)this.GetChildAt(13);
			m_showShuXingTrans = this.GetTransitionAt(0);
		}
	}
}