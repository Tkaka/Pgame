/**
 * Auto generated, do not edit it
 *
 * t_pet_soulup_exp
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_pet_soulup_expContainer : BaseContainer
	{
		private List<t_pet_soulup_expBean> list = new List<t_pet_soulup_expBean>();
		private Dictionary<int, t_pet_soulup_expBean> map = new Dictionary<int, t_pet_soulup_expBean>();

		//public override List<t_pet_soulup_expBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_pet_soulup_expBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_pet_soulup_expBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_pet_soulup_expBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_pet_soulup_expBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_pet_soulup_expBean bean = new t_pet_soulup_expBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_pet_soulup_expContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_pet_soulup_expBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_pet_soulup_expBean).Name + ".bytes");
			}
		}
	}

}


