/**
 * Auto generated, do not edit it
 *
 * t_fighting_args
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_fighting_argsContainer : BaseContainer
	{
		private List<t_fighting_argsBean> list = new List<t_fighting_argsBean>();
		private Dictionary<int, t_fighting_argsBean> map = new Dictionary<int, t_fighting_argsBean>();

		//public override List<t_fighting_argsBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_fighting_argsBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_fighting_argsBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_fighting_argsBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_fighting_argsBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_fighting_argsBean bean = new t_fighting_argsBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_fighting_argsContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_fighting_argsBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_fighting_argsBean).Name + ".bytes");
			}
		}
	}

}


