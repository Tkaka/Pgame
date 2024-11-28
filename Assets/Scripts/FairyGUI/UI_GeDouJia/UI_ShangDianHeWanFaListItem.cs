/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_ShangDianHeWanFaListItem : GComponent
	{
		public GTextField m_HuoQuLuJing;
		public GButton m_QiangWangBtn;
		public GTextField m_type;

		public const string URL = "ui://4asqm7awfps84b";

		public static UI_ShangDianHeWanFaListItem CreateInstance()
		{
			return (UI_ShangDianHeWanFaListItem)UIPackage.CreateObject("UI_GeDouJia","ShangDianHeWanFaListItem");
		}

		public UI_ShangDianHeWanFaListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_HuoQuLuJing = (GTextField)this.GetChildAt(2);
			m_QiangWangBtn = (GButton)this.GetChildAt(3);
			m_type = (GTextField)this.GetChildAt(4);
		}
	}
}