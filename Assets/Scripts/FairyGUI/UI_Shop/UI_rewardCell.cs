/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_rewardCell : GComponent
	{
		public GButton m_itemIcon;

		public const string URL = "ui://w9mypx6jiy9s19";

		public static UI_rewardCell CreateInstance()
		{
			return (UI_rewardCell)UIPackage.CreateObject("UI_Shop","rewardCell");
		}

		public UI_rewardCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_itemIcon = (GButton)this.GetChildAt(1);
		}
	}
}