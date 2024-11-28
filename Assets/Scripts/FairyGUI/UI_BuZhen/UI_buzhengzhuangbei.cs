/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BuZhen
{
	public partial class UI_buzhengzhuangbei : GComponent
	{
		public GImage m_boardLoader;
		public GTextField m_levelLabel;
		public GComponent m_starList;

		public const string URL = "ui://z0csav43rvac2n";

		public static UI_buzhengzhuangbei CreateInstance()
		{
			return (UI_buzhengzhuangbei)UIPackage.CreateObject("UI_BuZhen","buzhengzhuangbei");
		}

		public UI_buzhengzhuangbei()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GImage)this.GetChildAt(0);
			m_levelLabel = (GTextField)this.GetChildAt(1);
			m_starList = (GComponent)this.GetChildAt(2);
		}
	}
}