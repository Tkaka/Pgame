// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:True,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:True;n:type:ShaderForge.SFN_Final,id:1,x:32235,y:32724|emission-7-OUT,alpha-5795-OUT;n:type:ShaderForge.SFN_Fresnel,id:2,x:33535,y:32381;n:type:ShaderForge.SFN_Tex2d,id:3,x:33254,y:33115,ptlb:Maintex,ptin:_Maintex,tex:b2cff2f097b7cad469c09aa78573971c,ntxv:1,isnm:False;n:type:ShaderForge.SFN_Color,id:4,x:33158,y:32848,ptlb:Color,ptin:_Color,glob:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Power,id:5,x:32601,y:32588|VAL-31-OUT,EXP-6-OUT;n:type:ShaderForge.SFN_Slider,id:6,x:33121,y:32695,ptlb:Fresnel,ptin:_Fresnel,min:0,cur:3.611163,max:10;n:type:ShaderForge.SFN_Multiply,id:7,x:32738,y:32678|A-4-RGB,B-3-RGB,C-16-OUT;n:type:ShaderForge.SFN_ValueProperty,id:16,x:33073,y:33280,ptlb:ColorPower,ptin:_ColorPower,glob:False,v1:2;n:type:ShaderForge.SFN_ToggleProperty,id:21,x:32996,y:32616,ptlb:One Minus,ptin:_OneMinus,on:False;n:type:ShaderForge.SFN_OneMinus,id:22,x:33147,y:32325|IN-2-OUT;n:type:ShaderForge.SFN_Lerp,id:31,x:32782,y:32456|A-22-OUT,B-2-OUT,T-21-OUT;n:type:ShaderForge.SFN_Tex2d,id:5784,x:33109,y:30457,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False|TEX-5790-TEX;n:type:ShaderForge.SFN_Multiply,id:5786,x:32652,y:30624|A-5784-RGB,B-5788-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5788,x:33084,y:30622,ptlb:Detailpower,ptin:_Detailpower,glob:False,v1:2;n:type:ShaderForge.SFN_Tex2dAsset,id:5790,x:33533,y:30457,ptlb:Detail,ptin:_Detail,glob:False,tex:b66bceaf0cc0ace4e9bdc92f14bba709;n:type:ShaderForge.SFN_Tex2d,id:5792,x:33229,y:30669,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False|TEX-5790-TEX;n:type:ShaderForge.SFN_Lerp,id:5794,x:32657,y:30925|B-5786-OUT,T-5792-A;n:type:ShaderForge.SFN_Multiply,id:5795,x:32577,y:32967|A-5-OUT,B-5801-R,C-4-A;n:type:ShaderForge.SFN_Tex2d,id:5801,x:33371,y:32828,ptlb:Alpha,ptin:_Alpha,ntxv:0,isnm:False;proporder:3-4-6-16-21-5801;pass:END;sub:END;*/

Shader "TF/AlphaBFresnel" {
    Properties {
        _Maintex ("Maintex", 2D) = "gray" {}
        _Color ("Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _Fresnel ("Fresnel", Range(0, 10)) = 3.611163
        _ColorPower ("ColorPower", Float ) = 2
        [MaterialToggle] _OneMinus ("One Minus", Float ) = 0
        _Alpha ("Alpha", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
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
                float2 IUV = i.uv0;
                half3 emissive = (_Color.rgb*tex2D(_Maintex,TRANSFORM_TEX(IUV.rg, _Maintex)).rgb*_ColorPower);
                half3 finalColor = emissive;
                float Fre = (1.0-max(0,dot(normalDirection, viewDirection)));
/// Final Color:
                return fixed4(finalColor,(pow(lerp((1.0 - Fre),Fre,_OneMinus),_Fresnel)*tex2D(_Alpha,TRANSFORM_TEX(IUV.rg, _Alpha)).r*_Color.a));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
