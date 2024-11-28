/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_shengJi : GComponent
	{
		public GProgressBar m_expJinDuTiao;
		public GTextField m_levelLabel;
		public GList m_expPropList;
		public GTextField m_useNumLabel;
		public GTextField m_progressTip;
		public GTextField m_fullLevelTip;
		public GGraph m_lvEffecPos;
		public Transition m_levelUpAnim;

		public const string URL = "ui://qnd9tp35hccn10";

		public static UI_shengJi CreateInstance()
		{
			return (UI_shengJi)UIPackage.CreateObject("UI_Strength","shengJi");
		}

		public UI_shengJi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_expJinDuTiao = (GProgressBar)this.GetChildAt(2);
			m_levelLabel = (GTextField)this.GetChildAt(3);
			m_expPropList = (GList)this.GetChildAt(5);
			m_useNumLabel = (GTextField)this.GetChildAt(6);
			m_progressTip = (GTextField)this.GetChildAt(7);
			m_fullLevelTip = (GTextField)this.GetChildAt(8);
			m_lvEffecPos = (GGraph)this.GetChildAt(10);
			m_levelUpAnim = this.GetTransitionAt(0);
		}
	}
}