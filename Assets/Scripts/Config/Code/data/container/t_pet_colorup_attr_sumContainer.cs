/**
 * Auto generated, do not edit it
 *
 * t_pet_colorup_attr_sum
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_pet_colorup_attr_sumContainer : BaseContainer
	{
		private List<t_pet_colorup_attr_sumBean> list = new List<t_pet_colorup_attr_sumBean>();
		private Dictionary<int, t_pet_colorup_attr_sumBean> map = new Dictionary<int, t_pet_colorup_attr_sumBean>();

		//public override List<t_pet_colorup_attr_sumBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_pet_colorup_attr_sumBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_pet_colorup_attr_sumBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_pet_colorup_attr_sumBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_pet_colorup_attr_sumBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_pet_colorup_attr_sumBean bean = new t_pet_colorup_attr_sumBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_pet_colorup_attr_sumContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_pet_colorup_attr_sumBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_pet_colorup_attr_sumBean).Name + ".bytes");
			}
		}
	}

}


