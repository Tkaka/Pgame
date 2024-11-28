/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_commonRankPanel : GComponent
	{
		public GList m_PaiHangList;
		public GTextField m_name;
		public GLoader m_Icon;
		public GTextField m_level;
		public GTextField m_GuilName;
		public GButton m_ChaKanBtn;
		public GComponent m_Rank_MyData;
		public GComponent m_Guild_MyData;
		public GTextField m_paiming;
		public GTextField m_juesemign;
		public GTextField m_type1;
		public GTextField m_type2;

		public const string URL = "ui://42sthz3thf5jxr3";

		public static UI_commonRankPanel CreateInstance()
		{
			return (UI_commonRankPanel)UIPackage.CreateObject("UI_Common","commonRankPanel");
		}

		public UI_commonRankPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_PaiHangList = (GList)this.GetChildAt(2);
			m_name = (GTextField)this.GetChildAt(3);
			m_Icon = (GLoader)this.GetChildAt(4);
			m_level = (GTextField)this.GetChildAt(5);
			m_GuilName = (GTextField)this.GetChildAt(6);
			m_ChaKanBtn = (GButton)this.GetChildAt(7);
			m_Rank_MyData = (GComponent)this.GetChildAt(8);
			m_Guild_MyData = (GComponent)this.GetChildAt(9);
			m_paiming = (GTextField)this.GetChildAt(11);
			m_juesemign = (GTextField)this.GetChildAt(12);
			m_type1 = (GTextField)this.GetChildAt(13);
			m_type2 = (GTextField)this.GetChildAt(14);
		}
	}
}