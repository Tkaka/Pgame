/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Talent
{
	public partial class UI_talentCell1 : GComponent
	{
		public GLoader m_imgIcon;
		public GTextField m_txtLevel;
		public GImage m_imgLock;

		public const string URL = "ui://erk5lfvwm6aam";

		public static UI_talentCell1 CreateInstance()
		{
			return (UI_talentCell1)UIPackage.CreateObject("UI_Talent","talentCell1");
		}

		public UI_talentCell1()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgIcon = (GLoader)this.GetChildAt(1);
			m_txtLevel = (GTextField)this.GetChildAt(2);
			m_imgLock = (GImage)this.GetChildAt(3);
		}
	}
}