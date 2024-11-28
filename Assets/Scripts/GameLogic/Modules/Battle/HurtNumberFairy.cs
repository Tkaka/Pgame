using UnityEngine;
using FairyGUI;
using UI_Battle;

public enum NumberType
{
    //Enemy,
    //Player,
    RedBall,
    //Combo,
    Cure,
    Hurt,
}

public class HurtNumberFairy : GComponent
{
    private UI_HurtNumberWrapper numberWrapper;

    private GTextField numberText = new GTextField();

    private Actor owner;

    private int posId = -1;

    private int eftPosId = -1;

    private bool directCmp = false;

    public bool isRecycled = false;

    private NumberType numberType = NumberType.Hurt;

    public HurtNumberFairy()
    {
        this.touchable = false;
        numberWrapper = UI_HurtNumberWrapper.CreateInstance();
        AddChild(numberWrapper);
    }

    public void SetHurt(Actor owner, NumberType type, long hurt, bool critical)
    {
        this.owner = owner;
        this.numberType = type;
        
        TextFormat tf = numberText.textFormat;
        tf.letterSpacing = -12;
        if (type == NumberType.Hurt)
        {
            if (critical)
            {
                numberText.text = hurt + "";
                tf.font = HurtNumberMgr.Singleton.hurtFont2;
            }
            else
            {
                numberText.text = hurt + "";
                tf.font = HurtNumberMgr.Singleton.hurtFont1;
            }
        }
        /*else if (type == NumberType.Combo)
        {
            numberText.text = "=";
            tf.font = HurtNumberMgr.Singleton.hurtFont1;
        }*/
        else if (type == NumberType.RedBall || type == NumberType.Cure)
        {
            tf.font = HurtNumberMgr.Singleton.hpFont;
            numberText.text = "+" + hurt;
        }
        else
        {
            tf.font = HurtNumberMgr.Singleton.hurtFont1;
            numberText.text = "" + hurt;
            Logger.err("HurtNumberFairy:SetHurt:无法识别的NumberType:" + type);
        }
        numberText.textFormat = tf;

        numberWrapper.m_hurtNumber.AddChild(numberText);
        this.SetSize(numberText.width, numberText.height);

        if (owner.monoBehavior.headBar == null)
        {
            //Logger.err("HurtNumberFairy:SetHurt:headBar is null");
            directCmp = true;
            OnCompleted();
            return;
        }

        if (numberType == NumberType.RedBall)
        {

            PetHeadBar headBar = owner.headBar as PetHeadBar;
            if (headBar != null)
            {
                Vector3 pos = headBar.LocalToRoot(new Vector2(headBar.m_numberPos.x, headBar.m_numberPos.y), GRoot.inst);
                posId = owner.posDistributer.GetNextHpPos();
                this.SetScale(0.8f, 0.8f);
                this.SetXY(pos.x, pos.y - actualHeight - posId * actualHeight);
            }
            else
            {
                Logger.log("HurtNumberFairy:SetHurt:can not find UI_PetHeadBar");
            }
        }
        else if (type == NumberType.Hurt || type == NumberType.Cure)
        {
            Vector3 pos = owner.monoBehavior.headBar.position;
            pos = WinMgr.Singleton.WorldToScreen(pos);
            Vector3 rnd = Vector3.zero;
            posId = owner.posDistributer.GetNextPosition(out rnd);
            pos.x += rnd.x;
            pos.y += rnd.y;
            this.SetXY(pos.x - this.actualWidth / 2, pos.y - this.actualHeight);
            float scale = Random.Range(0.7f, 1.0f);
            this.SetScale(scale, scale);
        }
        else
        {
            Vector3 pos = owner.monoBehavior.headBar.position;
            pos = WinMgr.Singleton.WorldToScreen(pos);
            Vector3 rnd = Vector3.zero;
            eftPosId = owner.EftPosDistributer.GetNextPosition(out rnd);
            pos.x += rnd.x;
            pos.y += rnd.y;
            this.SetXY(pos.x - this.actualWidth / 2, pos.y - this.actualHeight);
            float scale = Random.Range(0.7f, 1.0f);
            this.SetScale(scale, scale);
        }
    }

    public void Start()
    {
        if (isRecycled)
            return;
        HurtNumberMgr.Singleton.view.AddChild(this);
        if (numberType == NumberType.RedBall)
            numberWrapper.m_hongqiu.Play(OnCompleted);
        else
            numberWrapper.m_ani.Play(OnCompleted);
    }

    private void OnCompleted()
    {
        if (!directCmp)
        {
            if (numberType == NumberType.RedBall)
            {
                numberWrapper.m_hongqiu.Stop();
                owner.posDistributer.ReturnHpPos(posId);
            }
            else if (numberType == NumberType.Hurt || numberType == NumberType.Cure)
            {
                numberWrapper.m_ani.Stop();
                owner.posDistributer.RestorePositionToPool(posId);
            }
            /*else if(numberType == NumberType.Combo)
            {
                numberWrapper.m_ani.Stop();
                owner.EftPosDistributer.RestorePositionToPool(eftPosId);
            }*/
        }
        owner = null;
        directCmp = false;
        posId = -1;
        eftPosId = -1;
        HurtNumberMgr.Singleton.view.RemoveChild(this);
        HurtNumberMgr.Singleton.ReturnComponent(this);
        isRecycled = true;
    }

    /*public void Cancel()
    {
        if (this.parent != null)
        {
            EmitManager.Singleton.view.RemoveChild(this);
        }
        EmitManager.Singleton.ReturnComponent(this);
    }*/

}
