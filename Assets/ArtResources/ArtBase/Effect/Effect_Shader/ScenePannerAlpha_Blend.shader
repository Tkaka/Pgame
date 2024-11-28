// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TF/ScenePannerAlpha_Blend" {
    Properties {
        _MaskMap ("MaskMap", 2D) = "white" {}
        _Alpha ("Alpha", 2D) = "white" {}
        _Alphabias ("Alphabias", Range(0, 1)) = 0
        _Color01 ("Color01", Color) = (0.5,0.5,0.5,1)
        _ColorPower01 ("ColorPower01", Float ) = 2
        _MainTex01 ("MainTex01", 2D) = "black" {}
        _USpeed ("USpeed", Float ) = 1
        _Vspeed ("Vspeed", Float ) = 1
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
            uniform sampler2D _MainTex01; uniform float4 _MainTex01_ST;
            uniform sampler2D _MaskMap; uniform float4 _MaskMap_ST;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            uniform float _Alphabias;
            uniform float _USpeed;
            uniform float _Vspeed;
            uniform fixed4 _Color01;
            uniform float _ColorPower01;
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
                float3 emissive = _Color01.rgb*tex2D(_MainTex01,TRANSFORM_TEX(Speed1, _MainTex01)).rgb*_ColorPower01;
                float3 finalColor = emissive;
                float3 M = tex2D(_MaskMap,TRANSFORM_TEX(i.uv0, _MaskMap));
                return fixed4(finalColor,saturate(tex2D(_Alpha,TRANSFORM_TEX(Speed1, _MainTex01)).r-_Alphabias)*M.r);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
