/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_EvaluateTip : GComponent
	{
		public GTextField m_hurtTxt;
		public GLoader m_levelImg;

		public const string URL = "ui://028ppdzhq99vb4";

		public static UI_EvaluateTip CreateInstance()
		{
			return (UI_EvaluateTip)UIPackage.CreateObject("UI_Battle","EvaluateTip");
		}

		public UI_EvaluateTip()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_hurtTxt = (GTextField)this.GetChildAt(0);
			m_levelImg = (GLoader)this.GetChildAt(1);
		}
	}
}