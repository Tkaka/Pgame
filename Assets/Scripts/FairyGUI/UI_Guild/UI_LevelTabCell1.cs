/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_LevelTabCell1 : GButton
	{
		public GImage m_imgLock;

		public const string URL = "ui://oe7ras64ts1sb3x";

		public static UI_LevelTabCell1 CreateInstance()
		{
			return (UI_LevelTabCell1)UIPackage.CreateObject("UI_Guild","LevelTabCell1");
		}

		public UI_LevelTabCell1()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgLock = (GImage)this.GetChildAt(2);
		}
	}
}