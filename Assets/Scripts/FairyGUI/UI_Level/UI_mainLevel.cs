/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_mainLevel : GComponent
	{
		public GLoader m_bg;

		public const string URL = "ui://z04ymz0emyik1";

		public static UI_mainLevel CreateInstance()
		{
			return (UI_mainLevel)UIPackage.CreateObject("UI_Level","mainLevel");
		}

		public UI_mainLevel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GLoader)this.GetChildAt(0);
		}
	}
}