/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_objSelect : GComponent
	{
		public GImage m_imgSelect;

		public const string URL = "ui://51gazvjdkb3124";

		public static UI_objSelect CreateInstance()
		{
			return (UI_objSelect)UIPackage.CreateObject("UI_Chat","objSelect");
		}

		public UI_objSelect()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgSelect = (GImage)this.GetChildAt(1);
		}
	}
}