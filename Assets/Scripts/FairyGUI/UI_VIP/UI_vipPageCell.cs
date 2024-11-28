/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_VIP
{
	public partial class UI_vipPageCell : GComponent
	{
		public GList m_desList;
		public GTextField m_txtRewardDes;
		public GTextField m_txtOriginalPrice;
		public GTextField m_txtCurPrice;
		public GList m_rewardList;
		public GButton m_btnBuy;
		public GTextField m_objBuyed;

		public const string URL = "ui://7pvd5vi4qaa2f";

		public static UI_vipPageCell CreateInstance()
		{
			return (UI_vipPageCell)UIPackage.CreateObject("UI_VIP","vipPageCell");
		}

		public UI_vipPageCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_desList = (GList)this.GetChildAt(1);
			m_txtRewardDes = (GTextField)this.GetChildAt(3);
			m_txtOriginalPrice = (GTextField)this.GetChildAt(8);
			m_txtCurPrice = (GTextField)this.GetChildAt(9);
			m_rewardList = (GList)this.GetChildAt(11);
			m_btnBuy = (GButton)this.GetChildAt(12);
			m_objBuyed = (GTextField)this.GetChildAt(13);
		}
	}
}