/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_jingJi_Btn : GButton
	{
		public GImage m_imgRed;

		public const string URL = "ui://jdfufi06ro1f69";

		public static UI_jingJi_Btn CreateInstance()
		{
			return (UI_jingJi_Btn)UIPackage.CreateObject("UI_MainCity","jingJi_Btn");
		}

		public UI_jingJi_Btn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgRed = (GImage)this.GetChildAt(1);
		}
	}
}