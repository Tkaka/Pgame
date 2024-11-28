/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_RedEnvelopeItem : GComponent
	{
		public GTextField m_name;
		public GLoader m_type;
		public GTextField m_yiqiangguo;
		public GTextField m_JieSuoYuYan;
		public GGroup m_JieSuo;

		public const string URL = "ui://r816m4tmfzr6e";

		public static UI_GRE_RedEnvelopeItem CreateInstance()
		{
			return (UI_GRE_RedEnvelopeItem)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_RedEnvelopeItem");
		}

		public UI_GRE_RedEnvelopeItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_name = (GTextField)this.GetChildAt(1);
			m_type = (GLoader)this.GetChildAt(2);
			m_yiqiangguo = (GTextField)this.GetChildAt(4);
			m_JieSuoYuYan = (GTextField)this.GetChildAt(6);
			m_JieSuo = (GGroup)this.GetChildAt(7);
		}
	}
}