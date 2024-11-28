/**
 * Auto generated, do not edit it
 *
 * t_guild_battle_win
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_guild_battle_winContainer : BaseContainer
	{
		private List<t_guild_battle_winBean> list = new List<t_guild_battle_winBean>();
		private Dictionary<int, t_guild_battle_winBean> map = new Dictionary<int, t_guild_battle_winBean>();

		//public override List<t_guild_battle_winBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_guild_battle_winBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_guild_battle_winBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_guild_battle_winBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_guild_battle_winBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_guild_battle_winBean bean = new t_guild_battle_winBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_guild_battle_winContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_guild_battle_winBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_guild_battle_winBean).Name + ".bytes");
			}
		}
	}

}


