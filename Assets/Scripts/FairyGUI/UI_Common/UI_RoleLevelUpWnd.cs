/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_RoleLevelUpWnd : GComponent
	{
		public GGraph m_mask;
		public UI_xingxing_ain_l m_leftStarAnim;
		public UI_xingxing_ain_r m_rightStarAnim;
		public GTextField m_zhan;
		public GTextField m_txtLevelPre;
		public GTextField m_txtLevelNext;
		public GTextField m_zhan_2;
		public GTextField m_txtTiLiPre;
		public GTextField m_txtTiLiNext;
		public GTextField m_zhan_3;
		public GTextField m_txtTLLimitPre;
		public GTextField m_txtTLLimitNext;
		public Transition m_animation;

		public const string URL = "ui://42sthz3tjx87xky";

		public static UI_RoleLevelUpWnd CreateInstance()
		{
			return (UI_RoleLevelUpWnd)UIPackage.CreateObject("UI_Common","RoleLevelUpWnd");
		}

		public UI_RoleLevelUpWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_leftStarAnim = (UI_xingxing_ain_l)this.GetChildAt(2);
			m_rightStarAnim = (UI_xingxing_ain_r)this.GetChildAt(3);
			m_zhan = (GTextField)this.GetChildAt(6);
			m_txtLevelPre = (GTextField)this.GetChildAt(8);
			m_txtLevelNext = (GTextField)this.GetChildAt(9);
			m_zhan_2 = (GTextField)this.GetChildAt(10);
			m_txtTiLiPre = (GTextField)this.GetChildAt(12);
			m_txtTiLiNext = (GTextField)this.GetChildAt(13);
			m_zhan_3 = (GTextField)this.GetChildAt(14);
			m_txtTLLimitPre = (GTextField)this.GetChildAt(16);
			m_txtTLLimitNext = (GTextField)this.GetChildAt(17);
			m_animation = this.GetTransitionAt(0);
		}
	}
}