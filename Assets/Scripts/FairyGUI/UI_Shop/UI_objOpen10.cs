/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_objOpen10 : GComponent
	{
		public GComponent m_btnOpen10;
		public GLoader m_imgComsume;
		public GTextField m_txtNum;

		public const string URL = "ui://w9mypx6jasm2r";

		public static UI_objOpen10 CreateInstance()
		{
			return (UI_objOpen10)UIPackage.CreateObject("UI_Shop","objOpen10");
		}

		public UI_objOpen10()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnOpen10 = (GComponent)this.GetChildAt(3);
			m_imgComsume = (GLoader)this.GetChildAt(5);
			m_txtNum = (GTextField)this.GetChildAt(6);
		}
	}
}