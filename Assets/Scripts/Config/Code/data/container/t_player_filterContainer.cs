/**
 * Auto generated, do not edit it
 *
 * t_player_filter
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_player_filterContainer : BaseContainer
	{
		private List<t_player_filterBean> list = new List<t_player_filterBean>();
		private Dictionary<int, t_player_filterBean> map = new Dictionary<int, t_player_filterBean>();

		//public override List<t_player_filterBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_player_filterBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_player_filterBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_player_filterBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_player_filterBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_player_filterBean bean = new t_player_filterBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_player_filterContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_player_filterBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_player_filterBean).Name + ".bytes");
			}
		}
	}

}


