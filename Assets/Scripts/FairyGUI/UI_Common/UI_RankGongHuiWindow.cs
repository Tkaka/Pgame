/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_RankGongHuiWindow : GComponent
	{
		public GTextField m_xuanYan;
		public GLoader m_iconLoader;
		public GTextField m_nameLabel;
		public GTextField m_sheZhanNameLabel;
		public GTextField m_paiMingLabel;
		public GTextField m_bianHaoLabel;
		public GTextField m_renShuLabel;

		public const string URL = "ui://42sthz3t12d6xkk";

		public static UI_RankGongHuiWindow CreateInstance()
		{
			return (UI_RankGongHuiWindow)UIPackage.CreateObject("UI_Common","RankGongHuiWindow");
		}

		public UI_RankGongHuiWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_xuanYan = (GTextField)this.GetChildAt(3);
			m_iconLoader = (GLoader)this.GetChildAt(4);
			m_nameLabel = (GTextField)this.GetChildAt(5);
			m_sheZhanNameLabel = (GTextField)this.GetChildAt(8);
			m_paiMingLabel = (GTextField)this.GetChildAt(9);
			m_bianHaoLabel = (GTextField)this.GetChildAt(12);
			m_renShuLabel = (GTextField)this.GetChildAt(13);
		}
	}
}