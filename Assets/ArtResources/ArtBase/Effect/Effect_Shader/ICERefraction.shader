// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Shader created with Shader Forge Beta 0.35 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.35;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:0,limd:0,uamb:False,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:False,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:True,ufog:False,aust:False,igpj:False,qofs:2,qpre:3,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:True;n:type:ShaderForge.SFN_Final,id:0,x:32044,y:32303|normal-5430-OUT,emission-6441-OUT,alpha-6129-OUT,refract-14-OUT;n:type:ShaderForge.SFN_Slider,id:13,x:34160,y:32852,ptlb:Refraction Intensity,ptin:_RefractionIntensity,min:0,cur:0,max:0.2;n:type:ShaderForge.SFN_Multiply,id:14,x:33570,y:32807|A-5428-OUT,B-13-OUT;n:type:ShaderForge.SFN_Tex2d,id:25,x:34758,y:32282,ptlb:NormalMap,ptin:_NormalMap,tex:bbab0a6f7bae9cf42bf057d8ee2755f6,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Fresnel,id:217,x:34671,y:31829|NRM-6421-OUT;n:type:ShaderForge.SFN_Slider,id:5425,x:33516,y:32632,ptlb:AlphaBias,ptin:_AlphaBias,min:0,cur:0.3,max:1;n:type:ShaderForge.SFN_Multiply,id:5426,x:34505,y:32336|A-25-R,B-5431-OUT;n:type:ShaderForge.SFN_Multiply,id:5427,x:34519,y:32462|A-25-G,B-5431-OUT;n:type:ShaderForge.SFN_Append,id:5428,x:34326,y:32415|A-5426-OUT,B-5427-OUT;n:type:ShaderForge.SFN_Append,id:5430,x:34105,y:32465|A-5428-OUT,B-25-B;n:type:ShaderForge.SFN_ValueProperty,id:5431,x:34800,y:32544,ptlb:Normalpower,ptin:_Normalpower,glob:False,v1:1;n:type:ShaderForge.SFN_Color,id:5478,x:33212,y:31718,ptlb:MainColor,ptin:_MainColor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:5479,x:33233,y:31884,ptlb:ColorPower,ptin:_ColorPower,glob:False,v1:4;n:type:ShaderForge.SFN_Tex2d,id:5480,x:33289,y:31510,ptlb:MainTex,ptin:_MainTex,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5481,x:32785,y:31656|A-5480-RGB,B-5478-RGB,C-5479-OUT;n:type:ShaderForge.SFN_Power,id:5549,x:34355,y:31890|VAL-217-OUT,EXP-5575-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5575,x:34684,y:32016,ptlb:FresnelPower,ptin:_FresnelPower,glob:False,v1:2.5;n:type:ShaderForge.SFN_Cubemap,id:5809,x:33270,y:32010,ptlb:SpecularCube,ptin:_SpecularCube,cube:a596436b21c6d484bb9b3b6385e3e666,pvfc:0;n:type:ShaderForge.SFN_Slider,id:6083,x:33486,y:32169,ptlb:Shininess,ptin:_Shininess,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Clamp01,id:6129,x:32471,y:32482|IN-6725-OUT;n:type:ShaderForge.SFN_Power,id:6179,x:32803,y:32098|VAL-6181-OUT,EXP-6185-OUT;n:type:ShaderForge.SFN_Max,id:6181,x:32980,y:32028|A-6182-OUT,B-5809-RGB;n:type:ShaderForge.SFN_Vector1,id:6182,x:33136,y:31971,v1:0;n:type:ShaderForge.SFN_Multiply,id:6183,x:33247,y:32145|A-6083-OUT,B-6184-OUT;n:type:ShaderForge.SFN_Vector1,id:6184,x:33549,y:32287,v1:10;n:type:ShaderForge.SFN_Add,id:6185,x:33058,y:32224|A-6183-OUT,B-6186-OUT;n:type:ShaderForge.SFN_Vector1,id:6186,x:33181,y:32354,v1:1;n:type:ShaderForge.SFN_Color,id:6358,x:32763,y:32284,ptlb:SpecColor,ptin:_SpecColor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_NormalVector,id:6421,x:34975,y:31870,pt:False;n:type:ShaderForge.SFN_Add,id:6441,x:32341,y:31980|A-6702-OUT,B-6546-OUT;n:type:ShaderForge.SFN_Multiply,id:6546,x:32577,y:32031|A-6179-OUT,B-6358-RGB;n:type:ShaderForge.SFN_Multiply,id:6679,x:33076,y:31356|A-6680-RGB,B-5480-RGB,C-6720-OUT;n:type:ShaderForge.SFN_Color,id:6680,x:33438,y:31223,ptlb:EdgeColor,ptin:_EdgeColor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:6702,x:32815,y:31479|A-5481-OUT,B-6679-OUT,T-5549-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6720,x:33417,y:31399,ptlb:EdgePower,ptin:_EdgePower,glob:False,v1:8;n:type:ShaderForge.SFN_Add,id:6725,x:33231,y:32614|A-5549-OUT,B-5425-OUT;proporder:5478-5479-6680-6720-5480-13-5431-25-6358-6083-5809-5575-5425;pass:END;sub:END;*/

