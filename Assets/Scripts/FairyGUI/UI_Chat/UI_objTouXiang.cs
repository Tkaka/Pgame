/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_objTouXiang : GComponent
	{
		public GLoader m_imgTitle;
		public GLoader m_imgIcon;

		public const string URL = "ui://51gazvjd7igt1c";

		public static UI_objTouXiang CreateInstance()
		{
			return (UI_objTouXiang)UIPackage.CreateObject("UI_Chat","objTouXiang");
		}

		public UI_objTouXiang()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgTitle = (GLoader)this.GetChildAt(0);
			m_imgIcon = (GLoader)this.GetChildAt(1);
		}
	}
}