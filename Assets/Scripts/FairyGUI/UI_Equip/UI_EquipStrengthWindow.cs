/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_EquipStrengthWindow : GComponent
	{
		public Controller m_equipCtril;
		public UI_equipToggleGroup m_equipToggleGroup;
		public UI_StrengthPanel m_strengthPanel;
		public UI_JueXing m_jueXingPanel;
		public GGraph m_modelPos;
		public GGraph m_modelToucher;
		public GLoader m_petTypeLoader;
		public GTextField m_petNameLabel;
		public GComponent m_weaponItem;
		public GComponent m_clothItem;
		public GComponent m_kuZiItem;
		public GComponent m_shoesItem;
		public GComponent m_huiZhanItem;
		public GComponent m_miJiItem;
		public GList m_petList;
		public GButton m_switchLeftBtn;
		public GButton m_switchRightBtn;
		public GComponent m_comomTop;
		public GGraph m_mask;

		public const string URL = "ui://8u3gv94nt5fa0";

		public static UI_EquipStrengthWindow CreateInstance()
		{
			return (UI_EquipStrengthWindow)UIPackage.CreateObject("UI_Equip","EquipStrengthWindow");
		}

		public UI_EquipStrengthWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_equipCtril = this.GetControllerAt(0);
			m_equipToggleGroup = (UI_equipToggleGroup)this.GetChildAt(1);
			m_strengthPanel = (UI_StrengthPanel)this.GetChildAt(3);
			m_jueXingPanel = (UI_JueXing)this.GetChildAt(4);
			m_modelPos = (GGraph)this.GetChildAt(7);
			m_modelToucher = (GGraph)this.GetChildAt(8);
			m_petTypeLoader = (GLoader)this.GetChildAt(10);
			m_petNameLabel = (GTextField)this.GetChildAt(11);
			m_weaponItem = (GComponent)this.GetChildAt(12);
			m_clothItem = (GComponent)this.GetChildAt(13);
			m_kuZiItem = (GComponent)this.GetChildAt(14);
			m_shoesItem = (GComponent)this.GetChildAt(15);
			m_huiZhanItem = (GComponent)this.GetChildAt(16);
			m_miJiItem = (GComponent)this.GetChildAt(17);
			m_petList = (GList)this.GetChildAt(19);
			m_switchLeftBtn = (GButton)this.GetChildAt(20);
			m_switchRightBtn = (GButton)this.GetChildAt(21);
			m_comomTop = (GComponent)this.GetChildAt(23);
			m_mask = (GGraph)this.GetChildAt(24);
		}
	}
}