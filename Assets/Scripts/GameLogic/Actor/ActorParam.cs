using UnityEngine;

public class ActorParam
{
    public Vector3 Pos;      //pos
    public Vector3 Dir;       //dir
    public int Star;            //星级
    public int GridId;         //格子ID

    public static ActorParam create(Vector3 pos, Vector3 dir, int star =-1, int GridId = 0)
    {
        ActorParam param = new ActorParam();
        param.Pos = pos;
        param.Dir = dir;
        param.Star = star;
        param.GridId = GridId;
        return param;
    }

}
