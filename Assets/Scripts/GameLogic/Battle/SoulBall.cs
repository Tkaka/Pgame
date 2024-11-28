using UnityEngine;
using DG.Tweening;


public enum SoulBallType
{
    Red = 100,
    Blue,
}

public class SoulBall : BaseBehaviour
{

    private SoulBallType type;

    private LNumber val;

    private long targetId;

    public void Init(long targetId, SoulBallType type, LNumber val)
    {
        this.targetId = targetId;
        this.type = type;
        this.val = val;
        RandomPos();
    }

    private void RandomPos()
    {
        bool isMul = true;
        Vector3 position = TransformExt.position;
        position.y = 0.7f;
        Vector3 vector3 = TransformExt.position;
        vector3.x += Random.Range(-0.5f, 0.5f);
        if (isMul)
        {
            vector3.y += Random.Range(0.3f, 0.6f);
        }
        else
        {
            vector3.y += Random.Range(-0.3f, 0.3f);
        }
        vector3.z += Random.Range(-0.5f, 0.5f);
        tween = TransformExt.DOMove(vector3, 0.5f).OnComplete(StartIdle);
        //TransformExt.position = vector3;
        //delayCall(0.5f, StartIdle);
    }

    private Tween tween;
    private void StartIdle()
    {
        Vector3 pos = transform.position;
        pos.y += Random.Range(0.3f, 0.5f);
        tween = TransformExt.DOMove(pos, 0.8f);
        tween.SetLoops(-1, LoopType.Yoyo);
        //delayCall(1f, StartFly);
    }

    Vector3 targetPos = Vector3.zero;
    Actor target = null;
    public void StartFly()
    {
        //long actorId = RangeSelector.Singleton.GetRandom(ActorCamp.CampFriend);
        target = ActorManager.Singleton.Get(targetId);
        if (target != null)
        {
            //int hp = Random.Range(100, 150);
            //EmitManager.Singleton.Emit(target, NumberType.RedBall, hp);

            Vector3 startPos = TransformExt.position;
            Transform trans = target.monoBehavior.hitPos;
            if (trans != null)
                targetPos = trans.position;
            else
                Logger.err("SoulBall:StartFly:hitEftMount is null");
            Vector3 mid = Vector3.Lerp(startPos, targetPos, 0.4f);
            //float dis = Vector3.Distance(startPos, targetPos);
            //mid.x += Random.Range(-5f, 5f) / dis / Random.Range(2f, 3f);
            //mid.y += Random.Range(3f, 8f) / dis / Random.Range(2f, 3f);
            mid.x += Random.Range(-1f, 2f);
            mid.y += Random.Range(1f, 2f);
            Vector3[] path = new Vector3[] { startPos, mid, targetPos };
            if(tween != null && tween.IsActive())
                tween.Kill();
            float flyTime = Random.Range(0.5f, 0.8f);
            //float flyTime = 0.9f;
            tween = TransformExt.DOPath(path, flyTime, PathType.CatmullRom, PathMode.Full3D, 5);
            tween.SetEase(Ease.InSine);
            tween.OnComplete(OnFlyCmp);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static CdMgr cdMgr = new CdMgr();
    private void OnFlyCmp()
    {
        if (target != null && !target.IsDestoryed)
        {
            if (type == SoulBallType.Blue)
            {
                if (cdMgr.isCoolDown("blue"))
                {
                    FightManager.R.LoadGo("eff_hunzhu_blue_absorption", targetPos);
                    cdMgr.addCoolDown("blue", 1);
                }
                //加蓝
                //target.ChangeProperty(PropertyType.Mp, val);
                target.ViewPropertyMgr.ChangeProperty(PropertyType.Mp, val);
            }
            else
            {
                if (cdMgr.isCoolDown("red"))
                {
                    FightManager.R.LoadGo("eff_hunzhu_red_absorption", targetPos);
                    cdMgr.addCoolDown("red", 1);
                }
                //飘数字
                if((long)val > 0)
                    HurtNumberMgr.Singleton.Emit(target, NumberType.RedBall, (long)val);
            }
            AudioManager.Singleton.PlayEffect("snd_xihun");
        }
        DestroySelf();
    }

    public void DestroySelf()
    {
        if (tween != null && tween.IsActive())
            tween.Kill();
        tween = null;
        target = null;
        Destroy(gameObject);
    }

}