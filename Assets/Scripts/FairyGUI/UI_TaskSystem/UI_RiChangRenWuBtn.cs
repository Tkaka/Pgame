/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TaskSystem
{
	public partial class UI_RiChangRenWuBtn : GButton
	{
		public GImage m_hongdian;
		public GComponent m_lockGroup;

		public const string URL = "ui://zswzat1eif24d";

		public static UI_RiChangRenWuBtn CreateInstance()
		{
			return (UI_RiChangRenWuBtn)UIPackage.CreateObject("UI_TaskSystem","RiChangRenWuBtn");
		}

		public UI_RiChangRenWuBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_hongdian = (GImage)this.GetChildAt(3);
			m_lockGroup = (GComponent)this.GetChildAt(5);
		}
	}
}