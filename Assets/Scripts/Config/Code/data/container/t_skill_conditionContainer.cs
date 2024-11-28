/**
 * Auto generated, do not edit it
 *
 * t_skill_condition
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_skill_conditionContainer : BaseContainer
	{
		private List<t_skill_conditionBean> list = new List<t_skill_conditionBean>();
		private Dictionary<int, t_skill_conditionBean> map = new Dictionary<int, t_skill_conditionBean>();

		//public override List<t_skill_conditionBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_skill_conditionBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_skill_conditionBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_skill_conditionBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_skill_conditionBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_skill_conditionBean bean = new t_skill_conditionBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_skill_conditionContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_skill_conditionBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_skill_conditionBean).Name + ".bytes");
			}
		}
	}

}


