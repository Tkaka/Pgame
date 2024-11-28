/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_DaoJuListItem : GComponent
	{
		public GGraph m_MoXing;
		public GComponent m_XingJi;
		public GTextField m_Name;
		public GGraph m_XiangQing;

		public const string URL = "ui://zy7t2yeggkzx1r";

		public static UI_DaoJuListItem CreateInstance()
		{
			return (UI_DaoJuListItem)UIPackage.CreateObject("UI_DrawCard","DaoJuListItem");
		}

		public UI_DaoJuListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_MoXing = (GGraph)this.GetChildAt(1);
			m_XingJi = (GComponent)this.GetChildAt(2);
			m_Name = (GTextField)this.GetChildAt(4);
			m_XiangQing = (GGraph)this.GetChildAt(5);
		}
	}
}