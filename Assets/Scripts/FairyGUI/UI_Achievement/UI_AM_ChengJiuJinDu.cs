/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Achievement
{
	public partial class UI_AM_ChengJiuJinDu : GProgressBar
	{
		public GTextField m_number;

		public const string URL = "ui://xpd8f6j0entei";

		public static UI_AM_ChengJiuJinDu CreateInstance()
		{
			return (UI_AM_ChengJiuJinDu)UIPackage.CreateObject("UI_Achievement","AM_ChengJiuJinDu");
		}

		public UI_AM_ChengJiuJinDu()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_number = (GTextField)this.GetChildAt(2);
		}
	}
}