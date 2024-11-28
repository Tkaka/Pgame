/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_objAyRewardCell : GComponent
	{
		public GImage m_imgBg;
		public GTextField m_txtAoyiName;
		public UI_ayIocnList m_ayIconList;
		public GButton m_btnGetReward;
		public GTextField m_txtAyNum;
		public GTextField m_txtCoinNum;
		public GTextField m_objActive;

		public const string URL = "ui://vexa0xrygc7j1c";

		public static UI_objAyRewardCell CreateInstance()
		{
			return (UI_objAyRewardCell)UIPackage.CreateObject("UI_AoYi","objAyRewardCell");
		}

		public UI_objAyRewardCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GImage)this.GetChildAt(0);
			m_txtAoyiName = (GTextField)this.GetChildAt(1);
			m_ayIconList = (UI_ayIocnList)this.GetChildAt(3);
			m_btnGetReward = (GButton)this.GetChildAt(4);
			m_txtAyNum = (GTextField)this.GetChildAt(7);
			m_txtCoinNum = (GTextField)this.GetChildAt(9);
			m_objActive = (GTextField)this.GetChildAt(10);
		}
	}
}