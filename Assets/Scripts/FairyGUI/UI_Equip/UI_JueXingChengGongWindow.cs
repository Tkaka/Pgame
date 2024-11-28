/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_JueXingChengGongWindow : GComponent
	{
		public GGraph m_BeiJing;
		public UI_ChengGongXianSHiIcon m_OldIcon;
		public UI_ChengGongXianSHiIcon m_NewIcon;
		public GList m_ShuXingList;
		public GComponent m_leftStarAnim;
		public GComponent m_rightStarAnim;
		public Transition m_anim;

		public const string URL = "ui://8u3gv94nlzc519";

		public static UI_JueXingChengGongWindow CreateInstance()
		{
			return (UI_JueXingChengGongWindow)UIPackage.CreateObject("UI_Equip","JueXingChengGongWindow");
		}

		public UI_JueXingChengGongWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GGraph)this.GetChildAt(0);
			m_OldIcon = (UI_ChengGongXianSHiIcon)this.GetChildAt(4);
			m_NewIcon = (UI_ChengGongXianSHiIcon)this.GetChildAt(5);
			m_ShuXingList = (GList)this.GetChildAt(6);
			m_leftStarAnim = (GComponent)this.GetChildAt(9);
			m_rightStarAnim = (GComponent)this.GetChildAt(10);
			m_anim = this.GetTransitionAt(0);
		}
	}
}