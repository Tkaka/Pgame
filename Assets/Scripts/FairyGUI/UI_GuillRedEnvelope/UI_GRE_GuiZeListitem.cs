/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_GuiZeListitem : GComponent
	{
		public GImage m_beijing;
		public GTextField m_miaoshu;

		public const string URL = "ui://r816m4tmjh7wj";

		public static UI_GRE_GuiZeListitem CreateInstance()
		{
			return (UI_GRE_GuiZeListitem)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_GuiZeListitem");
		}

		public UI_GRE_GuiZeListitem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_beijing = (GImage)this.GetChildAt(0);
			m_miaoshu = (GTextField)this.GetChildAt(1);
		}
	}
}