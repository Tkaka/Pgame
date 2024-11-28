/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Talent
{
	public partial class UI_talentCell2 : GComponent
	{
		public UI_talentCell1 m_objTalent;

		public const string URL = "ui://erk5lfvwm6aao";

		public static UI_talentCell2 CreateInstance()
		{
			return (UI_talentCell2)UIPackage.CreateObject("UI_Talent","talentCell2");
		}

		public UI_talentCell2()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_objTalent = (UI_talentCell1)this.GetChildAt(0);
		}
	}
}