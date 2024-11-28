/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_XuanZeListItem : GComponent
	{
		public UI_GD_TouXiang m_pet;
		public GTextField m_name;
		public GTextField m_level;
		public UI_GD_JingYanJinDu m_jingyanjindu;
		public GTextField m_manji;
		public GButton m_xuanzeBtn;

		public const string URL = "ui://wwlsouxzkzeuc";

		public static UI_GD_XuanZeListItem CreateInstance()
		{
			return (UI_GD_XuanZeListItem)UIPackage.CreateObject("UI_GuildDrill","GD_XuanZeListItem");
		}

		public UI_GD_XuanZeListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_pet = (UI_GD_TouXiang)this.GetChildAt(1);
			m_name = (GTextField)this.GetChildAt(2);
			m_level = (GTextField)this.GetChildAt(3);
			m_jingyanjindu = (UI_GD_JingYanJinDu)this.GetChildAt(5);
			m_manji = (GTextField)this.GetChildAt(6);
			m_xuanzeBtn = (GButton)this.GetChildAt(7);
		}
	}
}