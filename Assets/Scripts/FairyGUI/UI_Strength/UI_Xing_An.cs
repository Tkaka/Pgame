/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_Xing_An : GComponent
	{
		public GImage m_xing;

		public const string URL = "ui://qnd9tp35e9233o";

		public static UI_Xing_An CreateInstance()
		{
			return (UI_Xing_An)UIPackage.CreateObject("UI_Strength","Xing_An");
		}

		public UI_Xing_An()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_xing = (GImage)this.GetChildAt(0);
		}
	}
}