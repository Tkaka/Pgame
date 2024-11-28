/**
 * Auto generated, do not edit it
 *
 * t_guild_boss
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_guild_bossBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				private int m_t_pet; // bossID 
				public int t_pet{get{return m_t_pet;} set{m_t_pet = value;}} // bossID 
				private int m_t_drop; // 每次战斗掉落id(掉落表掉落ID） 
				public int t_drop{get{return m_t_drop;} set{m_t_drop = value;}} // 每次战斗掉落id(掉落表掉落ID） 
				public string t_first_award {get; set;}   // 首通奖励（道具ID+道具数量;）
				public string t_month_award {get; set;}   // 月击败奖励(道具ID+道具数量;)
				private string m_t_special_id;   // boss特性介绍语言ID
				public string t_special_id
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_special_id, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_special_id;
                        }
                        else
                            return m_t_special_id;
                    }
                    set { m_t_special_id = value; }
                } 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_pet = XBuffer.ReadInt(data, ref offset);
					m_t_drop = XBuffer.ReadInt(data, ref offset);
					t_first_award = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_month_award = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_special_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
		} 

	}
}


