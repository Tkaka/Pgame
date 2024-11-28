/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_JiNengDianGouMaiWindow : GComponent
	{
		public GGraph m_BeiJing;
		public UI_jiNengDianBuyPV m_popupView;

		public const string URL = "ui://qnd9tp35cbtn34";

		public static UI_JiNengDianGouMaiWindow CreateInstance()
		{
			return (UI_JiNengDianGouMaiWindow)UIPackage.CreateObject("UI_Strength","JiNengDianGouMaiWindow");
		}

		public UI_JiNengDianGouMaiWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_jiNengDianBuyPV)this.GetChildAt(1);
		}
	}
}