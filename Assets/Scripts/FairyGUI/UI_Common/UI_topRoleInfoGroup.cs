/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_topRoleInfoGroup : GComponent
	{
		public GGraph m_goldToucher;
		public GGraph m_damondToucher;
		public GGraph m_energyToucher;
		public GTextField m_goldTxt;
		public GTextField m_damondTxt;
		public GRichTextField m_energyTxt;
		public GGroup m_tiliGroup;
		public GLoader m_commonIconLoader;
		public GTextField m_commonText;
		public GGroup m_commonGroup;
		public Transition m_anim;

		public const string URL = "ui://42sthz3tqmtxxrn";

		public static UI_topRoleInfoGroup CreateInstance()
		{
			return (UI_topRoleInfoGroup)UIPackage.CreateObject("UI_Common","topRoleInfoGroup");
		}

		public UI_topRoleInfoGroup()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_goldToucher = (GGraph)this.GetChildAt(0);
			m_damondToucher = (GGraph)this.GetChildAt(1);
			m_energyToucher = (GGraph)this.GetChildAt(2);
			m_goldTxt = (GTextField)this.GetChildAt(6);
			m_damondTxt = (GTextField)this.GetChildAt(7);
			m_energyTxt = (GRichTextField)this.GetChildAt(14);
			m_tiliGroup = (GGroup)this.GetChildAt(15);
			m_commonIconLoader = (GLoader)this.GetChildAt(16);
			m_commonText = (GTextField)this.GetChildAt(17);
			m_commonGroup = (GGroup)this.GetChildAt(18);
			m_anim = this.GetTransitionAt(0);
		}
	}
}