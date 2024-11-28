/**
 * Auto generated, do not edit it
 *
 * t_pet
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_petContainer : BaseContainer
	{
		private List<t_petBean> list = new List<t_petBean>();
		private Dictionary<int, t_petBean> map = new Dictionary<int, t_petBean>();

		//public override List<t_petBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_petBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_petBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_petBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_petBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_petBean bean = new t_petBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_petContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_petBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_petBean).Name + ".bytes");
			}
		}
	}

}


