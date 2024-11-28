/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_DiaoLuoListItem : GComponent
	{
		public GLoader m_BianKuang;
		public GLoader m_TouXiang;

		public const string URL = "ui://4asqm7awfps84d";

		public static UI_DiaoLuoListItem CreateInstance()
		{
			return (UI_DiaoLuoListItem)UIPackage.CreateObject("UI_GeDouJia","DiaoLuoListItem");
		}

		public UI_DiaoLuoListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BianKuang = (GLoader)this.GetChildAt(0);
			m_TouXiang = (GLoader)this.GetChildAt(1);
		}
	}
}