// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:34062,y:32772,varname:node_4795,prsc:2|custl-4082-OUT,clip-8419-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:33629,y:32324,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:7174,x:33134,y:32519,varname:node_7174,prsc:2|EXP-9009-OUT;n:type:ShaderForge.SFN_Add,id:2289,x:33666,y:32554,varname:node_2289,prsc:2|A-7270-OUT,B-6074-RGB;n:type:ShaderForge.SFN_Slider,id:9009,x:32785,y:32578,ptovrint:False,ptlb:fnl_bian,ptin:_fnl_bian,varname:node_9009,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3.758263,max:5;n:type:ShaderForge.SFN_Multiply,id:284,x:33282,y:32475,varname:node_284,prsc:2|A-2523-OUT,B-7174-OUT;n:type:ShaderForge.SFN_Slider,id:2523,x:32872,y:32453,ptovrint:False,ptlb:fnl_qiangdu,ptin:_fnl_qiangdu,varname:node_2523,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.859968,max:5;n:type:ShaderForge.SFN_Color,id:9560,x:33187,y:32321,ptovrint:False,ptlb:fre_color,ptin:_fre_color,varname:node_9560,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:7270,x:33464,y:32432,varname:node_7270,prsc:2|A-9560-RGB,B-284-OUT;n:type:ShaderForge.SFN_Tex2d,id:9913,x:33347,y:32704,ptovrint:False,ptlb:liudong,ptin:_liudong,varname:node_9913,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9355-UVOUT;n:type:ShaderForge.SFN_Panner,id:9355,x:33176,y:32704,varname:node_9355,prsc:2,spu:-0.06,spv:0|UVIN-7962-UVOUT;n:type:ShaderForge.SFN_Add,id:4082,x:33878,y:32827,varname:node_4082,prsc:2|A-2289-OUT,B-9858-OUT;n:type:ShaderForge.SFN_TexCoord,id:7962,x:32978,y:32704,varname:node_7962,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:9858,x:33676,y:32751,varname:node_9858,prsc:2|A-9913-RGB,B-7660-RGB;n:type:ShaderForge.SFN_Color,id:7660,x:33482,y:32774,ptovrint:False,ptlb:liudong_color,ptin:_liudong_color,varname:node_7660,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_TexCoord,id:5404,x:33101,y:33092,varname:node_5404,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_Panner,id:2116,x:33316,y:33092,varname:node_2116,prsc:2,spu:1,spv:1|UVIN-5404-UVOUT,DIST-5230-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5824,x:33487,y:33092,varname:node_5824,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-2116-UVOUT;n:type:ShaderForge.SFN_Lerp,id:8419,x:33850,y:33056,varname:node_8419,prsc:2|A-4207-OUT,B-6719-OUT,T-3559-OUT;n:type:ShaderForge.SFN_Vector1,id:4207,x:33593,y:32944,varname:node_4207,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:6719,x:33593,y:33016,varname:node_6719,prsc:2,v1:0;n:type:ShaderForge.SFN_Clamp01,id:3559,x:33652,y:33092,varname:node_3559,prsc:2|IN-5824-OUT;n:type:ShaderForge.SFN_Slider,id:5230,x:32928,y:33281,ptovrint:False,ptlb:Grow,ptin:_Grow,varname:node_5230,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1,max:1;proporder:6074-9009-2523-9560-9913-7660-5230;pass:END;sub:END;*/

Shader "Shader Forge/dm_grow" {
    Properties {
        [HDR]_MainTex ("MainTex", 2D) = "white" {}
        _fnl_bian ("fnl_bian", Range(0, 5)) = 3.758263
        _fnl_qiangdu ("fnl_qiangdu", Range(0, 5)) = 1.859968
        [HDR]_fre_color ("fre_color", Color) = (0.5,0.5,0.5,1)
        _liudong ("liudong", 2D) = "white" {}
        _liudong_color ("liudong_color", Color) = (0.5,0.5,0.5,1)
        _Grow ("Grow", Range(-1, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _fnl_bian;
            uniform float _fnl_qiangdu;
            uniform float4 _fre_color;
            uniform sampler2D _liudong; uniform float4 _liudong_ST;
            uniform float4 _liudong_color;
            uniform float _Grow;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                clip(lerp(1.0,0.0,saturate((i.uv1+_Grow*float2(1,1)).r)) - 0.5);
////// Lighting:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_1306 = _Time + _TimeEditor;
                float2 node_9355 = (i.uv0+node_1306.g*float2(-0.06,0));
                float4 _liudong_var = tex2D(_liudong,TRANSFORM_TEX(node_9355, _liudong));
                float3 finalColor = (((_fre_color.rgb*(_fnl_qiangdu*pow(1.0-max(0,dot(normalDirection, viewDirection)),_fnl_bian)))+_MainTex_var.rgb)+(_liudong_var.rgb*_liudong_color.rgb));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
