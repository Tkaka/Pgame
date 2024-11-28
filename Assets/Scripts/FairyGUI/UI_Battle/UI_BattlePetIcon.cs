/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_BattlePetIcon : GComponent
	{
		public GLoader m_borderLoder;
		public GLoader m_headLoader;
		public GComponent m_starList;
		public GTextField m_lvTxt;

		public const string URL = "ui://028ppdzhjkpz6p";

		public static UI_BattlePetIcon CreateInstance()
		{
			return (UI_BattlePetIcon)UIPackage.CreateObject("UI_Battle","BattlePetIcon");
		}

		public UI_BattlePetIcon()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_borderLoder = (GLoader)this.GetChildAt(0);
			m_headLoader = (GLoader)this.GetChildAt(1);
			m_starList = (GComponent)this.GetChildAt(2);
			m_lvTxt = (GTextField)this.GetChildAt(3);
		}
	}
}