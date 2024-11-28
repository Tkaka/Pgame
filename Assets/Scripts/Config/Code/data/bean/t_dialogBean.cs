/**
 * Auto generated, do not edit it
 *
 * t_dialog
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_dialogBean : BaseBin
	{
				private int m_t_id; // 对白id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 对白id 
				private string m_npc_name;   // 名字
				public string npc_name
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_npc_name, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_npc_name;
                        }
                        else
                            return m_npc_name;
                    }
                    set { m_npc_name = value; }
                } 
				private string m_npc_content;   // 对白内容
				public string npc_content
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_npc_content, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_npc_content;
                        }
                        else
                            return m_npc_content;
                    }
                    set { m_npc_content = value; }
                } 
				private int m_t_left_right; // 1=左边 2 = 右边 
				public int t_left_right{get{return m_t_left_right;} set{m_t_left_right = value;}} // 1=左边 2 = 右边 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					npc_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					npc_content = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_left_right = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


