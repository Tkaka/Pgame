/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Talent
{
	public partial class UI_talentCell3 : GComponent
	{
		public UI_talentCell2 m_t3;
		public UI_talentCell2 m_t2;
		public UI_talentCell2 m_t1;
		public GLoader m_imgName;

		public const string URL = "ui://erk5lfvwm6aap";

		public static UI_talentCell3 CreateInstance()
		{
			return (UI_talentCell3)UIPackage.CreateObject("UI_Talent","talentCell3");
		}

		public UI_talentCell3()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_t3 = (UI_talentCell2)this.GetChildAt(0);
			m_t2 = (UI_talentCell2)this.GetChildAt(1);
			m_t1 = (UI_talentCell2)this.GetChildAt(2);
			m_imgName = (GLoader)this.GetChildAt(3);
		}
	}
}