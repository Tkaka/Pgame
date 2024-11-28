/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_emojiCell : GComponent
	{
		public GLoader m_imgLoad;

		public const string URL = "ui://51gazvjd10ifg28";

		public static UI_emojiCell CreateInstance()
		{
			return (UI_emojiCell)UIPackage.CreateObject("UI_Chat","emojiCell");
		}

		public UI_emojiCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgLoad = (GLoader)this.GetChildAt(1);
		}
	}
}