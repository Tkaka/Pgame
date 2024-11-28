
var targetMaterialSlot:int=0;
//var scrollThis:Material;
var speedY:float=0.5;
var speedX:float=0.0;
private var timeWentX:float=0;
private var timeWentY:float=0;
function Start () {
    #if UNITY_EDITOR
        var shadername = GetComponent.<Renderer>().materials[targetMaterialSlot].shader.name;
    GetComponent.<Renderer>().materials[targetMaterialSlot].shader = Shader.Find(shadername);
   #endif
}

function Update () {
timeWentY += Time.deltaTime*speedY;
timeWentX += Time.deltaTime*speedX; 
GetComponent.<Renderer>().materials[targetMaterialSlot].SetTextureOffset ("_MainTex", Vector2(timeWentX, timeWentY));


}