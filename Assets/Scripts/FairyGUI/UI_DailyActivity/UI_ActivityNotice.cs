/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DailyActivity
{
	public partial class UI_ActivityNotice : GComponent
	{
		public GTextField m_txtTime;
		public GTextField m_txtLeft;
		public GRichTextField m_txtContent;

		public const string URL = "ui://0n5r1ymrjqqo3";

		public static UI_ActivityNotice CreateInstance()
		{
			return (UI_ActivityNotice)UIPackage.CreateObject("UI_DailyActivity","ActivityNotice");
		}

		public UI_ActivityNotice()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtTime = (GTextField)this.GetChildAt(0);
			m_txtLeft = (GTextField)this.GetChildAt(1);
			m_txtContent = (GRichTextField)this.GetChildAt(3);
		}
	}
}