Shader "TF/ICERefraction" {
    Properties {
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _ColorPower ("ColorPower", Float ) = 4
        _EdgeColor ("EdgeColor", Color) = (0.5,0.5,0.5,1)
        _EdgePower ("EdgePower", Float ) = 8
        _MainTex ("MainTex", 2D) = "white" {}
        _RefractionIntensity ("Refraction Intensity", Range(0, 0.2)) = 0
        _Normalpower ("Normalpower", Float ) = 1
        _NormalMap ("NormalMap", 2D) = "bump" {}
        _SpecColor ("SpecColor", Color) = (1,1,1,1)
        _Shininess ("Shininess", Range(0, 10)) = 0
        _SpecularCube ("SpecularCube", Cube) = "_Skybox" {}
        _FresnelPower ("FresnelPower", Float ) = 2.5
        _AlphaBias ("AlphaBias", Range(0, 1)) = 0.3
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="Transparent+2"
            "RenderType"="TransparentCutout"
        }
        GrabPass{ }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles xbox360 ps3 flash 
            #pragma target 2.0
            uniform sampler2D _GrabTexture;
            uniform float _RefractionIntensity;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _AlphaBias;
            uniform float _Normalpower;
            uniform float4 _MainColor;
            uniform float _ColorPower;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _FresnelPower;
            uniform samplerCUBE _SpecularCube;
            uniform float _Shininess;
            uniform float4 _SpecColor;
            uniform float4 _EdgeColor;
            uniform float _EdgePower;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 node_6814 = i.uv0;
                float3 node_25 = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_6814.rg, _NormalMap)));
                float2 node_5428 = float2((node_25.r*_Normalpower),(node_25.g*_Normalpower));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (node_5428*_RefractionIntensity);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalLocal = float3(node_5428,node_25.b);
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 node_5480 = tex2D(_MainTex,TRANSFORM_TEX(node_6814.rg, _MainTex));
                float3 node_5481 = (node_5480.rgb*_MainColor.rgb*_ColorPower);
                float node_217 = (1.0-max(0,dot(i.normalDir, viewDirection)));
                float node_5549 = pow(node_217,_FresnelPower);
                float4 node_5809 = texCUBE(_SpecularCube,viewReflectDirection);
                float3 emissive = (lerp(node_5481,(_EdgeColor.rgb*node_5480.rgb*_EdgePower),node_5549)+(pow(max(0.0,node_5809.rgb),((_Shininess*10.0)+1.0))*_SpecColor.rgb));
                float3 finalColor = emissive;
/// Final Color:
                return fixed4(lerp(sceneColor.rgb, finalColor,saturate((node_5549+_AlphaBias))),1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
