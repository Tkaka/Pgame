/**
 * Auto generated, do not edit it
 *
 * t_open_fight
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_open_fightBean : BaseBin
	{
				private int m_t_id; // 子项目ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 子项目ID 
				public string t_fight {get; set;}   // 战斗力达到多少子项
				public string t_rank {get; set;}   // 战斗力排行子项
				private int m_t_level; // 开启等级 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 开启等级 
				private int m_t_pet; // 活动宠物id（宠物表） 
				public int t_pet{get{return m_t_pet;} set{m_t_pet = value;}} // 活动宠物id（宠物表） 
				private string m_t_desc;   // 活动描述
				public string t_desc
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_desc, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_desc;
                        }
                        else
                            return m_t_desc;
                    }
                    set { m_t_desc = value; }
                } 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_fight = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_rank = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_level = XBuffer.ReadInt(data, ref offset);
					m_t_pet = XBuffer.ReadInt(data, ref offset);
					t_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
		} 

	}
}


