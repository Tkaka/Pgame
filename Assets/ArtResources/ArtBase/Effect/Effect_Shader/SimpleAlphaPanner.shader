// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TF/SimpleAlphaPanner" {
    Properties {
        _Color01 ("Color01", Color) = (0.5,0.5,0.5,1)
        _ColorPower01 ("ColorPower01", Float ) = 2
        _MainTex01 ("MainTex01", 2D) = "black" {}
        _USpeed ("USpeed", Float ) = 1
        _Vspeed ("Vspeed", Float ) = 1
        _Mask1 ("Mask1", 2D) = "white" {}
        _AlphaBias ("AlphaBias", Range(0, 1)) = 0
        _ClipBias ("ClipBias", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+1"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 2.0
            uniform sampler2D _MainTex01; uniform float4 _MainTex01_ST;
            uniform float _USpeed;
            uniform float _Vspeed;
            uniform fixed4 _Color01;
            uniform float _ColorPower01;
            uniform sampler2D _Mask1; uniform float4 _Mask1_ST;
            uniform float _AlphaBias;
            uniform float _ClipBias;
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
                float2 UV = i.uv0;
                float2 Speed = float2((UV.rg+(_Time.r*_USpeed)*float2(1,1)).r,(UV.rg+(_Time.r*_Vspeed)*float2(1,1)).g);
                float4 A = tex2D(_Mask1,TRANSFORM_TEX(Speed, _Mask1));
                clip((A.r-_ClipBias) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (_Color01.rgb*tex2D(_MainTex01,TRANSFORM_TEX(Speed, _MainTex01)).rgb*_ColorPower01);
                float3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,(A.r-_AlphaBias));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"

}
