/**
 * Auto generated, do not edit it
 *
 * t_attr_name
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_attr_nameContainer : BaseContainer
	{
		private List<t_attr_nameBean> list = new List<t_attr_nameBean>();
		private Dictionary<int, t_attr_nameBean> map = new Dictionary<int, t_attr_nameBean>();

		//public override List<t_attr_nameBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_attr_nameBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_attr_nameBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_attr_nameBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_attr_nameBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_attr_nameBean bean = new t_attr_nameBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_attr_nameContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_attr_nameBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_attr_nameBean).Name + ".bytes");
			}
		}
	}

}


