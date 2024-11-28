/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_ZHDiamondStrengthPV : GComponent
	{
		public GButton m_confirmBtn;
		public GTextField m_zhanHunName;
		public GTextField m_levelLabel;
		public GTextField m_diamondNum;

		public const string URL = "ui://qnd9tp35lxvz4p";

		public static UI_ZHDiamondStrengthPV CreateInstance()
		{
			return (UI_ZHDiamondStrengthPV)UIPackage.CreateObject("UI_Strength","ZHDiamondStrengthPV");
		}

		public UI_ZHDiamondStrengthPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_confirmBtn = (GButton)this.GetChildAt(5);
			m_zhanHunName = (GTextField)this.GetChildAt(8);
			m_levelLabel = (GTextField)this.GetChildAt(10);
			m_diamondNum = (GTextField)this.GetChildAt(13);
		}
	}
}