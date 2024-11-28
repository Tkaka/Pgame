/**
 * Auto generated, do not edit it
 *
 * t_aoyi
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_aoyiContainer : BaseContainer
	{
		private List<t_aoyiBean> list = new List<t_aoyiBean>();
		private Dictionary<int, t_aoyiBean> map = new Dictionary<int, t_aoyiBean>();

		//public override List<t_aoyiBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_aoyiBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_aoyiBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_aoyiBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_aoyiBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_aoyiBean bean = new t_aoyiBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_aoyiContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_aoyiBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_aoyiBean).Name + ".bytes");
			}
		}
	}

}


