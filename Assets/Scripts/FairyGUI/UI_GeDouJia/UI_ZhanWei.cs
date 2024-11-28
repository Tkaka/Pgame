/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_ZhanWei : GComponent
	{
		public GGraph m_zhanwei;

		public const string URL = "ui://4asqm7awi4kd5p";

		public static UI_ZhanWei CreateInstance()
		{
			return (UI_ZhanWei)UIPackage.CreateObject("UI_GeDouJia","ZhanWei");
		}

		public UI_ZhanWei()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_zhanwei = (GGraph)this.GetChildAt(0);
		}
	}
}