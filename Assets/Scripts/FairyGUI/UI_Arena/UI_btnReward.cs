/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_btnReward : GComponent
	{
		public GImage m_imgRewardRed;

		public const string URL = "ui://3xs7lfyxo0dep";

		public static UI_btnReward CreateInstance()
		{
			return (UI_btnReward)UIPackage.CreateObject("UI_Arena","btnReward");
		}

		public UI_btnReward()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgRewardRed = (GImage)this.GetChildAt(2);
		}
	}
}