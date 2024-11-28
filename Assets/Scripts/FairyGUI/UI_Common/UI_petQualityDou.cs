/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_petQualityDou : GComponent
	{
		public GLoader m_leftDou;
		public GLoader m_rightDou;
		public GList m_centerDouList;

		public const string URL = "ui://42sthz3ty63cxqh";

		public static UI_petQualityDou CreateInstance()
		{
			return (UI_petQualityDou)UIPackage.CreateObject("UI_Common","petQualityDou");
		}

		public UI_petQualityDou()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_leftDou = (GLoader)this.GetChildAt(0);
			m_rightDou = (GLoader)this.GetChildAt(1);
			m_centerDouList = (GList)this.GetChildAt(2);
		}
	}
}