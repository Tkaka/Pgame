/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_bigDou : GComponent
	{
		public GImage m_dou;

		public const string URL = "ui://42sthz3tm4bixqa";

		public static UI_bigDou CreateInstance()
		{
			return (UI_bigDou)UIPackage.CreateObject("UI_Common","bigDou");
		}

		public UI_bigDou()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_dou = (GImage)this.GetChildAt(0);
		}
	}
}