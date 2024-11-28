/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_Xing_Liang : GComponent
	{
		public GImage m_xing;

		public const string URL = "ui://qnd9tp35e9233n";

		public static UI_Xing_Liang CreateInstance()
		{
			return (UI_Xing_Liang)UIPackage.CreateObject("UI_Strength","Xing_Liang");
		}

		public UI_Xing_Liang()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_xing = (GImage)this.GetChildAt(0);
		}
	}
}