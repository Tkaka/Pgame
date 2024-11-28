/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_EquipSPSuccessWindow : GComponent
	{
		public GGraph m_mask;
		public GLoader m_oldBoarderLoader;
		public GLoader m_oldIconLoader;
		public GTextField m_oldLvLabel;
		public GTextField m_oldNameLabel;
		public GLoader m_newBoarderLoader;
		public GLoader m_newIconLoader;
		public GTextField m_newLvLabel;
		public GTextField m_newNameLabel;
		public UI_attributeChangeList m_attributeChangeList;
		public GComponent m_oldStarList;
		public GComponent m_newStarList;
		public GComponent m_oldQualityDou;
		public GComponent m_newQulaityDou;
		public GComponent m_leftStarAnim;
		public GComponent m_rightStarAnim;

		public const string URL = "ui://8u3gv94nd5g914";

		public static UI_EquipSPSuccessWindow CreateInstance()
		{
			return (UI_EquipSPSuccessWindow)UIPackage.CreateObject("UI_Equip","EquipSPSuccessWindow");
		}

		public UI_EquipSPSuccessWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_oldBoarderLoader = (GLoader)this.GetChildAt(3);
			m_oldIconLoader = (GLoader)this.GetChildAt(4);
			m_oldLvLabel = (GTextField)this.GetChildAt(5);
			m_oldNameLabel = (GTextField)this.GetChildAt(6);
			m_newBoarderLoader = (GLoader)this.GetChildAt(8);
			m_newIconLoader = (GLoader)this.GetChildAt(9);
			m_newLvLabel = (GTextField)this.GetChildAt(10);
			m_newNameLabel = (GTextField)this.GetChildAt(11);
			m_attributeChangeList = (UI_attributeChangeList)this.GetChildAt(12);
			m_oldStarList = (GComponent)this.GetChildAt(13);
			m_newStarList = (GComponent)this.GetChildAt(14);
			m_oldQualityDou = (GComponent)this.GetChildAt(15);
			m_newQulaityDou = (GComponent)this.GetChildAt(16);
			m_leftStarAnim = (GComponent)this.GetChildAt(17);
			m_rightStarAnim = (GComponent)this.GetChildAt(18);
		}
	}
}