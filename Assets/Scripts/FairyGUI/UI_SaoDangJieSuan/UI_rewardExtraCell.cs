/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_rewardExtraCell : GComponent
	{
		public GList m_List;

		public const string URL = "ui://34cd5b4hgojs2m";

		public static UI_rewardExtraCell CreateInstance()
		{
			return (UI_rewardExtraCell)UIPackage.CreateObject("UI_SaoDangJieSuan","rewardExtraCell");
		}

		public UI_rewardExtraCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_List = (GList)this.GetChildAt(3);
		}
	}
}