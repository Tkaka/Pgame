/**
 * Auto generated, do not edit it
 *
 * t_arena_type
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_arena_typeContainer : BaseContainer
	{
		private List<t_arena_typeBean> list = new List<t_arena_typeBean>();
		private Dictionary<int, t_arena_typeBean> map = new Dictionary<int, t_arena_typeBean>();

		//public override List<t_arena_typeBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_arena_typeBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_arena_typeBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_arena_typeBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_arena_typeBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_arena_typeBean bean = new t_arena_typeBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_arena_typeContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_arena_typeBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_arena_typeBean).Name + ".bytes");
			}
		}
	}

}


