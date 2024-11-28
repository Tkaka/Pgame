/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_objNoReadInfo : GComponent
	{
		public GTextField m_txtDes;

		public const string URL = "ui://51gazvjdqknig2a";

		public static UI_objNoReadInfo CreateInstance()
		{
			return (UI_objNoReadInfo)UIPackage.CreateObject("UI_Chat","objNoReadInfo");
		}

		public UI_objNoReadInfo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(1);
		}
	}
}