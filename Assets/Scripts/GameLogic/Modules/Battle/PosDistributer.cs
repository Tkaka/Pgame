using System.Collections.Generic;
using UnityEngine;

public class PosDistributer
{

    public enum PositionType
    {
        Damage,
        StatusEffect
    }

    protected PositionType mPositionType;

    // Fields
    protected List<bool> mPosIdxList = new List<bool>();
    protected List<Vector3> mPosList = new List<Vector3>();

    // 加血位置
    protected List<bool> mHpPosIdxList = new List<bool>();

    private void InitHpPosPool()
    {
        for (int i = 0; i < 20; i++)
        {
            mHpPosIdxList.Add(true);
        }
    }

    public void ReturnHpPos(int idx)
    {
        if (idx >= 0 && idx < mHpPosIdxList.Count)
        {
            this.mHpPosIdxList[idx] = true;
        }
    }

    public int GetNextHpPos()
    {
        for (int i = 0; i < this.mHpPosIdxList.Count; i++)
        {
            if (mHpPosIdxList[i])
            {
                mHpPosIdxList[i] = false;
                return i;
            }
        }
        return 0;
    }

    // Methods
    public PosDistributer(PositionType pt = PositionType.Damage)
    {
        this.mPositionType = pt;
        if (this.mPositionType == PositionType.Damage)
        {
            this.InitDamagePool();
        }
        else if (this.mPositionType == PositionType.StatusEffect)
        {
            this.InitStatusEffectPool();
        }
        InitHpPosPool();
    }

    public int GetNextPosition(out Vector3 vecOut)
    {
        for (int i = 0; i < this.mPosIdxList.Count; i++)
        {
            if (this.mPosIdxList[i])
            {
                this.mPosIdxList[i] = false;
                vecOut = this.mPosList[i];
                return i;
            }
        }
        vecOut = Vector3.zero;
        return 0;
    }

    private void InitDamagePool()
    {
        int num4 = 1;
        int num5 = 0;
        int num6 = 0;
        int num7 = 0;
        List<Vector3> list = new List<Vector3>();
        while (num5 < 100)
        {
            Vector3 item = new Vector3
            {
                x = ((num7 * 80f) - ((80f * num4) / 2f)) + Random.Range(-16f, 16f),
                y = (num6 * 50f) + Random.Range(-10f, 10f)
            };
            num7++;
            if (num7 >= num4)
            {
                num4++;
                num6++;
                num7 = 0;
                foreach (Vector3 vector2 in list)
                {
                    this.mPosList.Add(vector2);
                    this.mPosIdxList.Add(true);
                }
                list.Clear();
            }
            list.Add(item);
            num5++;
        }
    }

    private void InitStatusEffectPool()
    {
        int num3 = 0;
        List<Vector3> list = new List<Vector3>();
        while (num3 < 10)
        {
            Vector3 item = new Vector3
            {
                x = num3 * 5,
                y = num3 * 0x23
            };
            this.mPosList.Add(item);
            this.mPosIdxList.Add(true);
            num3++;
        }
    }

    public void RestorePositionToPool(int idx)
    {
        if (idx >=0 && idx < this.mPosIdxList.Count)
        {
            this.mPosIdxList[idx] = true;
        }
    }

}
