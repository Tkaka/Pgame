/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TaskSystem
{
	public partial class UI_ZhuXianRenWuBtn : GButton
	{
		public GImage m_hongdian;
		public GComponent m_lockGroup;

		public const string URL = "ui://zswzat1eif24e";

		public static UI_ZhuXianRenWuBtn CreateInstance()
		{
			return (UI_ZhuXianRenWuBtn)UIPackage.CreateObject("UI_TaskSystem","ZhuXianRenWuBtn");
		}

		public UI_ZhuXianRenWuBtn()
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