/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_PageReward2 : GComponent
	{
		public GTextField m_txtCurRankDes;
		public GList m_rewardList;
		public GList m_rankRewardList;
		public GTextField m_txtRank;

		public const string URL = "ui://3xs7lfyxgawd1y";

		public static UI_PageReward2 CreateInstance()
		{
			return (UI_PageReward2)UIPackage.CreateObject("UI_Arena","PageReward2");
		}

		public UI_PageReward2()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtCurRankDes = (GTextField)this.GetChildAt(0);
			m_rewardList = (GList)this.GetChildAt(1);
			m_rankRewardList = (GList)this.GetChildAt(2);
			m_txtRank = (GTextField)this.GetChildAt(5);
		}
	}
}