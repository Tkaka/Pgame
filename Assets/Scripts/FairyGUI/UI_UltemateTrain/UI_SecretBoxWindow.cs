/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_SecretBoxWindow : GComponent
	{
		public GGraph m_mask;
		public GTextField m_titleLabel;
		public GTextField m_tipLabel;
		public GButton m_buyBtn;
		public GButton m_leaveBtn;
		public GList m_itemList;
		public GTextField m_comsumeDiamondLabel;
		public GTextField m_remainTimes;
		public GTextField m_remainDiamondLabel;

		public const string URL = "ui://1wdkrtiuw0hu14";

		public static UI_SecretBoxWindow CreateInstance()
		{
			return (UI_SecretBoxWindow)UIPackage.CreateObject("UI_UltemateTrain","SecretBoxWindow");
		}

		public UI_SecretBoxWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_titleLabel = (GTextField)this.GetChildAt(1);
			m_tipLabel = (GTextField)this.GetChildAt(2);
			m_buyBtn = (GButton)this.GetChildAt(4);
			m_leaveBtn = (GButton)this.GetChildAt(5);
			m_itemList = (GList)this.GetChildAt(6);
			m_comsumeDiamondLabel = (GTextField)this.GetChildAt(8);
			m_remainTimes = (GTextField)this.GetChildAt(11);
			m_remainDiamondLabel = (GTextField)this.GetChildAt(14);
		}
	}
}