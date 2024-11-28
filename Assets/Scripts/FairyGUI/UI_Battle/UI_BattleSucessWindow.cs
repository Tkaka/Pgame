/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattleSucessWindow : GComponent
	{
		public GComponent m_imgBg;
		public GTextField m_lvTxt;
		public GTextField m_exp;
		public GTextField m_gold;
		public GList m_itemList;
		public GTextField m_objTitle;
		public GList m_desList;

		public const string URL = "ui://028ppdzhq2pd2";

		public static UI_BattleSucessWindow CreateInstance()
		{
			return (UI_BattleSucessWindow)UIPackage.CreateObject("UI_Battle","BattleSucessWindow");
		}

		public UI_BattleSucessWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GComponent)this.GetChildAt(1);
			m_lvTxt = (GTextField)this.GetChildAt(4);
			m_exp = (GTextField)this.GetChildAt(6);
			m_gold = (GTextField)this.GetChildAt(8);
			m_itemList = (GList)this.GetChildAt(11);
			m_objTitle = (GTextField)this.GetChildAt(12);
			m_desList = (GList)this.GetChildAt(13);
		}
	}
}