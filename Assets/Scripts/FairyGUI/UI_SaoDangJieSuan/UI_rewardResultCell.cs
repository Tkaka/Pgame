/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_rewardResultCell : GComponent
	{
		public GTextField m_txtFightNumber;
		public UI_JingYan m_expAndCoin;
		public GList m_List;
		public GGroup m_rewardGroup;
		public GTextField m_txtNoReward;

		public const string URL = "ui://34cd5b4hgojs2l";

		public static UI_rewardResultCell CreateInstance()
		{
			return (UI_rewardResultCell)UIPackage.CreateObject("UI_SaoDangJieSuan","rewardResultCell");
		}

		public UI_rewardResultCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtFightNumber = (GTextField)this.GetChildAt(2);
			m_expAndCoin = (UI_JingYan)this.GetChildAt(3);
			m_List = (GList)this.GetChildAt(4);
			m_rewardGroup = (GGroup)this.GetChildAt(5);
			m_txtNoReward = (GTextField)this.GetChildAt(6);
		}
	}
}