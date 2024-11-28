/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TaskSystem
{
	public partial class UI_YiWanChengBtn : GComponent
	{
		public GGraph m_WanChengDongXiao;
		public GGroup m_YiWanCheng;

		public const string URL = "ui://zswzat1eetrtc";

		public static UI_YiWanChengBtn CreateInstance()
		{
			return (UI_YiWanChengBtn)UIPackage.CreateObject("UI_TaskSystem","YiWanChengBtn");
		}

		public UI_YiWanChengBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_WanChengDongXiao = (GGraph)this.GetChildAt(2);
			m_YiWanCheng = (GGroup)this.GetChildAt(3);
		}
	}
}