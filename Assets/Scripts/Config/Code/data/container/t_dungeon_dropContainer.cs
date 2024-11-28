/**
 * Auto generated, do not edit it
 *
 * t_dungeon_drop
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_dungeon_dropContainer : BaseContainer
	{
		private List<t_dungeon_dropBean> list = new List<t_dungeon_dropBean>();
		private Dictionary<int, t_dungeon_dropBean> map = new Dictionary<int, t_dungeon_dropBean>();

		//public override List<t_dungeon_dropBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_dungeon_dropBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_dungeon_dropBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_dungeon_dropBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_dungeon_dropBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_dungeon_dropBean bean = new t_dungeon_dropBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_dungeon_dropContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_dungeon_dropBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_dungeon_dropBean).Name + ".bytes");
			}
		}
	}

}


