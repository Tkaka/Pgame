/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_petModel : GComponent
	{
		public GLoader m_typeIcon;
		public GTextField m_nameLabel;
		public GList m_starList;
		public GGraph m_modelTocher;
		public GGraph m_modelHandler;

		public const string URL = "ui://8u3gv94nktoo1m";

		public static UI_petModel CreateInstance()
		{
			return (UI_petModel)UIPackage.CreateObject("UI_Equip","petModel");
		}

		public UI_petModel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_typeIcon = (GLoader)this.GetChildAt(0);
			m_nameLabel = (GTextField)this.GetChildAt(1);
			m_starList = (GList)this.GetChildAt(2);
			m_modelTocher = (GGraph)this.GetChildAt(4);
			m_modelHandler = (GGraph)this.GetChildAt(5);
		}
	}
}