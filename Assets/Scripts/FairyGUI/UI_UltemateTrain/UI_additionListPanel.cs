/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_additionListPanel : GComponent
	{
		public GImage m_propertyBg;
		public GTextField m_propertyListLabel;

		public const string URL = "ui://1wdkrtiuw0huy";

		public static UI_additionListPanel CreateInstance()
		{
			return (UI_additionListPanel)UIPackage.CreateObject("UI_UltemateTrain","additionListPanel");
		}

		public UI_additionListPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_propertyBg = (GImage)this.GetChildAt(0);
			m_propertyListLabel = (GTextField)this.GetChildAt(1);
		}
	}
}