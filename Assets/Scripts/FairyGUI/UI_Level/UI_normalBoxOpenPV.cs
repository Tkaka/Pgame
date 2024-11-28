/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_normalBoxOpenPV : GComponent
	{
		public GTextField m_tipLabel;
		public GTextField m_nameLabel;
		public GList m_itemList;
		public GButton m_receiveBtn;
		public GButton m_cancelBtn;
		public GTextField m_receiveTipLabel;
		public GTextField m_starNum;
		public GGroup m_starGroup;
		public GButton m_closeBtn;

		public const string URL = "ui://z04ymz0ew50m25m";

		public static UI_normalBoxOpenPV CreateInstance()
		{
			return (UI_normalBoxOpenPV)UIPackage.CreateObject("UI_Level","normalBoxOpenPV");
		}

		public UI_normalBoxOpenPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tipLabel = (GTextField)this.GetChildAt(6);
			m_nameLabel = (GTextField)this.GetChildAt(7);
			m_itemList = (GList)this.GetChildAt(8);
			m_receiveBtn = (GButton)this.GetChildAt(9);
			m_cancelBtn = (GButton)this.GetChildAt(10);
			m_receiveTipLabel = (GTextField)this.GetChildAt(11);
			m_starNum = (GTextField)this.GetChildAt(13);
			m_starGroup = (GGroup)this.GetChildAt(14);
			m_closeBtn = (GButton)this.GetChildAt(15);
		}
	}
}