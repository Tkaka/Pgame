/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_JiangXingWindow : GComponent
	{
		public GGraph m_BeiJing;
		public GButton m_Close;
		public GTextField m_JiaGe;
		public GButton m_QuXiao;
		public GButton m_QueDing;

		public const string URL = "ui://8u3gv94nt5fah";

		public static UI_JiangXingWindow CreateInstance()
		{
			return (UI_JiangXingWindow)UIPackage.CreateObject("UI_Equip","JiangXingWindow");
		}

		public UI_JiangXingWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GGraph)this.GetChildAt(1);
			m_Close = (GButton)this.GetChildAt(5);
			m_JiaGe = (GTextField)this.GetChildAt(10);
			m_QuXiao = (GButton)this.GetChildAt(12);
			m_QueDing = (GButton)this.GetChildAt(13);
		}
	}
}