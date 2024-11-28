/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_bubble : GComponent
	{
		public GTextField m_bubbleLabel;

		public const string URL = "ui://z04ymz0e97kmi";

		public static UI_bubble CreateInstance()
		{
			return (UI_bubble)UIPackage.CreateObject("UI_Level","bubble");
		}

		public UI_bubble()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bubbleLabel = (GTextField)this.GetChildAt(1);
		}
	}
}