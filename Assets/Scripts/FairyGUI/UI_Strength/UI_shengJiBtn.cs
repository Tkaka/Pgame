/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_shengJiBtn : GButton
	{
		public GTextField m_title1;
		public GComponent m_lockGroup;

		public const string URL = "ui://qnd9tp35uxhi45";

		public static UI_shengJiBtn CreateInstance()
		{
			return (UI_shengJiBtn)UIPackage.CreateObject("UI_Strength","shengJiBtn");
		}

		public UI_shengJiBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_title1 = (GTextField)this.GetChildAt(3);
			m_lockGroup = (GComponent)this.GetChildAt(4);
		}
	}
}