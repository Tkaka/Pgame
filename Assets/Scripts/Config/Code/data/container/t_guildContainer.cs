/**
 * Auto generated, do not edit it
 *
 * t_guild
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_guildContainer : BaseContainer
	{
		private List<t_guildBean> list = new List<t_guildBean>();
		private Dictionary<int, t_guildBean> map = new Dictionary<int, t_guildBean>();

		//public override List<t_guildBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_guildBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_guildBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_guildBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_guildBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_guildBean bean = new t_guildBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_guildContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_guildBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_guildBean).Name + ".bytes");
			}
		}
	}

}


