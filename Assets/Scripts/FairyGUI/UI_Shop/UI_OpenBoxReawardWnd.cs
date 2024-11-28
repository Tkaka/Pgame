/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_OpenBoxReawardWnd : GComponent
	{
		public GList m_rewardList;
		public GTextField m_txtTitle;
		public GTextField m_ShengYu;
		public GComponent m_btnOpen1;
		public GComponent m_btnOpen10;
		public GComponent m_btnOk;
		public GLoader m_imgComsume;
		public GTextField m_txtNum;
		public GGroup m_coinGroup;
		public GGroup m_objGroup;

		public const string URL = "ui://w9mypx6jiy9s10";

		public static UI_OpenBoxReawardWnd CreateInstance()
		{
			return (UI_OpenBoxReawardWnd)UIPackage.CreateObject("UI_Shop","OpenBoxReawardWnd");
		}

		public UI_OpenBoxReawardWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rewardList = (GList)this.GetChildAt(1);
			m_txtTitle = (GTextField)this.GetChildAt(2);
			m_ShengYu = (GTextField)this.GetChildAt(3);
			m_btnOpen1 = (GComponent)this.GetChildAt(4);
			m_btnOpen10 = (GComponent)this.GetChildAt(5);
			m_btnOk = (GComponent)this.GetChildAt(6);
			m_imgComsume = (GLoader)this.GetChildAt(8);
			m_txtNum = (GTextField)this.GetChildAt(9);
			m_coinGroup = (GGroup)this.GetChildAt(10);
			m_objGroup = (GGroup)this.GetChildAt(11);
		}
	}
}