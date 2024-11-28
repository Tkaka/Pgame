/**
 * Auto generated, do not edit it
 *
 * t_skill_effect
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_skill_effectBean : BaseBin
	{
				private int m_t_id; // 效果ID(技能ID*4+序号,序号为0，是这个技能的主效果） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 效果ID(技能ID*4+序号,序号为0，是这个技能的主效果） 
				private int m_t_effect_turn; // 额外效果在技能主效果之前还是之后(1=主效果之后,2 = 主效果之前) 
				public int t_effect_turn{get{return m_t_effect_turn;} set{m_t_effect_turn = value;}} // 额外效果在技能主效果之前还是之后(1=主效果之后,2 = 主效果之前) 
				private int m_t_scope_id; // 范围类型98=技能主效果目标 99=前排单体 100=己方全体 101=己方全体（不含自己） 102=敌方全体 103=己方种族为"某"的宝贝（宠物表t_race） 105=己方类型为"某"的宝贝（如：攻，防，技） 106=首领（0：首领，1：非首领） 108=自己 109=自己及身后 110=我方前排 111=敌方前排 112=敌方后排 113=敌方后排随机单体 114=敌方一列 115=敌方攻击最高目标(属性id，1=最高 2=最低) 116=敌方生命最 目标(属性id，1=最高 2=最低) 117=敌方生命最低目标(属性id，1=最高 2=最低) 118=敌方随机x名女性 119=敌方特定宝贝（如：炎之克里斯） 120=自己所在列友军 121=血量最少友军携带指定buff的目标(属性id，buffID)122=目标周围(目标上下左右) 
				public int t_scope_id{get{return m_t_scope_id;} set{m_t_scope_id = value;}} // 范围类型98=技能主效果目标 99=前排单体 100=己方全体 101=己方全体（不含自己） 102=敌方全体 103=己方种族为"某"的宝贝（宠物表t_race） 105=己方类型为"某"的宝贝（如：攻，防，技） 106=首领（0：首领，1：非首领） 108=自己 109=自己及身后 110=我方前排 111=敌方前排 112=敌方后排 113=敌方后排随机单体 114=敌方一列 115=敌方攻击最高目标(属性id，1=最高 2=最低) 116=敌方生命最 目标(属性id，1=最高 2=最低) 117=敌方生命最低目标(属性id，1=最高 2=最低) 118=敌方随机x名女性 119=敌方特定宝贝（如：炎之克里斯） 120=自己所在列友军 121=血量最少友军携带指定buff的目标(属性id，buffID)122=目标周围(目标上下左右) 
				private int m_t_effect_type; // 效果类型 0=强化被动加属性 1=添加buff 2=附加A溅射N/百分比N 3=吸取属性N%，自己拿M% 4=技能主效果伤害 5=技能主效果治疗（攻击者攻击力百分比+技能数值=治疗量）6=技能额外效果灼烧（扣血量= 攻击者攻击力*技能百分比+技能数值）8=真空类额外获得战魂效果（参数3=1，额外获得龟魂；参数3=2额外获得熊魂；）9=添加复活buff 
				public int t_effect_type{get{return m_t_effect_type;} set{m_t_effect_type = value;}} // 效果类型 0=强化被动加属性 1=添加buff 2=附加A溅射N/百分比N 3=吸取属性N%，自己拿M% 4=技能主效果伤害 5=技能主效果治疗（攻击者攻击力百分比+技能数值=治疗量）6=技能额外效果灼烧（扣血量= 攻击者攻击力*技能百分比+技能数值）8=真空类额外获得战魂效果（参数3=1，额外获得龟魂；参数3=2额外获得熊魂；）9=添加复活buff 
				public string t_condition_id {get; set;}   // 效果触发条件ID(99=无条件  4=自己方N1-N8在场 101=敌方N-N8在场 5=我方四魂达成（4个类都有） 105=目标生命值小于45%时 104=敌方上阵战魂（虎蛇熊龟）小于三种 106=若自己上场大于等于N回合 106=双方14以上资质宝贝总数大于等于N 107=敌方14以上资质宝贝总数大于等于N )
				private int m_t_trigger_id; // triggerID 
				public int t_trigger_id{get{return m_t_trigger_id;} set{m_t_trigger_id = value;}} // triggerID 
				private int m_t_rate_base; // 效果概率基础值 
				public int t_rate_base{get{return m_t_rate_base;} set{m_t_rate_base = value;}} // 效果概率基础值 
				private int m_t_rate_grow; // 效果概率成长值 
				public int t_rate_grow{get{return m_t_rate_grow;} set{m_t_rate_grow = value;}} // 效果概率成长值 
				private int m_t_param1_base; // 参数1基础值 
				public int t_param1_base{get{return m_t_param1_base;} set{m_t_param1_base = value;}} // 参数1基础值 
				private int m_t_param1_grow; // 参数1成长值 
				public int t_param1_grow{get{return m_t_param1_grow;} set{m_t_param1_grow = value;}} // 参数1成长值 
				private int m_t_param2_base; // 参数2基础值 
				public int t_param2_base{get{return m_t_param2_base;} set{m_t_param2_base = value;}} // 参数2基础值 
				private int m_t_param2_grow; // 参数2成长值 
				public int t_param2_grow{get{return m_t_param2_grow;} set{m_t_param2_grow = value;}} // 参数2成长值 
				private int m_t_param3; // 参数3基础值(效果类型为3时，这里写返还给自己的百分比；当效果类型为附加buff且buff为免疫buff或者免疫控制时，这里写的是buff的优先级；复活时是否再次触发BUFF1=是，0=否） 
				public int t_param3{get{return m_t_param3;} set{m_t_param3 = value;}} // 参数3基础值(效果类型为3时，这里写返还给自己的百分比；当效果类型为附加buff且buff为免疫buff或者免疫控制时，这里写的是buff的优先级；复活时是否再次触发BUFF1=是，0=否） 
				public string t_paramr {get; set;}   // 分支条件，对应的buffID的字符串连接
				private int m_t_property1; // 被动属性1ID 
				public int t_property1{get{return m_t_property1;} set{m_t_property1 = value;}} // 被动属性1ID 
				private int m_t_property2; // 被动属性2ID 
				public int t_property2{get{return m_t_property2;} set{m_t_property2 = value;}} // 被动属性2ID 
				private int m_t_buff_id; // 效果参数1(buffID) 
				public int t_buff_id{get{return m_t_buff_id;} set{m_t_buff_id = value;}} // 效果参数1(buffID) 
				private int m_t_buff_effect_type; // 效果参数2（0= 到自己方的回合执行buff功能 1=立即执行buff功能） 
				public int t_buff_effect_type{get{return m_t_buff_effect_type;} set{m_t_buff_effect_type = value;}} // 效果参数2（0= 到自己方的回合执行buff功能 1=立即执行buff功能） 
				private int m_t_buff_turn; // 效果参数3(持续回合数.-1为一直存在） 
				public int t_buff_turn{get{return m_t_buff_turn;} set{m_t_buff_turn = value;}} // 效果参数3(持续回合数.-1为一直存在） 
				private int m_t_is_show; // 是否表现(0 =不表现1=表现) 
				public int t_is_show{get{return m_t_is_show;} set{m_t_is_show = value;}} // 是否表现(0 =不表现1=表现) 
				private int m_t_start_turn; // 从第几回合开始生效 
				public int t_start_turn{get{return m_t_start_turn;} set{m_t_start_turn = value;}} // 从第几回合开始生效 
				private int m_t_effect_count; // 生效次数上限(-1为不限次数) 
				public int t_effect_count{get{return m_t_effect_count;} set{m_t_effect_count = value;}} // 生效次数上限(-1为不限次数) 
				private int m_t_extra_buff_id; // 额外buff（只有3类型的效果用得到，目标为自己，buff属性继承主buffID，数值百分比由效果参数3决定） 
				public int t_extra_buff_id{get{return m_t_extra_buff_id;} set{m_t_extra_buff_id = value;}} // 额外buff（只有3类型的效果用得到，目标为自己，buff属性继承主buffID，数值百分比由效果参数3决定） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_effect_turn = XBuffer.ReadInt(data, ref offset);
					m_t_scope_id = XBuffer.ReadInt(data, ref offset);
					m_t_effect_type = XBuffer.ReadInt(data, ref offset);
					t_condition_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_trigger_id = XBuffer.ReadInt(data, ref offset);
					m_t_rate_base = XBuffer.ReadInt(data, ref offset);
					m_t_rate_grow = XBuffer.ReadInt(data, ref offset);
					m_t_param1_base = XBuffer.ReadInt(data, ref offset);
					m_t_param1_grow = XBuffer.ReadInt(data, ref offset);
					m_t_param2_base = XBuffer.ReadInt(data, ref offset);
					m_t_param2_grow = XBuffer.ReadInt(data, ref offset);
					m_t_param3 = XBuffer.ReadInt(data, ref offset);
					t_paramr = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_property1 = XBuffer.ReadInt(data, ref offset);
					m_t_property2 = XBuffer.ReadInt(data, ref offset);
					m_t_buff_id = XBuffer.ReadInt(data, ref offset);
					m_t_buff_effect_type = XBuffer.ReadInt(data, ref offset);
					m_t_buff_turn = XBuffer.ReadInt(data, ref offset);
					m_t_is_show = XBuffer.ReadInt(data, ref offset);
					m_t_start_turn = XBuffer.ReadInt(data, ref offset);
					m_t_effect_count = XBuffer.ReadInt(data, ref offset);
					m_t_extra_buff_id = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


