/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_playerTip : GComponent
	{
		public GTextField m_nameLabel;

		public const string URL = "ui://1wdkrtiuw0hu9";

		public static UI_playerTip CreateInstance()
		{
			return (UI_playerTip)UIPackage.CreateObject("UI_UltemateTrain","playerTip");
		}

		public UI_playerTip()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_nameLabel = (GTextField)this.GetChildAt(1);
		}
	}
}