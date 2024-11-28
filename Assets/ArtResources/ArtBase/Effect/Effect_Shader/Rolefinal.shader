// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "TF/RoleFinal" {
    Properties {
        //_Desaturate ("Desaturate", Range(0, 1)) = 0
        _Hit ("Hit", Float ) = 1
        _MainColor ("MainColor", Color) = (0.5019608,0.5019608,0.5019608,1)
        _ColorPower ("ColorPower", Float ) = 2
        _MainTex ("MainTex", 2D) = "black" {}
        _Alpha ("Alpha", 2D) = "white" {}
        _Alphabias ("Alphabias", Range(0, 1)) = 0
        //_ClipBias ("ClipBias", Range(0, 1)) = 0
        _ColorR ("ColorR", Color) = (1,1,1,1)
        _CubeMap ("CubeMap", Cube) = "_Skybox" {}
        _PannerColor ("PannerColor", Color) = (0,0,0,1)
        _PannerPower ("PannerPower", Float ) = 1
        _MaskMap ("MaskMap", 2D) = "black" {}
        _PanU ("PanU", Float ) = 1
        _PanV ("PanV", Float ) = 1
        _UTiling ("UTiling", Float ) = 1
        _VTiling ("VTiling", Float ) = 1
        _FresnelPower ("FresnelPower", Range(0, 5)) = 2
        _FresnelColor ("FresnelColor", Color) = (0,0,0,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase
            #pragma target 2.0
          
            uniform sampler2D _MainTex; uniform fixed4 _MainTex_ST;
            uniform fixed4 _ColorR;
            uniform samplerCUBE _CubeMap;
            uniform fixed4 _MainColor;
            uniform fixed4 _PannerColor;
            //uniform float _Desaturate;
            uniform sampler2D _MaskMap; uniform fixed4 _MaskMap_ST;
            uniform float _PanU;
            uniform float _PanV;
            //uniform fixed _ClipBias;
            uniform fixed _ColorPower;
            uniform fixed _PannerPower;
            uniform float _UTiling;
            uniform float _VTiling;
            uniform fixed _FresnelPower;
            uniform fixed4 _FresnelColor;
            uniform fixed _Alphabias;
            uniform fixed _Hit;
            uniform sampler2D _Alpha; uniform fixed4 _Alpha_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                //float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                //float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                //float3 tangentDir : TEXCOORD3;
                //float3 binormalDir : TEXCOORD4;                
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;               
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                //o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                //o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                fixed4 Alpha = tex2D(_Alpha,TRANSFORM_TEX(i.uv0, _Alpha));
                clip(saturate(Alpha.r) - 0.5);
                i.normalDir = normalize(i.normalDir);
                //float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection =  i.normalDir;                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); 
                i.normalDir *= nSign;
                normalDirection *= nSign;
                fixed4 col = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                fixed4 m = tex2D(_MaskMap,TRANSFORM_TEX(i.uv0.rg, _MaskMap));
                fixed3 c = lerp(col.rgb,(col.rgb*_ColorR.rgb),m.r);
                float2 UV2 = i.uv0;
                float4 T = _Time ;
                float Uspeed = (T.r*_PanU);
                float Vspeed = (T.r*_PanV);
                float2 UVspeed = (float2((UV2.r*_UTiling),(UV2.g*_VTiling))+float2(Uspeed,Vspeed));
                float3 Pan = (_PannerColor.rgb*tex2D(_MaskMap,TRANSFORM_TEX(UVspeed, _MaskMap)).b*_PannerPower);
                float3 emissive = (saturate((c+(m.g*Pan)))*(texCUBE(_CubeMap,mul( UNITY_MATRIX_V, float4(i.normalDir,0) ).xyz.rgb).rgb*_MainColor.rgb*_ColorPower)*_Hit)+(pow((1.0-max(0,dot(normalDirection, viewDirection))),_FresnelPower)*_FresnelColor.rgb*2.0);
                //float3 emissive = lerp(((saturate((c+(m.g*Pan)))*(texCUBE(_CubeMap,mul( UNITY_MATRIX_V, float4(i.normalDir,0) ).xyz.rgb).rgb*_MainColor.rgb*_ColorPower)*_Hit)+(pow((1.0-max(0,dot(normalDirection, viewDirection))),_FresnelPower)*_FresnelColor.rgb*2.0)),dot(((saturate((c+(m.g*Pan)))*(texCUBE(_CubeMap,mul( UNITY_MATRIX_V, float4(i.normalDir,0) ).xyz.rgb).rgb*_MainColor.rgb*_ColorPower)*_Hit)+(pow((1.0-max(0,dot(normalDirection, viewDirection))),_FresnelPower)*_FresnelColor.rgb*2.0)),float3(0.3,0.59,0.11)),_Desaturate);
                float3 finalColor = emissive;
                return fixed4(finalColor,saturate((Alpha.r-_Alphabias)));
            }
            ENDCG
        }        
    }
    FallBack "VertexLit"
}
