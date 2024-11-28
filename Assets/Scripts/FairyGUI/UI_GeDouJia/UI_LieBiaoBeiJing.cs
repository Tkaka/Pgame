/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_LieBiaoBeiJing : GButton
	{
		public GImage m_LieBiaoBeiJing;

		public const string URL = "ui://4asqm7awm5be5u";

		public static UI_LieBiaoBeiJing CreateInstance()
		{
			return (UI_LieBiaoBeiJing)UIPackage.CreateObject("UI_GeDouJia","LieBiaoBeiJing");
		}

		public UI_LieBiaoBeiJing()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_LieBiaoBeiJing = (GImage)this.GetChildAt(0);
		}
	}
}