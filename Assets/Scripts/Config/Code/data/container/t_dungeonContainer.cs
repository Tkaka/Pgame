/**
 * Auto generated, do not edit it
 *
 * t_dungeon
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_dungeonContainer : BaseContainer
	{
		private List<t_dungeonBean> list = new List<t_dungeonBean>();
		private Dictionary<int, t_dungeonBean> map = new Dictionary<int, t_dungeonBean>();

		//public override List<t_dungeonBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_dungeonBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_dungeonBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_dungeonBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_dungeonBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_dungeonBean bean = new t_dungeonBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_dungeonContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_dungeonBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_dungeonBean).Name + ".bytes");
			}
		}
	}

}


