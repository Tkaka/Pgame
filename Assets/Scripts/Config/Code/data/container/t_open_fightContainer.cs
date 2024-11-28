/**
 * Auto generated, do not edit it
 *
 * t_open_fight
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_open_fightContainer : BaseContainer
	{
		private List<t_open_fightBean> list = new List<t_open_fightBean>();
		private Dictionary<int, t_open_fightBean> map = new Dictionary<int, t_open_fightBean>();

		//public override List<t_open_fightBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_open_fightBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_open_fightBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_open_fightBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_open_fightBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_open_fightBean bean = new t_open_fightBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_open_fightContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_open_fightBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_open_fightBean).Name + ".bytes");
			}
		}
	}

}


