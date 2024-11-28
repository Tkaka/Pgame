/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_HF_shuxing : GComponent
	{
		public GTextField m_type;
		public GTextField m_number;

		public const string URL = "ui://yo5kunkilddyn";

		public static UI_HF_shuxing CreateInstance()
		{
			return (UI_HF_shuxing)UIPackage.CreateObject("UI_HallFame","HF_shuxing");
		}

		public UI_HF_shuxing()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_type = (GTextField)this.GetChildAt(1);
			m_number = (GTextField)this.GetChildAt(2);
		}
	}
}