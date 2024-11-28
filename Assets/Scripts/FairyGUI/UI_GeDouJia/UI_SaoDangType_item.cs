/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_SaoDangType_item : GButton
	{
		public GImage m_BeiJing;

		public const string URL = "ui://4asqm7awiou15z";

		public static UI_SaoDangType_item CreateInstance()
		{
			return (UI_SaoDangType_item)UIPackage.CreateObject("UI_GeDouJia","SaoDangType_item");
		}

		public UI_SaoDangType_item()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GImage)this.GetChildAt(0);
		}
	}
}