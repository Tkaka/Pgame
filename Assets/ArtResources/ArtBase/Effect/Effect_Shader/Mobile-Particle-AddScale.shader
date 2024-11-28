// Simplified Additive Particle shader. Differences from regular Additive Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask
Shader "TF/ParticlesScale/Mobile/AddScale" {
Properties {
	_MainTex ("Particle Texture", 2D) = "white" {}
	_Alpha ("Alpha", 2D) = "white" {}
	_Position ("Position", Vector) = (0,0,0,0)
	_Scale ("Scale", Vector) = (1,1,1,1)
	[Toggle] _NeedScale ("NeedScale", Float) = 1
}

Category {
	Tags { "Queue"="Transparent+1" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _Alpha;
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			
			float4 _MainTex_ST;
			float4 _Position;
			float4 _Scale;

			v2f vert (appdata_t v)
			{
				v2f o;
				float4 objV = mul(UNITY_MATRIX_MV, v.vertex);
				objV.xyz -= _Position;
				objV.xyz = float3(_Scale.x * objV.x, _Scale.y * objV.y, _Scale.z * objV.z);
				objV.xyz += _Position;
				o.vertex = mul(UNITY_MATRIX_P, objV);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			sampler2D _CameraDepthTexture;
			float _InvFade;
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				fixed4 Alpha = tex2D(_Alpha, i.texcoord);
				col.a = Alpha.r;
				
				return i.color * col;
			}
			ENDCG 
		}
	}	
}
}
