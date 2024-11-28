/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_ExplainListItem : GComponent
	{
		public GTextField m_MiaoShu;

		public const string URL = "ui://yo5kunkic4uwk";

		public static UI_ExplainListItem CreateInstance()
		{
			return (UI_ExplainListItem)UIPackage.CreateObject("UI_HallFame","ExplainListItem");
		}

		public UI_ExplainListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_MiaoShu = (GTextField)this.GetChildAt(1);
		}
	}
}