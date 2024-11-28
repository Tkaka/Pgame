/**
 * Auto generated, do not edit it
 *
 * t_guild_battle_time
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_guild_battle_timeContainer : BaseContainer
	{
		private List<t_guild_battle_timeBean> list = new List<t_guild_battle_timeBean>();
		private Dictionary<int, t_guild_battle_timeBean> map = new Dictionary<int, t_guild_battle_timeBean>();

		//public override List<t_guild_battle_timeBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_guild_battle_timeBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_guild_battle_timeBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_guild_battle_timeBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_guild_battle_timeBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_guild_battle_timeBean bean = new t_guild_battle_timeBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_guild_battle_timeContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_guild_battle_timeBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_guild_battle_timeBean).Name + ".bytes");
			}
		}
	}

}


