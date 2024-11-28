/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_LevelStar : GComponent
	{
		public GImage m_star;

		public const string URL = "ui://z04ymz0ee0tdv";

		public static UI_LevelStar CreateInstance()
		{
			return (UI_LevelStar)UIPackage.CreateObject("UI_Level","LevelStar");
		}

		public UI_LevelStar()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_star = (GImage)this.GetChildAt(0);
		}
	}
}