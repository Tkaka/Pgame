/**
 * Auto generated, do not edit it
 *
 * t_bexp
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_bexpContainer : BaseContainer
	{
		private List<t_bexpBean> list = new List<t_bexpBean>();
		private Dictionary<int, t_bexpBean> map = new Dictionary<int, t_bexpBean>();

		//public override List<t_bexpBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_bexpBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_bexpBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_bexpBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_bexpBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_bexpBean bean = new t_bexpBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_bexpContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_bexpBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_bexpBean).Name + ".bytes");
			}
		}
	}

}


