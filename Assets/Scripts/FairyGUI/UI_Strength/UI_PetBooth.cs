/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_PetBooth : GComponent
	{
		public GGraph m_modelHolder;
		public GTextField m_unlockTxt;
		public GGroup m_unlock;
		public GComponent m_starList;

		public const string URL = "ui://qnd9tp35w5481n";

		public static UI_PetBooth CreateInstance()
		{
			return (UI_PetBooth)UIPackage.CreateObject("UI_Strength","PetBooth");
		}

		public UI_PetBooth()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_modelHolder = (GGraph)this.GetChildAt(1);
			m_unlockTxt = (GTextField)this.GetChildAt(3);
			m_unlock = (GGroup)this.GetChildAt(4);
			m_starList = (GComponent)this.GetChildAt(5);
		}
	}
}