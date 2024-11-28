/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_ShenQi
{
	public partial class UI_textField : GComponent
	{
		public GTextField m_contentLabel;

		public const string URL = "ui://bi2nkn43fd9in";

		public static UI_textField CreateInstance()
		{
			return (UI_textField)UIPackage.CreateObject("UI_ShenQi","textField");
		}

		public UI_textField()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_contentLabel = (GTextField)this.GetChildAt(0);
		}
	}
}