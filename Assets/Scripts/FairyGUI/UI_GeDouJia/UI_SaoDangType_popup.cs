/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_SaoDangType_popup : GComponent
	{
		public GList m_list;

		public const string URL = "ui://4asqm7awiou160";

		public static UI_SaoDangType_popup CreateInstance()
		{
			return (UI_SaoDangType_popup)UIPackage.CreateObject("UI_GeDouJia","SaoDangType_popup");
		}

		public UI_SaoDangType_popup()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_list = (GList)this.GetChildAt(1);
		}
	}
}