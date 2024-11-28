/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_scoreRewardItem : GComponent
	{
		public GTextField m_coditionLabel;
		public GList m_rewardItemList;
		public GGroup m_receivedGroup;
		public GImage m_canReveiveIcon;

		public const string URL = "ui://1wdkrtiusi7j1q";

		public static UI_scoreRewardItem CreateInstance()
		{
			return (UI_scoreRewardItem)UIPackage.CreateObject("UI_UltemateTrain","scoreRewardItem");
		}

		public UI_scoreRewardItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_coditionLabel = (GTextField)this.GetChildAt(2);
			m_rewardItemList = (GList)this.GetChildAt(3);
			m_receivedGroup = (GGroup)this.GetChildAt(6);
			m_canReveiveIcon = (GImage)this.GetChildAt(7);
		}
	}
}