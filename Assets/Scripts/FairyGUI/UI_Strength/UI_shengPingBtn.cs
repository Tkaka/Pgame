/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_shengPingBtn : GButton
	{
		public GTextField m_title1;
		public GComponent m_lockGroup;
		public GImage m_redPoint;

		public const string URL = "ui://qnd9tp35uxhi44";

		public static UI_shengPingBtn CreateInstance()
		{
			return (UI_shengPingBtn)UIPackage.CreateObject("UI_Strength","shengPingBtn");
		}

		public UI_shengPingBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_title1 = (GTextField)this.GetChildAt(3);
			m_lockGroup = (GComponent)this.GetChildAt(4);
			m_redPoint = (GImage)this.GetChildAt(5);
		}
	}
}