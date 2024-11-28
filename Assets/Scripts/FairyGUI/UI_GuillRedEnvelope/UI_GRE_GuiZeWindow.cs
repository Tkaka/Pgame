/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_GuiZeWindow : GComponent
	{
		public GButton m_closeBtn;
		public GList m_guizeList;

		public const string URL = "ui://r816m4tmfzr6a";

		public static UI_GRE_GuiZeWindow CreateInstance()
		{
			return (UI_GRE_GuiZeWindow)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_GuiZeWindow");
		}

		public UI_GRE_GuiZeWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (GButton)this.GetChildAt(4);
			m_guizeList = (GList)this.GetChildAt(6);
		}
	}
}