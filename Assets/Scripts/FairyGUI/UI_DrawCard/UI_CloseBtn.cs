/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_CloseBtn : GButton
	{
		public GImage m_CloseBtn;

		public const string URL = "ui://zy7t2yegxbfd1e";

		public static UI_CloseBtn CreateInstance()
		{
			return (UI_CloseBtn)UIPackage.CreateObject("UI_DrawCard","CloseBtn");
		}

		public UI_CloseBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_CloseBtn = (GImage)this.GetChildAt(0);
		}
	}
}