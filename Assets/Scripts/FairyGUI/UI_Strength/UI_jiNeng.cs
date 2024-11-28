/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_jiNeng : GComponent
	{
		public GLoader m_typeLoder;
		public GTextField m_nameLabel;
		public GList m_starList;
		public GTextField m_levelLabel;
		public GTextField m_zhanDouLiLabel;
		public GList m_jiNengList;
		public GTextField m_JiNengDian;
		public UI_SkillUpBtn m_JiNengDianGouMai;

		public const string URL = "ui://qnd9tp35p9fv1m";

		public static UI_jiNeng CreateInstance()
		{
			return (UI_jiNeng)UIPackage.CreateObject("UI_Strength","jiNeng");
		}

		public UI_jiNeng()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_typeLoder = (GLoader)this.GetChildAt(2);
			m_nameLabel = (GTextField)this.GetChildAt(3);
			m_starList = (GList)this.GetChildAt(4);
			m_levelLabel = (GTextField)this.GetChildAt(6);
			m_zhanDouLiLabel = (GTextField)this.GetChildAt(8);
			m_jiNengList = (GList)this.GetChildAt(9);
			m_JiNengDian = (GTextField)this.GetChildAt(10);
			m_JiNengDianGouMai = (UI_SkillUpBtn)this.GetChildAt(11);
		}
	}
}