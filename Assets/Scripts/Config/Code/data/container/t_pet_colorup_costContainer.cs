/**
 * Auto generated, do not edit it
 *
 * t_pet_colorup_cost
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_pet_colorup_costContainer : BaseContainer
	{
		private List<t_pet_colorup_costBean> list = new List<t_pet_colorup_costBean>();
		private Dictionary<int, t_pet_colorup_costBean> map = new Dictionary<int, t_pet_colorup_costBean>();

		//public override List<t_pet_colorup_costBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_pet_colorup_costBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_pet_colorup_costBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_pet_colorup_costBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_pet_colorup_costBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_pet_colorup_costBean bean = new t_pet_colorup_costBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_pet_colorup_costContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_pet_colorup_costBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_pet_colorup_costBean).Name + ".bytes");
			}
		}
	}

}


