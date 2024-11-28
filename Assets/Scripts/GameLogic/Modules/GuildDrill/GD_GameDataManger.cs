using Message.Guild;

public class GD_GameDataManger
{
    public ResOpenExpHome mydrill;//我的训练所
    public ResExpHomeRoleList roleList;//训练所角色列表
    public int GuildLevel;
    public GD_GameDataManger()
    {
        mydrill = new ResOpenExpHome();//内含宠物列表
        roleList = new ResExpHomeRoleList();

        Debuger.Log("-------------addListener");
        AddEventListener();
        TestData();
    }
    private void TestData()
    {
    }
    private void AddEventListener()
    {
        GED.ED.addListener(EventID.OnGuildDrillReceiveData, OnGuildDrillReceiveData);
        GED.ED.addListener(EventID.OnGuildDrillExpediteRole, OnGuildDrillExpediteRole);
        GED.ED.addListener(EventID.OnGuildDrillSetPet,OnSetPet);
    }
    private void RemoveEventListener()
    {
        GED.ED.removeListener(EventID.OnGuildDrillReceiveData, OnGuildDrillReceiveData);
        GED.ED.removeListener(EventID.OnGuildDrillExpediteRole, OnGuildDrillExpediteRole);
        GED.ED.removeListener(EventID.OnGuildDrillSetPet,OnSetPet);
    }
    private void OnGuildDrillReceiveData(GameEvent evt)
    {
        mydrill = (ResOpenExpHome)evt.Data;
        mydrill.pets.Sort(SortPaml);
        GuildLevel = GuildService.Singleton.GetGuildInfo().level;//公会等级
    }
    private void OnGuildDrillExpediteRole(GameEvent evt)
    {
        roleList = (ResExpHomeRoleList)evt.Data;
    }
    private void OnSetPet(GameEvent evt)
    {
        PosPet posPet = evt.Data as PosPet;
        for (int i = 0; i < mydrill.pets.Count; ++i)
        {
            if (mydrill.pets[i].id == posPet.id)
            {
                mydrill.pets[i].petId = posPet.petId;
                break;
            }
        }
        if (posPet != null)
        {
            TwoParam<int, int> twoParam = new TwoParam<int, int>();
            twoParam.value1 = posPet.id;
            twoParam.value2 = posPet.petId;
            GED.ED.dispatchEvent(EventID.OnGuildDrillAffirmChange, twoParam);
        }
        
    }
    private int SortPaml(PosPet a, PosPet b)
    {
        int resA = 0;
        int resB = 0;

        if (a.id < b.id)
            resA += 1000;
        else if (a.id > b.id)
            resB += 1000;

        if (resA > resB)
            return -1;
        else if (resA < resB)
            return 1;
        else
            return 0;

    }
    public void Close()
    {
        RemoveEventListener();
        mydrill = null;
        roleList = null;
    }
}