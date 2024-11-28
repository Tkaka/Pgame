// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "TF/Particles/AddtiveFresnelPan" {
    Properties {
        _Maintex ("Maintex", 2D) = "gray" {}
        _Color ("Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _Fresnel ("Fresnel", Range(0, 10)) = 5
        _ColorPower ("ColorPower", Float ) = 2
        [MaterialToggle] _OneMinus ("One Minus", Float ) = 0
        _Alpha ("Alpha", 2D) = "white" {}
        _USpeed ("USpeed", Float ) = 0
        _Vspeed ("Vspeed", Float ) = 0
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
            uniform float _USpeed;
            uniform float _Vspeed;
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
                float2 UV = i.uv0;
                float2 Speed1 = float2((UV.rg+(_Time.x*_USpeed)).r,(UV.rg+(_Time.x*_Vspeed)*float2(1,1)).g);
                half3 emissive = (pow(lerp((1.0 - Fre),Fre,_OneMinus),_Fresnel)*_Color.rgb*tex2D(_Maintex,TRANSFORM_TEX(Speed1, _Maintex)).rgb*_ColorPower*(tex2D(_Alpha,TRANSFORM_TEX(Speed1, _Alpha)).r*_Color.a));
                fixed3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"

}
