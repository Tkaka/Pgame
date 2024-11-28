/**
 * Auto generated, do not edit it
 *
 * t_skill
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_skillContainer : BaseContainer
	{
		private List<t_skillBean> list = new List<t_skillBean>();
		private Dictionary<int, t_skillBean> map = new Dictionary<int, t_skillBean>();

		//public override List<t_skillBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_skillBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_skillBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_skillBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_skillBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_skillBean bean = new t_skillBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_skillContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_skillBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_skillBean).Name + ".bytes");
			}
		}
	}

}


