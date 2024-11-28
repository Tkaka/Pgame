/**
 * Auto generated, do not edit it
 *
 * t_open_carnival_column
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_open_carnival_columnContainer : BaseContainer
	{
		private List<t_open_carnival_columnBean> list = new List<t_open_carnival_columnBean>();
		private Dictionary<int, t_open_carnival_columnBean> map = new Dictionary<int, t_open_carnival_columnBean>();

		//public override List<t_open_carnival_columnBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_open_carnival_columnBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_open_carnival_columnBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_open_carnival_columnBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_open_carnival_columnBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_open_carnival_columnBean bean = new t_open_carnival_columnBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_open_carnival_columnContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_open_carnival_columnBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_open_carnival_columnBean).Name + ".bytes");
			}
		}
	}

}


