/**
 * Auto generated, do not edit it
 *
 * t_pet_colorup_cost
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_pet_colorup_costBean : BaseBin
	{
				private int m_t_id; // 品阶*1000+角色类型*100+资质（1111代表白色攻击11资质） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 品阶*1000+角色类型*100+资质（1111代表白色攻击11资质） 
				private int m_t_drug_id; // 升下品药剂id 
				public int t_drug_id{get{return m_t_drug_id;} set{m_t_drug_id = value;}} // 升下品药剂id 
				private int m_t_drug_num; // 升下品药剂数量 
				public int t_drug_num{get{return m_t_drug_num;} set{m_t_drug_num = value;}} // 升下品药剂数量 
				private int m_t_main_id; // 升下品主宝石id 
				public int t_main_id{get{return m_t_main_id;} set{m_t_main_id = value;}} // 升下品主宝石id 
				private int m_t_main_num; // 升下品主宝石数量 
				public int t_main_num{get{return m_t_main_num;} set{m_t_main_num = value;}} // 升下品主宝石数量 
				private int m_t_vice_id1; // 升下品副宝石id1 
				public int t_vice_id1{get{return m_t_vice_id1;} set{m_t_vice_id1 = value;}} // 升下品副宝石id1 
				private int m_t_vice_id2; // 升下品副宝石id2 
				public int t_vice_id2{get{return m_t_vice_id2;} set{m_t_vice_id2 = value;}} // 升下品副宝石id2 
				private int m_t_vice_num; // 升下品副宝石数量 
				public int t_vice_num{get{return m_t_vice_num;} set{m_t_vice_num = value;}} // 升下品副宝石数量 
				private int m_t_gold; // 升下品消耗金币 
				public int t_gold{get{return m_t_gold;} set{m_t_gold = value;}} // 升下品消耗金币 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_drug_id = XBuffer.ReadInt(data, ref offset);
					m_t_drug_num = XBuffer.ReadInt(data, ref offset);
					m_t_main_id = XBuffer.ReadInt(data, ref offset);
					m_t_main_num = XBuffer.ReadInt(data, ref offset);
					m_t_vice_id1 = XBuffer.ReadInt(data, ref offset);
					m_t_vice_id2 = XBuffer.ReadInt(data, ref offset);
					m_t_vice_num = XBuffer.ReadInt(data, ref offset);
					m_t_gold = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


