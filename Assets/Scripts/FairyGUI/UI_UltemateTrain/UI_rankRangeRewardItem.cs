/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_rankRangeRewardItem : GComponent
	{
		public GTextField m_conditionLabel;
		public GList m_rewardItemList;

		public const string URL = "ui://1wdkrtiusi7j1t";

		public static UI_rankRangeRewardItem CreateInstance()
		{
			return (UI_rankRangeRewardItem)UIPackage.CreateObject("UI_UltemateTrain","rankRangeRewardItem");
		}

		public UI_rankRangeRewardItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_conditionLabel = (GTextField)this.GetChildAt(2);
			m_rewardItemList = (GList)this.GetChildAt(3);
		}
	}
}