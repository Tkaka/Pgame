using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class RandomExt
{
    private long seed = 100;

    public RandomExt(long seed)
    {
        setSeed(seed);
    }

    public RandomExt()
    {
 
    }

    public void setSeed(long seed)
    {
        this.seed = (seed ^ 0x5DEECE66DL) & ((1L << 48) - 1);
    }

    public int NextInt(int n)
    {
        if (n <= 0) throw new ArgumentException("n must be positive");

        if ((n & -n) == n)  // i.e., n is a power of 2
            return (int)((n * (long)Next(31)) >> 31);

        long bits, val;
        do
        {
            bits = Next(31);
            val = bits % (UInt32)n;
        }
        while (bits - val + (n - 1) < 0);

        return (int)val;
    }

    protected UInt32 Next(int bits)
    {
        seed = (seed * 0x5DEECE66DL + 0xBL) & ((1L << 48) - 1);

        return (UInt32)(seed >> (48 - bits));
    }

}