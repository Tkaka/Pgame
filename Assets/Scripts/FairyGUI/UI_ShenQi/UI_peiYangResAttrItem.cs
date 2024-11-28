/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_ShenQi
{
	public partial class UI_peiYangResAttrItem : GComponent
	{
		public GTextField m_tipLabel;
		public GTextField m_downLabel;
		public GGroup m_downGroup;
		public GTextField m_upLabel;
		public GGroup m_upGroup;
		public GTextField m_noChangeLabel;

		public const string URL = "ui://bi2nkn43k0ngv";

		public static UI_peiYangResAttrItem CreateInstance()
		{
			return (UI_peiYangResAttrItem)UIPackage.CreateObject("UI_ShenQi","peiYangResAttrItem");
		}

		public UI_peiYangResAttrItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tipLabel = (GTextField)this.GetChildAt(0);
			m_downLabel = (GTextField)this.GetChildAt(2);
			m_downGroup = (GGroup)this.GetChildAt(3);
			m_upLabel = (GTextField)this.GetChildAt(5);
			m_upGroup = (GGroup)this.GetChildAt(6);
			m_noChangeLabel = (GTextField)this.GetChildAt(7);
		}
	}
}