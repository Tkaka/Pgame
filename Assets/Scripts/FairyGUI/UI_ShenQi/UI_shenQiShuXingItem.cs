/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_ShenQi
{
	public partial class UI_shenQiShuXingItem : GComponent
	{
		public GTextField m_tipLabel;
		public GProgressBar m_prograssBar;
		public GTextField m_progressLabel;
		public GTextField m_changeTipLabel;

		public const string URL = "ui://bi2nkn43fd9ib";

		public static UI_shenQiShuXingItem CreateInstance()
		{
			return (UI_shenQiShuXingItem)UIPackage.CreateObject("UI_ShenQi","shenQiShuXingItem");
		}

		public UI_shenQiShuXingItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tipLabel = (GTextField)this.GetChildAt(0);
			m_prograssBar = (GProgressBar)this.GetChildAt(1);
			m_progressLabel = (GTextField)this.GetChildAt(2);
			m_changeTipLabel = (GTextField)this.GetChildAt(3);
		}
	}
}