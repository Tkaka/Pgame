/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Achievement
{
	public partial class UI_AM_YangChengBtn : GButton
	{
		public GImage m_HongDian;

		public const string URL = "ui://xpd8f6j0e2gr2";

		public static UI_AM_YangChengBtn CreateInstance()
		{
			return (UI_AM_YangChengBtn)UIPackage.CreateObject("UI_Achievement","AM_YangChengBtn");
		}

		public UI_AM_YangChengBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_HongDian = (GImage)this.GetChildAt(2);
		}
	}
}