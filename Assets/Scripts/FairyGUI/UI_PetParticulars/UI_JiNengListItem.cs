/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_JiNengListItem : GComponent
	{
		public GLoader m_icon;
		public GTextField m_name;
		public GTextField m_jiesuoxingji;
		public GGraph m_mengban;
		public GGraph m_XiangQingBtn;
		public GImage m_suo;

		public const string URL = "ui://rn5o3g4tfzr62";

		public static UI_JiNengListItem CreateInstance()
		{
			return (UI_JiNengListItem)UIPackage.CreateObject("UI_PetParticulars","JiNengListItem");
		}

		public UI_JiNengListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_icon = (GLoader)this.GetChildAt(2);
			m_name = (GTextField)this.GetChildAt(3);
			m_jiesuoxingji = (GTextField)this.GetChildAt(4);
			m_mengban = (GGraph)this.GetChildAt(5);
			m_XiangQingBtn = (GGraph)this.GetChildAt(6);
			m_suo = (GImage)this.GetChildAt(7);
		}
	}
}