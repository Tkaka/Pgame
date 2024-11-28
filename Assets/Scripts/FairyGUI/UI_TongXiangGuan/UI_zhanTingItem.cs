/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TongXiangGuan
{
	public partial class UI_zhanTingItem : GComponent
	{
		public GTextField m_nameLabel;
		public GLoader m_typeLoader;
		public GTextField m_numLabel;
		public GList m_tongXiangList;
		public GImage m_redPoint;
		public GTextField m_lvLimitLabel;
		public GGroup m_lockGroup;
		public GGraph m_toucher;

		public const string URL = "ui://ansp6fm5lni7f";

		public static UI_zhanTingItem CreateInstance()
		{
			return (UI_zhanTingItem)UIPackage.CreateObject("UI_TongXiangGuan","zhanTingItem");
		}

		public UI_zhanTingItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_nameLabel = (GTextField)this.GetChildAt(1);
			m_typeLoader = (GLoader)this.GetChildAt(2);
			m_numLabel = (GTextField)this.GetChildAt(3);
			m_tongXiangList = (GList)this.GetChildAt(5);
			m_redPoint = (GImage)this.GetChildAt(6);
			m_lvLimitLabel = (GTextField)this.GetChildAt(9);
			m_lockGroup = (GGroup)this.GetChildAt(10);
			m_toucher = (GGraph)this.GetChildAt(11);
		}
	}
}