/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_propItem : GComponent
	{
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public GTextField m_numText;
		public GLoader m_fragmentLoader;
		public GTextField m_nameLabel;

		public const string URL = "ui://1wdkrtiuw0hup";

		public static UI_propItem CreateInstance()
		{
			return (UI_propItem)UIPackage.CreateObject("UI_UltemateTrain","propItem");
		}

		public UI_propItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_numText = (GTextField)this.GetChildAt(2);
			m_fragmentLoader = (GLoader)this.GetChildAt(3);
			m_nameLabel = (GTextField)this.GetChildAt(4);
		}
	}
}