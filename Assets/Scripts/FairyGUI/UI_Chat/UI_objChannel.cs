/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_objChannel : GComponent
	{
		public GLoader m_imgChannel;
		public GTextField m_txtChannel;

		public const string URL = "ui://51gazvjd7igt1f";

		public static UI_objChannel CreateInstance()
		{
			return (UI_objChannel)UIPackage.CreateObject("UI_Chat","objChannel");
		}

		public UI_objChannel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgChannel = (GLoader)this.GetChildAt(0);
			m_txtChannel = (GTextField)this.GetChildAt(1);
		}
	}
}