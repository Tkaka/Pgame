/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_ShenQi
{
	public partial class UI_unlockCoditionItem : GComponent
	{
		public GTextField m_conditionLabel;
		public GImage m_reachIcon;
		public GTextField m_progressLabel;
		public GImage m_colorIcon;
		public GImage m_lockIcon;

		public const string URL = "ui://bi2nkn43fd9is";

		public static UI_unlockCoditionItem CreateInstance()
		{
			return (UI_unlockCoditionItem)UIPackage.CreateObject("UI_ShenQi","unlockCoditionItem");
		}

		public UI_unlockCoditionItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_conditionLabel = (GTextField)this.GetChildAt(1);
			m_reachIcon = (GImage)this.GetChildAt(2);
			m_progressLabel = (GTextField)this.GetChildAt(3);
			m_colorIcon = (GImage)this.GetChildAt(4);
			m_lockIcon = (GImage)this.GetChildAt(5);
		}
	}
}