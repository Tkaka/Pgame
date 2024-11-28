using System.Collections.Generic;

public class Guide
{
    public int id { get; private set; }

    private int curStepIdx;
    private GuideStep curStep;
    private List<int> stepList;

    public Guide(int guideId)
    {
        id = guideId;
        curStep = null;
        curStepIdx = -1;
        stepList = new List<int>();
        var gb = ConfigBean.GetBean<Data.Beans.t_guideBean, int>(id);
        if (gb != null)
        {
            stepList = new List<int>();
            var arr = gb.t_step.Split('+');
            for(int i=0; i<arr.Length; ++i)
                stepList.Add(int.Parse(arr[i]));
        }
    }

    public void Begin()
    {
        CheckTrigger(GuideEventID.Invalid, null);
    }

    private void finish()
    {
        GED.GuideED.dispatchEvent(GuideEventID.GuideFinish, id);
    }

    public void CheckTrigger(GuideEventID evt, string param)
    {
        //只能有一步在引导
        if (curStep != null)
        {
            if(!curStep.isDisposed)
                curStep.CheckFinishTrigger(evt, param);
        }else
        {
            int idx = curStepIdx + 1;
            if(idx >= stepList.Count)
                return;

            int tmpId = stepList[idx];
            var gsb = ConfigBean.GetBean<Data.Beans.t_guide_stepBean, int>(tmpId);
            if(gsb != null)
            {
                if (GuideConditions.CheckTrigger(true, tmpId, gsb.t_trigger, evt, param))
                    stepForward();
            }else
            {
                //配置错误，执行下一步
                curStepIdx++;
                this.CheckTrigger(evt, param);
            }
        }
    }

    //往前走一步
    private void stepForward()
    {
        curStepIdx++;
        if(curStepIdx < stepList.Count)
        {
            //下一步
            curStep = new GuideStep(stepList[curStepIdx], this);
            curStep.Begin();
        }
    }
    
    public void FinishStep(int stepId)
    {
        if(curStep == null)
        {
            Debuger.Err("当前小步骤为空");
            return;
        }
        if(curStep.id != stepId)
        {
            Debuger.Err("当前步骤和完成步骤id不一致", stepId, curStep.id);
            return;
        }

        curStep = null;
        if (curStepIdx >= stepList.Count - 1)
            finish();
    }
}