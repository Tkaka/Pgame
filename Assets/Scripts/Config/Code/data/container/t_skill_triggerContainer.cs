/**
 * Auto generated, do not edit it
 *
 * t_skill_trigger
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_skill_triggerContainer : BaseContainer
	{
		private List<t_skill_triggerBean> list = new List<t_skill_triggerBean>();
		private Dictionary<int, t_skill_triggerBean> map = new Dictionary<int, t_skill_triggerBean>();

		//public override List<t_skill_triggerBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_skill_triggerBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_skill_triggerBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_skill_triggerBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_skill_triggerBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_skill_triggerBean bean = new t_skill_triggerBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_skill_triggerContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_skill_triggerBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_skill_triggerBean).Name + ".bytes");
			}
		}
	}

}


