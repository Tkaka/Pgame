/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_txtNoReward : GComponent
	{
		public GTextField m_txtNoReward;

		public const string URL = "ui://34cd5b4hiwuz18";

		public static UI_txtNoReward CreateInstance()
		{
			return (UI_txtNoReward)UIPackage.CreateObject("UI_SaoDangJieSuan","txtNoReward");
		}

		public UI_txtNoReward()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtNoReward = (GTextField)this.GetChildAt(0);
		}
	}
}