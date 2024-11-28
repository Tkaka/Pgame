/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_FaHongBaoWindow : GComponent
	{
		public GButton m_closeBtn;
		public GList m_typeList;

		public const string URL = "ui://r816m4tmfzr6d";

		public static UI_GRE_FaHongBaoWindow CreateInstance()
		{
			return (UI_GRE_FaHongBaoWindow)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_FaHongBaoWindow");
		}

		public UI_GRE_FaHongBaoWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (GButton)this.GetChildAt(3);
			m_typeList = (GList)this.GetChildAt(7);
		}
	}
}