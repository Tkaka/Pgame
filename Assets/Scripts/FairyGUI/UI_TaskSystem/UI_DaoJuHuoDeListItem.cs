/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TaskSystem
{
	public partial class UI_DaoJuHuoDeListItem : GComponent
	{
		public GLoader m_Icon;
		public GTextField m_number;
		public GTextField m_name;

		public const string URL = "ui://zswzat1ek4e69";

		public static UI_DaoJuHuoDeListItem CreateInstance()
		{
			return (UI_DaoJuHuoDeListItem)UIPackage.CreateObject("UI_TaskSystem","DaoJuHuoDeListItem");
		}

		public UI_DaoJuHuoDeListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Icon = (GLoader)this.GetChildAt(1);
			m_number = (GTextField)this.GetChildAt(2);
			m_name = (GTextField)this.GetChildAt(3);
		}
	}
}