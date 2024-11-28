/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_zhanKuanLunCiItem : GComponent
	{
		public GTextField m_lunCiNum;

		public const string URL = "ui://xj95784riouo2y";

		public static UI_zhanKuanLunCiItem CreateInstance()
		{
			return (UI_zhanKuanLunCiItem)UIPackage.CreateObject("UI_GuildBattle","zhanKuanLunCiItem");
		}

		public UI_zhanKuanLunCiItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_lunCiNum = (GTextField)this.GetChildAt(1);
		}
	}
}