/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_JueXing : GComponent
	{
		public GRichTextField m_SuMing;
		public UI_JiangXingBtn m_JiangXing;
		public GTextField m_SuMingName;
		public UI_JueSeShuXing m_OldRole;
		public UI_JueSeShuXing m_NewRole;
		public UI_JuXingCaiLiao m_ZhuanBeiHun;
		public UI_JuXingCaiLiao m_JuXingShi;
		public GButton m_JueXingBtn;
		public GList m_OldShuXingList;
		public GList m_NewShuXingList;
		public GGroup m_JueXing;
		public GList m_ManXingShuXngList;
		public GLoader m_Model;
		public GImage m_DiZuo;
		public GGroup m_ManXing;
		public GTextField m_sumingyijihuo;
		public GGroup m_SuMingYiJiHuo;
		public GTextField m_shuxingyijihuo;
		public GGroup m_ShuXingYiJiHuo;
		public GTextField m_sumingweijihuo;
		public GGroup m_SuMingWeiJiHuo;
		public GTextField m_shuxingweijihuo;
		public GGroup m_ShuXingWeiJiHuo;
		public Transition m_equipModelAnim;

		public const string URL = "ui://8u3gv94nt5faj";

		public static UI_JueXing CreateInstance()
		{
			return (UI_JueXing)UIPackage.CreateObject("UI_Equip","JueXing");
		}

		public UI_JueXing()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_SuMing = (GRichTextField)this.GetChildAt(1);
			m_JiangXing = (UI_JiangXingBtn)this.GetChildAt(2);
			m_SuMingName = (GTextField)this.GetChildAt(3);
			m_OldRole = (UI_JueSeShuXing)this.GetChildAt(4);
			m_NewRole = (UI_JueSeShuXing)this.GetChildAt(7);
			m_ZhuanBeiHun = (UI_JuXingCaiLiao)this.GetChildAt(8);
			m_JuXingShi = (UI_JuXingCaiLiao)this.GetChildAt(9);
			m_JueXingBtn = (GButton)this.GetChildAt(10);
			m_OldShuXingList = (GList)this.GetChildAt(11);
			m_NewShuXingList = (GList)this.GetChildAt(12);
			m_JueXing = (GGroup)this.GetChildAt(13);
			m_ManXingShuXngList = (GList)this.GetChildAt(15);
			m_Model = (GLoader)this.GetChildAt(16);
			m_DiZuo = (GImage)this.GetChildAt(17);
			m_ManXing = (GGroup)this.GetChildAt(18);
			m_sumingyijihuo = (GTextField)this.GetChildAt(21);
			m_SuMingYiJiHuo = (GGroup)this.GetChildAt(22);
			m_shuxingyijihuo = (GTextField)this.GetChildAt(24);
			m_ShuXingYiJiHuo = (GGroup)this.GetChildAt(25);
			m_sumingweijihuo = (GTextField)this.GetChildAt(27);
			m_SuMingWeiJiHuo = (GGroup)this.GetChildAt(28);
			m_shuxingweijihuo = (GTextField)this.GetChildAt(30);
			m_ShuXingWeiJiHuo = (GGroup)this.GetChildAt(31);
			m_equipModelAnim = this.GetTransitionAt(0);
		}
	}
}