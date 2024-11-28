/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_JinHuaLian
{
	public partial class UI_JinHuaLianWindow : GComponent
	{
		public GList m_XingTaiList;
		public GButton m_ColseBtn;
		public GButton m_qiehuanBtn;

		public const string URL = "ui://n8vdy261mrp70";

		public static UI_JinHuaLianWindow CreateInstance()
		{
			return (UI_JinHuaLianWindow)UIPackage.CreateObject("UI_JinHuaLian","JinHuaLianWindow");
		}

		public UI_JinHuaLianWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_XingTaiList = (GList)this.GetChildAt(5);
			m_ColseBtn = (GButton)this.GetChildAt(6);
			m_qiehuanBtn = (GButton)this.GetChildAt(7);
		}
	}
}