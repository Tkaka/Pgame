

using Message.GM;

public class GMService : SingletonService<GMService>
{

    public void ReqGm(string cmd, string param)
    {
        ReqGM msg = GetEmptyMsg<ReqGM>();
        msg.commandType = cmd;
        msg.parameters = param;
        SendMsg(ref msg);
    }

}
