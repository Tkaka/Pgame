/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_KFHD_FightPower
{
	public partial class UI_TabDesc : GComponent
	{
		public GGraph m_touch;
		public GLoader m_model;
		public GTextField m_name;

		public const string URL = "ui://9kjh5gh09gduh";

		public static UI_TabDesc CreateInstance()
		{
			return (UI_TabDesc)UIPackage.CreateObject("UI_KFHD_FightPower","TabDesc");
		}

		public UI_TabDesc()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_touch = (GGraph)this.GetChildAt(0);
			m_model = (GLoader)this.GetChildAt(1);
			m_name = (GTextField)this.GetChildAt(3);
		}
	}
}