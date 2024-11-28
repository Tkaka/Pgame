/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_SuMingListItem : GComponent
	{
		public GTextField m_Name;
		public GTextField m_MiaoShu;
		public GList m_PetIconList;

		public const string URL = "ui://4asqm7awn0x05n";

		public static UI_SuMingListItem CreateInstance()
		{
			return (UI_SuMingListItem)UIPackage.CreateObject("UI_GeDouJia","SuMingListItem");
		}

		public UI_SuMingListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Name = (GTextField)this.GetChildAt(2);
			m_MiaoShu = (GTextField)this.GetChildAt(3);
			m_PetIconList = (GList)this.GetChildAt(4);
		}
	}
}