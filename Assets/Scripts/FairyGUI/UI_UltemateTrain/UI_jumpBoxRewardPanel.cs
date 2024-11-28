/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_jumpBoxRewardPanel : GComponent
	{
		public GTextField m_tipLabel;
		public GList m_rewardItemList;

		public const string URL = "ui://1wdkrtiuw0huw";

		public static UI_jumpBoxRewardPanel CreateInstance()
		{
			return (UI_jumpBoxRewardPanel)UIPackage.CreateObject("UI_UltemateTrain","jumpBoxRewardPanel");
		}

		public UI_jumpBoxRewardPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tipLabel = (GTextField)this.GetChildAt(0);
			m_rewardItemList = (GList)this.GetChildAt(1);
		}
	}
}