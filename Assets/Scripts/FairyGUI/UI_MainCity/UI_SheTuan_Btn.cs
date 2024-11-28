/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_SheTuan_Btn : GButton
	{
		public GImage m_imgRed;

		public const string URL = "ui://jdfufi06ro1f6i";

		public static UI_SheTuan_Btn CreateInstance()
		{
			return (UI_SheTuan_Btn)UIPackage.CreateObject("UI_MainCity","SheTuan_Btn");
		}

		public UI_SheTuan_Btn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgRed = (GImage)this.GetChildAt(1);
		}
	}
}