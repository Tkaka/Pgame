/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_JinHuaLian
{
	public partial class UI_JinHuaDiZuo : GComponent
	{
		public GImage m_DiZuo;
		public GGraph m_model;
		public GTextField m_JieSuoTiaoJian;
		public GGroup m_WeiJieSuo;
		public GTextField m_name;
		public GGroup m_YiJieSuo;
		public GGroup m_all;
		public GGraph m_dianjixiangying;

		public const string URL = "ui://n8vdy261whks6";

		public static UI_JinHuaDiZuo CreateInstance()
		{
			return (UI_JinHuaDiZuo)UIPackage.CreateObject("UI_JinHuaLian","JinHuaDiZuo");
		}

		public UI_JinHuaDiZuo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_DiZuo = (GImage)this.GetChildAt(0);
			m_model = (GGraph)this.GetChildAt(1);
			m_JieSuoTiaoJian = (GTextField)this.GetChildAt(4);
			m_WeiJieSuo = (GGroup)this.GetChildAt(5);
			m_name = (GTextField)this.GetChildAt(7);
			m_YiJieSuo = (GGroup)this.GetChildAt(8);
			m_all = (GGroup)this.GetChildAt(9);
			m_dianjixiangying = (GGraph)this.GetChildAt(10);
		}
	}
}