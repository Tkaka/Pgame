using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class BaseRecycleAbleEffect : BaseBehaviour
{

    protected virtual void doEffect()
    {
        //do something
    }

    protected override void Start()
    {
        base.Start();
        doEffect();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        reset();
    }

    public virtual void reset()
    {
 
    }


}
