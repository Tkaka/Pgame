/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_trainRankRewardPanel : GComponent
	{
		public GTextField m_curRankLabel;
		public GTextField m_curRankRange;
		public GList m_getRewardList;
		public GTextField m_tipLabel;
		public GList m_rankRewardList;
		public GButton m_switchLeftBtn;
		public GButton m_switchRightBtn;

		public const string URL = "ui://1wdkrtiusi7j1u";

		public static UI_trainRankRewardPanel CreateInstance()
		{
			return (UI_trainRankRewardPanel)UIPackage.CreateObject("UI_UltemateTrain","trainRankRewardPanel");
		}

		public UI_trainRankRewardPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_curRankLabel = (GTextField)this.GetChildAt(2);
			m_curRankRange = (GTextField)this.GetChildAt(3);
			m_getRewardList = (GList)this.GetChildAt(5);
			m_tipLabel = (GTextField)this.GetChildAt(6);
			m_rankRewardList = (GList)this.GetChildAt(7);
			m_switchLeftBtn = (GButton)this.GetChildAt(8);
			m_switchRightBtn = (GButton)this.GetChildAt(9);
		}
	}
}