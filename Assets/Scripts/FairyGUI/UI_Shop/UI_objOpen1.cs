/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_objOpen1 : GComponent
	{
		public GTextField m_txtCount;
		public GComponent m_btnOpen1;
		public GLoader m_imgComsume;
		public GTextField m_txtNum;

		public const string URL = "ui://w9mypx6jasm2n";

		public static UI_objOpen1 CreateInstance()
		{
			return (UI_objOpen1)UIPackage.CreateObject("UI_Shop","objOpen1");
		}

		public UI_objOpen1()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtCount = (GTextField)this.GetChildAt(3);
			m_btnOpen1 = (GComponent)this.GetChildAt(5);
			m_imgComsume = (GLoader)this.GetChildAt(7);
			m_txtNum = (GTextField)this.GetChildAt(8);
		}
	}
}