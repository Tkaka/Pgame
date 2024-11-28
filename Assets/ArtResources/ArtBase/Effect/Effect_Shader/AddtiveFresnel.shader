// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:True;n:type:ShaderForge.SFN_Final,id:1,x:32235,y:32724|emission-7-OUT;n:type:ShaderForge.SFN_Fresnel,id:2,x:33365,y:32400;n:type:ShaderForge.SFN_Tex2d,id:3,x:33179,y:33025,ptlb:Maintex,ptin:_Maintex,ntxv:1,isnm:False;n:type:ShaderForge.SFN_Color,id:4,x:33158,y:32859,ptlb:Color,ptin:_Color,glob:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Power,id:5,x:32601,y:32588|VAL-31-OUT,EXP-6-OUT;n:type:ShaderForge.SFN_Slider,id:6,x:33121,y:32695,ptlb:Fresnel,ptin:_Fresnel,min:0,cur:5,max:10;n:type:ShaderForge.SFN_Multiply,id:7,x:32763,y:32990|A-5-OUT,B-4-RGB,C-3-RGB,D-16-OUT,E-43-OUT;n:type:ShaderForge.SFN_ValueProperty,id:16,x:33209,y:33230,ptlb:ColorPower,ptin:_ColorPower,glob:False,v1:2;n:type:ShaderForge.SFN_ToggleProperty,id:21,x:32996,y:32616,ptlb:One Minus,ptin:_OneMinus,on:True;n:type:ShaderForge.SFN_OneMinus,id:22,x:32996,y:32386|IN-2-OUT;n:type:ShaderForge.SFN_Lerp,id:31,x:32782,y:32456|A-22-OUT,B-2-OUT,T-21-OUT;n:type:ShaderForge.SFN_Tex2d,id:38,x:33189,y:33347,ptlb:Alpha,ptin:_Alpha,tex:e906535b2e269974698fcd2780f4c520,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:43,x:32892,y:33271|A-38-R,B-4-A;proporder:3-4-6-16-21-38;pass:END;sub:END;*/

Shader "TF/Particles/AddtiveFresnel" {
    Properties {
        _Maintex ("Maintex", 2D) = "gray" {}
        _Color ("Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _Fresnel ("Fresnel", Range(0, 10)) = 5
        _ColorPower ("ColorPower", Float ) = 2
        [MaterialToggle] _OneMinus ("One Minus", Float ) = 0
        _Alpha ("Alpha", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+1"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 2.0
            uniform sampler2D _Maintex; uniform float4 _Maintex_ST;
            uniform fixed4 _Color;
            uniform float _Fresnel;
            uniform float _ColorPower;
            uniform fixed _OneMinus;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
////// Lighting:
////// Emissive:
                float Fre = (1.0-max(0,dot(normalDirection, viewDirection)));
                float2 IUV = i.uv0;
                half3 emissive = (pow(lerp((1.0 - Fre),Fre,_OneMinus),_Fresnel)*_Color.rgb*tex2D(_Maintex,TRANSFORM_TEX(IUV.rg, _Maintex)).rgb*_ColorPower*(tex2D(_Alpha,TRANSFORM_TEX(IUV.rg, _Alpha)).r*_Color.a));
                fixed3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
