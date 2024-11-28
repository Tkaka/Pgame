/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.11.09
*/

public class GlobalNetBytesCache
{
    private static object lockObj = new object();
    private static NetCacheAlloctor allocator;
    public static void Init(int maxSize, int minSize, int increase, int count)
    {
        lock(lockObj)
        {
            if(allocator == null)
            {
                allocator = new NetCacheAlloctor();
                allocator.Init(maxSize, minSize, increase, count);
            }
        }
    }

    public static byte[] Alloc(int size)
    {
        lock(lockObj)
        {
            if(allocator != null)
                return allocator.Alloc(size);

            if(allocator == null)
                Init(512, 64, 64, 1);
            return allocator.Alloc(size);
        }
    }

    public static void Free(byte[] bytes)
    {
        lock(lockObj)
        {
            if(allocator != null)
                allocator.Free(bytes);
        }
    }
}