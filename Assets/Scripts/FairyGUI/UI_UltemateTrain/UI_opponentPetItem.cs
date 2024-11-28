/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_opponentPetItem : GComponent
	{
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public GComponent m_starList;
		public GTextField m_levelLabel;

		public const string URL = "ui://1wdkrtiusi7j1x";

		public static UI_opponentPetItem CreateInstance()
		{
			return (UI_opponentPetItem)UIPackage.CreateObject("UI_UltemateTrain","opponentPetItem");
		}

		public UI_opponentPetItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_starList = (GComponent)this.GetChildAt(2);
			m_levelLabel = (GTextField)this.GetChildAt(3);
		}
	}
}