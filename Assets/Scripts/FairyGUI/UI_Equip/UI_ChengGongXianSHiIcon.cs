/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_ChengGongXianSHiIcon : GComponent
	{
		public GLoader m_BeiJing;
		public GLoader m_TouXiang;
		public GList m_PingJieList;
		public GTextField m_Name;
		public GComponent m_StartList;
		public GTextField m_Level;
		public GComponent m_qualityDou;

		public const string URL = "ui://8u3gv94nlzc51a";

		public static UI_ChengGongXianSHiIcon CreateInstance()
		{
			return (UI_ChengGongXianSHiIcon)UIPackage.CreateObject("UI_Equip","ChengGongXianSHiIcon");
		}

		public UI_ChengGongXianSHiIcon()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GLoader)this.GetChildAt(0);
			m_TouXiang = (GLoader)this.GetChildAt(1);
			m_PingJieList = (GList)this.GetChildAt(2);
			m_Name = (GTextField)this.GetChildAt(3);
			m_StartList = (GComponent)this.GetChildAt(4);
			m_Level = (GTextField)this.GetChildAt(5);
			m_qualityDou = (GComponent)this.GetChildAt(6);
		}
	}
}