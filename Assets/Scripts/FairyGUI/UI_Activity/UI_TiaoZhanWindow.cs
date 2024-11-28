/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_TiaoZhanWindow : GComponent
	{
		public GComponent m_commonTop;
		public GList m_cardList;

		public const string URL = "ui://zwmeip9ukrhb0";

		public static UI_TiaoZhanWindow CreateInstance()
		{
			return (UI_TiaoZhanWindow)UIPackage.CreateObject("UI_Activity","TiaoZhanWindow");
		}

		public UI_TiaoZhanWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_commonTop = (GComponent)this.GetChildAt(1);
			m_cardList = (GList)this.GetChildAt(2);
		}
	}
}