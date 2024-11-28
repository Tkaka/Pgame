/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_ShengPingSuccessWindow : GComponent
	{
		public GGraph m_mask;
		public GTextField m_oldZhanDouLiLabel;
		public GTextField m_newZhanDouLiLabel;
		public GTextField m_oldXianShouZhiLabel;
		public GTextField m_newXianShouZhiLabel;
		public GLoader m_oldBorderLoader;
		public GLoader m_newBorderLoader;
		public GLoader m_newIconLoader;
		public GLoader m_oldIconLoader;
		public GTextField m_oldNameLabel;
		public GTextField m_newNameLabel;
		public GComponent m_oldPetQualityDou;
		public GComponent m_newPetQualityDou;
		public GComponent m_leftStarAnim;
		public GComponent m_rightStarAnim;
		public Transition m_anim;

		public const string URL = "ui://qnd9tp35vvs01e";

		public static UI_ShengPingSuccessWindow CreateInstance()
		{
			return (UI_ShengPingSuccessWindow)UIPackage.CreateObject("UI_Strength","ShengPingSuccessWindow");
		}

		public UI_ShengPingSuccessWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_oldZhanDouLiLabel = (GTextField)this.GetChildAt(5);
			m_newZhanDouLiLabel = (GTextField)this.GetChildAt(6);
			m_oldXianShouZhiLabel = (GTextField)this.GetChildAt(7);
			m_newXianShouZhiLabel = (GTextField)this.GetChildAt(8);
			m_oldBorderLoader = (GLoader)this.GetChildAt(11);
			m_newBorderLoader = (GLoader)this.GetChildAt(12);
			m_newIconLoader = (GLoader)this.GetChildAt(13);
			m_oldIconLoader = (GLoader)this.GetChildAt(14);
			m_oldNameLabel = (GTextField)this.GetChildAt(15);
			m_newNameLabel = (GTextField)this.GetChildAt(16);
			m_oldPetQualityDou = (GComponent)this.GetChildAt(20);
			m_newPetQualityDou = (GComponent)this.GetChildAt(21);
			m_leftStarAnim = (GComponent)this.GetChildAt(22);
			m_rightStarAnim = (GComponent)this.GetChildAt(23);
			m_anim = this.GetTransitionAt(0);
		}
	}
}