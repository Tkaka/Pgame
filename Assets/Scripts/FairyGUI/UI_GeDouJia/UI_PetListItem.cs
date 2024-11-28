/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_PetListItem : GComponent
	{
		public GLoader m_iconBorder;
		public GLoader m_headLoader;
		public GButton m_sourceBtn;
		public GProgressBar m_fragProgress;
		public GTextField m_ShuZhi;
		public GButton m_CompoundBtn;
		public GGroup m_notGetGroup;
		public GComponent m_startlist;
		public GLoader m_typeLoader;
		public GTextField m_petName;
		public GComponent m_PinJieDian;
		public GGroup m_isEquip;
		public GButton m_strengthenBtn;
		public UI_EquipBtn m_equipBtn;
		public GTextField m_fightPower;
		public GGroup m_getGroup;
		public GTextField m_Level;
		public GGroup m_levelGroup;

		public const string URL = "ui://4asqm7awe4jb5b";

		public static UI_PetListItem CreateInstance()
		{
			return (UI_PetListItem)UIPackage.CreateObject("UI_GeDouJia","PetListItem");
		}

		public UI_PetListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_iconBorder = (GLoader)this.GetChildAt(1);
			m_headLoader = (GLoader)this.GetChildAt(2);
			m_sourceBtn = (GButton)this.GetChildAt(3);
			m_fragProgress = (GProgressBar)this.GetChildAt(4);
			m_ShuZhi = (GTextField)this.GetChildAt(5);
			m_CompoundBtn = (GButton)this.GetChildAt(6);
			m_notGetGroup = (GGroup)this.GetChildAt(7);
			m_startlist = (GComponent)this.GetChildAt(8);
			m_typeLoader = (GLoader)this.GetChildAt(9);
			m_petName = (GTextField)this.GetChildAt(10);
			m_PinJieDian = (GComponent)this.GetChildAt(11);
			m_isEquip = (GGroup)this.GetChildAt(14);
			m_strengthenBtn = (GButton)this.GetChildAt(15);
			m_equipBtn = (UI_EquipBtn)this.GetChildAt(16);
			m_fightPower = (GTextField)this.GetChildAt(18);
			m_getGroup = (GGroup)this.GetChildAt(19);
			m_Level = (GTextField)this.GetChildAt(21);
			m_levelGroup = (GGroup)this.GetChildAt(22);
		}
	}
}