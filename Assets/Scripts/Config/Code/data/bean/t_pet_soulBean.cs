/**
 * Auto generated, do not edit it
 *
 * t_pet_soul
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_pet_soulBean : BaseBin
	{
				private int m_t_id; // 101=恶虎之魂、102=黑虎之魂、103=虎狼之魂、104=金虎之魂、105=猛虎之魂、106=虎狼之魂、107=炎虎之魂、108=白虎之魂、109=岩虎之魂、01=蝮蛇之魂、202=灵蛇之魂、203=蛟蛇之魂、204=翠蛇之魂、205=岚蛇之魂、206=暴蛇之魂、301=野熊之魂、302=幻熊之魂、303=暴熊之魂、304=斗熊之魂、305=雷熊之魂、401=赤龟之魂、402=神龟之魂、403=玄武之魂、404=真武之魂、405=王霸之魂、501=本源之魂、601=真空之魂、1000=毒草之魂、1001=猎鹰之魂、1002=坚石之魂 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 101=恶虎之魂、102=黑虎之魂、103=虎狼之魂、104=金虎之魂、105=猛虎之魂、106=虎狼之魂、107=炎虎之魂、108=白虎之魂、109=岩虎之魂、01=蝮蛇之魂、202=灵蛇之魂、203=蛟蛇之魂、204=翠蛇之魂、205=岚蛇之魂、206=暴蛇之魂、301=野熊之魂、302=幻熊之魂、303=暴熊之魂、304=斗熊之魂、305=雷熊之魂、401=赤龟之魂、402=神龟之魂、403=玄武之魂、404=真武之魂、405=王霸之魂、501=本源之魂、601=真空之魂、1000=毒草之魂、1001=猎鹰之魂、1002=坚石之魂 
				private int m_t_type; // 战魂类型1=虎之魂、2=蛇之魂、3=熊之魂、4=龟之魂、5=本源之魂、6=真空之魂、10=毒草、11=猎鹰、12=坚石 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 战魂类型1=虎之魂、2=蛇之魂、3=熊之魂、4=龟之魂、5=本源之魂、6=真空之魂、10=毒草、11=猎鹰、12=坚石 
				private string m_t_nameLanguageID;   // 战魂名称 
				public string t_nameLanguageID
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_nameLanguageID, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_nameLanguageID;
                        }
                        else
                            return m_t_nameLanguageID;
                    }
                    set { m_t_nameLanguageID = value; }
                } 
				private string m_t_descriptID;   // 战魂描述
				public string t_descriptID
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_descriptID, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_descriptID;
                        }
                        else
                            return m_t_descriptID;
                    }
                    set { m_t_descriptID = value; }
                } 
				public string t_effect_ID {get; set;}   // 战魂效果ID
				private int m_t_initValue1; // 初始参数1 
				public int t_initValue1{get{return m_t_initValue1;} set{m_t_initValue1 = value;}} // 初始参数1 
				private int m_t_initValue2; // 初始参数2 
				public int t_initValue2{get{return m_t_initValue2;} set{m_t_initValue2 = value;}} // 初始参数2 
				private int m_t_initValue3; // 初始参数3 
				public int t_initValue3{get{return m_t_initValue3;} set{m_t_initValue3 = value;}} // 初始参数3 
				private int m_t_initValue4; // 初始参数4 
				public int t_initValue4{get{return m_t_initValue4;} set{m_t_initValue4 = value;}} // 初始参数4 
				private int m_t_gropValue1; // 参数1等级成长加成 
				public int t_gropValue1{get{return m_t_gropValue1;} set{m_t_gropValue1 = value;}} // 参数1等级成长加成 
				private int m_t_groupValue2; // 参数2等级成长加成 
				public int t_groupValue2{get{return m_t_groupValue2;} set{m_t_groupValue2 = value;}} // 参数2等级成长加成 
				private int m_t_groupValue3; // 参数3等级成长加成 
				public int t_groupValue3{get{return m_t_groupValue3;} set{m_t_groupValue3 = value;}} // 参数3等级成长加成 
				private int m_t_groupValue4; // 参数4等级成长加成 
				public int t_groupValue4{get{return m_t_groupValue4;} set{m_t_groupValue4 = value;}} // 参数4等级成长加成 
				public string t_icon {get; set;}   // 图标
				private int m_t_exp_type; // 宠物战魂升级类别1-24 
				public int t_exp_type{get{return m_t_exp_type;} set{m_t_exp_type = value;}} // 宠物战魂升级类别1-24 
				public string t_condition {get; set;}   // 0=全部1=竞技场2=拳皇争霸3=社团战；分号相隔
				public string t_attr {get; set;}   // 属性初始值1（属性ID+属性算法类型[2=值相加3=百分比缩放]+属性值）（所有属性已乘以10000）
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					t_nameLanguageID = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_descriptID = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_effect_ID = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_initValue1 = XBuffer.ReadInt(data, ref offset);
					m_t_initValue2 = XBuffer.ReadInt(data, ref offset);
					m_t_initValue3 = XBuffer.ReadInt(data, ref offset);
					m_t_initValue4 = XBuffer.ReadInt(data, ref offset);
					m_t_gropValue1 = XBuffer.ReadInt(data, ref offset);
					m_t_groupValue2 = XBuffer.ReadInt(data, ref offset);
					m_t_groupValue3 = XBuffer.ReadInt(data, ref offset);
					m_t_groupValue4 = XBuffer.ReadInt(data, ref offset);
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_exp_type = XBuffer.ReadInt(data, ref offset);
					t_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_attr = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


