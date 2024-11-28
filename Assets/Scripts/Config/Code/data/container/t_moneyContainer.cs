/**
 * Auto generated, do not edit it
 *
 * t_money
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_moneyContainer : BaseContainer
	{
		private List<t_moneyBean> list = new List<t_moneyBean>();
		private Dictionary<int, t_moneyBean> map = new Dictionary<int, t_moneyBean>();

		//public override List<t_moneyBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_moneyBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_moneyBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_moneyBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_moneyBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_moneyBean bean = new t_moneyBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_moneyContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_moneyBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_moneyBean).Name + ".bytes");
			}
		}
	}

}


