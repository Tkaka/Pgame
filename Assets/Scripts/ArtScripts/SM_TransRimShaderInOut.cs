using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SM_TransRimShaderInOut : BaseRecycleAbleEffect
{

    public float Str = 1;

    public float FadeIn = 1;

    public float Stay = 1;

    public float FadeOut = 1;

    private float mTimeGoes = 0;

    private float mCurrStr = 0;

    private Renderer render;

    private float[] values = new float[4]; 
    protected override void Awake()
    {
        base.Awake();
        values[0] = Str;
        values[1] = FadeIn;
        values[2] = Stay;
        values[3] = FadeOut;
        render = GetComponent<Renderer>();
    }

    protected override void doEffect()
    {
        render.material.SetFloat("_AllPower", mCurrStr);
    }

    protected override void Update()
    {
        mTimeGoes += Time.deltaTime;

        if (mTimeGoes < FadeIn)
        {
            mCurrStr = mTimeGoes * Str * (1 / FadeIn);
        }

        if ((mTimeGoes > FadeIn) && (mTimeGoes < FadeIn + Stay))
        {
            mCurrStr = Str;
        }

        if (mTimeGoes > FadeIn + Stay)
        {
            mCurrStr = Str - ((mTimeGoes - (FadeIn + Stay)) * (1 / FadeOut));
        }

        //currStr=startStr-timeGoes;
        render.material.SetFloat("_AllPower", mCurrStr);
    }

    public override void reset()
    {
        mCurrStr = values[0];
        FadeIn = values[1];
        Stay = values[2];
        FadeOut = values[3];
        mTimeGoes = 0;
    }




}
