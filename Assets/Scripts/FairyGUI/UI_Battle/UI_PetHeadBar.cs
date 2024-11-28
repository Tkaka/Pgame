/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_PetHeadBar : GComponent
	{
		public GLoader m_borderLoader;
		public GLoader m_iconLoader;
		public GList m_starList;
		public GLoader m_typeLoader;
		public GImage m_hpBar;
		public GComponent m_petBean;
		public GImage m_mpBar;
		public GTextField m_txtLevel;
		public GImage m_txtSkill;
		public GImage m_txtSpuerSkill;
		public GGraph m_numberPos;
		public GGraph m_headEftPos;
		public GGraph m_blueEftPos;
		public GGraph m_touchHolder;
		public GMovieClip m_smallSkillAni;

		public const string URL = "ui://028ppdzhicla3m";

		public static UI_PetHeadBar CreateInstance()
		{
			return (UI_PetHeadBar)UIPackage.CreateObject("UI_Battle","PetHeadBar");
		}

		public UI_PetHeadBar()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_borderLoader = (GLoader)this.GetChildAt(1);
			m_iconLoader = (GLoader)this.GetChildAt(3);
			m_starList = (GList)this.GetChildAt(4);
			m_typeLoader = (GLoader)this.GetChildAt(5);
			m_hpBar = (GImage)this.GetChildAt(6);
			m_petBean = (GComponent)this.GetChildAt(7);
			m_mpBar = (GImage)this.GetChildAt(8);
			m_txtLevel = (GTextField)this.GetChildAt(9);
			m_txtSkill = (GImage)this.GetChildAt(10);
			m_txtSpuerSkill = (GImage)this.GetChildAt(11);
			m_numberPos = (GGraph)this.GetChildAt(12);
			m_headEftPos = (GGraph)this.GetChildAt(13);
			m_blueEftPos = (GGraph)this.GetChildAt(14);
			m_touchHolder = (GGraph)this.GetChildAt(15);
			m_smallSkillAni = (GMovieClip)this.GetChildAt(16);
		}
	}
}