/**
 * Auto generated, do not edit it
 *
 * t_teamfight
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_teamfightContainer : BaseContainer
	{
		private List<t_teamfightBean> list = new List<t_teamfightBean>();
		private Dictionary<int, t_teamfightBean> map = new Dictionary<int, t_teamfightBean>();

		//public override List<t_teamfightBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_teamfightBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_teamfightBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_teamfightBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_teamfightBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_teamfightBean bean = new t_teamfightBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_teamfightContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_teamfightBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_teamfightBean).Name + ".bytes");
			}
		}
	}

}


