/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TaskSystem
{
	public partial class UI_BaoXiangHuoDeWindow : GComponent
	{
		public GButton m_CloseBtn;
		public GComponent m_QueDing;
		public GList m_JiangLiList;

		public const string URL = "ui://zswzat1ek4e67";

		public static UI_BaoXiangHuoDeWindow CreateInstance()
		{
			return (UI_BaoXiangHuoDeWindow)UIPackage.CreateObject("UI_TaskSystem","BaoXiangHuoDeWindow");
		}

		public UI_BaoXiangHuoDeWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_CloseBtn = (GButton)this.GetChildAt(1);
			m_QueDing = (GComponent)this.GetChildAt(2);
			m_JiangLiList = (GList)this.GetChildAt(6);
		}
	}
}