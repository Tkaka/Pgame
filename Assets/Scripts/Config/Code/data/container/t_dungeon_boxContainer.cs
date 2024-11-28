/**
 * Auto generated, do not edit it
 *
 * t_dungeon_box
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_dungeon_boxContainer : BaseContainer
	{
		private List<t_dungeon_boxBean> list = new List<t_dungeon_boxBean>();
		private Dictionary<int, t_dungeon_boxBean> map = new Dictionary<int, t_dungeon_boxBean>();

		//public override List<t_dungeon_boxBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_dungeon_boxBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_dungeon_boxBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_dungeon_boxBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_dungeon_boxBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_dungeon_boxBean bean = new t_dungeon_boxBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_dungeon_boxContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_dungeon_boxBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_dungeon_boxBean).Name + ".bytes");
			}
		}
	}

}


