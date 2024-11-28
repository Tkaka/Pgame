// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TF/ScenePanner" {
    Properties {
        _MaskMap ("MaskMap", 2D) = "white" {}
        _Color01 ("Color01", Color) = (0.5,0.5,0.5,1)
        _ColorPower01 ("ColorPower01", Float ) = 2
        _MainTex01 ("MainTex01", 2D) = "black" {}
        _USpeed ("USpeed", Float ) = 1
        _Vspeed ("Vspeed", Float ) = 1
        _Color02 ("Color02", Color) = (0.5,0.5,0.5,1)
        _ColorPower02 ("ColorPower02", Float ) = 2
        _MainTex02 ("MainTex02", 2D) = "black" {}
        _USpeed2 ("USpeed2", Float ) = 1
        _Vspeed2 ("Vspeed2", Float ) = 1
        _Lerp ("Lerp", Range(0, 1)) = 0
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
            Blend One One
            Cull Off
            ZWrite Off
            Fog {Mode Off}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 2.0
            uniform sampler2D _MainTex01; uniform fixed4 _MainTex01_ST;
            uniform sampler2D _MainTex02; uniform fixed4 _MainTex02_ST;
            uniform sampler2D _MaskMap; uniform fixed4 _MaskMap_ST;
            uniform float _USpeed;
            uniform float _Vspeed;
            uniform float _USpeed2;
            uniform float _Vspeed2;
            uniform fixed4 _Color01;
            uniform fixed _ColorPower01;
            uniform fixed _ColorPower02;
            uniform fixed4 _Color02;
            uniform fixed _Lerp;

            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 UV = i.uv0;
                float2 Speed1 = float2((UV.rg+(_Time.x*_USpeed)*float2(1,1)).r,(UV.rg+(_Time.x*_Vspeed)*float2(1,1)).g);
                float2 Speed2 = float2((UV.rg+(_Time.x*_USpeed2)*float2(1,1)).r,(UV.rg+(_Time.x*_Vspeed2)*float2(1,1)).g);
                float3 emissive = (lerp((_Color01.rgb*tex2D(_MainTex01,TRANSFORM_TEX(Speed1, _MainTex01)).rgb*_ColorPower01),(_Color02.rgb*_ColorPower02*tex2D(_MainTex02,TRANSFORM_TEX(Speed2, _MainTex02)).rgb),_Lerp)*tex2D(_MaskMap,TRANSFORM_TEX(UV.rg, _MaskMap)).rgb);
                float3 finalColor = emissive;
/// Final Color:
                return float4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
