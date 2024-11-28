/**
 * Auto generated, do not edit it
 *
 * t_talent_page
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_talent_pageBean : BaseBin
	{
				private int m_t_id; // 天赋页id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 天赋页id 
				public string t_title2 {get; set;}   // 天赋title2
				public string t_index {get; set;}   // 天赋显示顺序(横着来 全局表id+天赋id+天赋id;全局表id+天赋id+天赋id;)
				private int m_t_level; // 开启战队条件 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 开启战队条件 
				private int m_t_befor_talent_page; // 前置天赋页id 
				public int t_befor_talent_page{get{return m_t_befor_talent_page;} set{m_t_befor_talent_page = value;}} // 前置天赋页id 
				private int m_t_befor_point; // 前置消耗点数 
				public int t_befor_point{get{return m_t_befor_point;} set{m_t_befor_point = value;}} // 前置消耗点数 
				private string m_t_des;   // 天赋页描述
				public string t_des
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_des, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_des;
                        }
                        else
                            return m_t_des;
                    }
                    set { m_t_des = value; }
                } 
				public string t_title {get; set;}   // 天赋页标题
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_title2 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_index = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_level = XBuffer.ReadInt(data, ref offset);
					m_t_befor_talent_page = XBuffer.ReadInt(data, ref offset);
					m_t_befor_point = XBuffer.ReadInt(data, ref offset);
					t_des = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_title = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


