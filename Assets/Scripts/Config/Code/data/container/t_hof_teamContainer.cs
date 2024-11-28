/**
 * Auto generated, do not edit it
 *
 * t_hof_team
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_hof_teamContainer : BaseContainer
	{
		private List<t_hof_teamBean> list = new List<t_hof_teamBean>();
		private Dictionary<int, t_hof_teamBean> map = new Dictionary<int, t_hof_teamBean>();

		//public override List<t_hof_teamBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_hof_teamBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_hof_teamBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_hof_teamBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_hof_teamBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_hof_teamBean bean = new t_hof_teamBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_hof_teamContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_hof_teamBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_hof_teamBean).Name + ".bytes");
			}
		}
	}

}


