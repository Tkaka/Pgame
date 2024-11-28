/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_PetHead : GComponent
	{
		public GLoader m_borderLoader;
		public GLoader m_iconLoader;
		public GTextField m_petName;
		public UI_StarList m_starList;
		public UI_petQualityDou m_petQualityDou;

		public const string URL = "ui://42sthz3tn1c0jd";

		public static UI_PetHead CreateInstance()
		{
			return (UI_PetHead)UIPackage.CreateObject("UI_Common","PetHead");
		}

		public UI_PetHead()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_borderLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_petName = (GTextField)this.GetChildAt(2);
			m_starList = (UI_StarList)this.GetChildAt(3);
			m_petQualityDou = (UI_petQualityDou)this.GetChildAt(4);
		}
	}
}