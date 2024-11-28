/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BuZhen
{
	public partial class UI_BuZhenHolder : GComponent
	{
		public GGraph m_toucher;
		public GGraph m_effrectHolder;
		public GGraph m_holder;

		public const string URL = "ui://z0csav43821n12";

		public static UI_BuZhenHolder CreateInstance()
		{
			return (UI_BuZhenHolder)UIPackage.CreateObject("UI_BuZhen","BuZhenHolder");
		}

		public UI_BuZhenHolder()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_toucher = (GGraph)this.GetChildAt(0);
			m_effrectHolder = (GGraph)this.GetChildAt(1);
			m_holder = (GGraph)this.GetChildAt(2);
		}
	}
}