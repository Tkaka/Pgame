/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_RankRoleInfoWindow : GComponent
	{
		public GTextField m_nameLabel;
		public GTextField m_lvLabel;
		public GTextField m_paiMingLabel;
		public GTextField m_zhanDouLiLabel;
		public GButton m_siLiaoBtn;
		public GButton m_jiaHaoYouBtn;

		public const string URL = "ui://42sthz3t12d6xkl";

		public static UI_RankRoleInfoWindow CreateInstance()
		{
			return (UI_RankRoleInfoWindow)UIPackage.CreateObject("UI_Common","RankRoleInfoWindow");
		}

		public UI_RankRoleInfoWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_nameLabel = (GTextField)this.GetChildAt(4);
			m_lvLabel = (GTextField)this.GetChildAt(6);
			m_paiMingLabel = (GTextField)this.GetChildAt(8);
			m_zhanDouLiLabel = (GTextField)this.GetChildAt(10);
			m_siLiaoBtn = (GButton)this.GetChildAt(13);
			m_jiaHaoYouBtn = (GButton)this.GetChildAt(14);
		}
	}
}