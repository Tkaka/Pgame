/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_FenGeXian : GComponent
	{
		public GTextField m_FenGeXian;

		public const string URL = "ui://4asqm7awf9s532";

		public static UI_FenGeXian CreateInstance()
		{
			return (UI_FenGeXian)UIPackage.CreateObject("UI_GeDouJia","FenGeXian");
		}

		public UI_FenGeXian()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_FenGeXian = (GTextField)this.GetChildAt(2);
		}
	}
}