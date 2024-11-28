/**
 * Auto generated, do not edit it
 *
 * t_achievement
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_achievementContainer : BaseContainer
	{
		private List<t_achievementBean> list = new List<t_achievementBean>();
		private Dictionary<int, t_achievementBean> map = new Dictionary<int, t_achievementBean>();

		//public override List<t_achievementBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_achievementBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_achievementBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_achievementBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_achievementBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_achievementBean bean = new t_achievementBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_achievementContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_achievementBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_achievementBean).Name + ".bytes");
			}
		}
	}

}


