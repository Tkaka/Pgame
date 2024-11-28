/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_JueSeShuXing : GComponent
	{
		public GLoader m_BeiJing;
		public GLoader m_TouXiang;
		public GTextField m_Name;
		public GComponent m_StartList;
		public GTextField m_Level;

		public const string URL = "ui://8u3gv94nt5fai";

		public static UI_JueSeShuXing CreateInstance()
		{
			return (UI_JueSeShuXing)UIPackage.CreateObject("UI_Equip","JueSeShuXing");
		}

		public UI_JueSeShuXing()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GLoader)this.GetChildAt(0);
			m_TouXiang = (GLoader)this.GetChildAt(1);
			m_Name = (GTextField)this.GetChildAt(2);
			m_StartList = (GComponent)this.GetChildAt(3);
			m_Level = (GTextField)this.GetChildAt(4);
		}
	}
}