/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TongXiangGuan
{
	public partial class UI_tongXiangMaterialBtn : GButton
	{
		public GButton m_btn;
		public GTextField m_materialNameLabel;
		public GTextField m_lockLabel;
		public GGroup m_lockGroup;

		public const string URL = "ui://ansp6fm5z1k6u";

		public static UI_tongXiangMaterialBtn CreateInstance()
		{
			return (UI_tongXiangMaterialBtn)UIPackage.CreateObject("UI_TongXiangGuan","tongXiangMaterialBtn");
		}

		public UI_tongXiangMaterialBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btn = (GButton)this.GetChildAt(0);
			m_materialNameLabel = (GTextField)this.GetChildAt(1);
			m_lockLabel = (GTextField)this.GetChildAt(2);
			m_lockGroup = (GGroup)this.GetChildAt(4);
		}
	}
}