/*
 * file IdAssginer.cs
 *
 * author: Pengmian
 * date:   2014/10/9
 */


using System;
using System.Collections.Generic;

/// <summary>
/// 实例ID分配器
/// </summary>
public class IdAssginer
{
	private static readonly Int64 Default_ID_Begin = 100;
	private static Dictionary<IdType, Int64> mIds = new Dictionary<IdType, Int64>();
	private static object mLock = new object();

	public enum IdType
	{
		CoroutineId = 1,
		ActorId = 2,
		AsyncHttpId = 3,
		PropsId = 4,
		Audio = 5,
		WWW = 6,
		CoolDown = 7,
		Window = 8,
	}

	/// <summary>
	/// 返回实例Id （线程安全）
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public static Int64 getId(IdType type)
	{
		lock (mLock)
		{
			Int64 startId;
			if (!mIds.TryGetValue(type, out startId))
			{
				mIds.Add(type, Default_ID_Begin);
			}
			return (Int64)((((UInt64)type & 0x00000000000000FF) << 56) | ((UInt64)((mIds[type]++) & 0x00FFFFFFFFFFFFFF)));
		}
	}

}
