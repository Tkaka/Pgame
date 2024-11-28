Shader "TF/ParticlesScale/KLClip_Add" {
Properties {
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Particle Texture", 2D) = "white" {}
	_Alpha ("Alpha", 2D) = "white" {}
	_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
	_Position ("Position", Vector) = (0,0,0,0)
	_Scale ("Scale", Vector) = (1,1,1,1)
	[Toggle] _NeedScale ("NeedScale", Float) = 1
	_ClipNois ("ClipNois", 2D) = "white" {}
    _Clip ("Clip", Range(-1, 1)) = -1
    [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}

Category {
	Tags { "Queue"="Transparent+1" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	//ColorMask RGB
	Cull Off Lighting Off ZWrite Off

	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _Alpha;
			fixed4 _TintColor;
			uniform float _Clip;
            uniform sampler2D _ClipNois; uniform float4 _ClipNois_ST;
			
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				#ifdef SOFTPARTICLES_ON
				float4 projPos : TEXCOORD1;
				#endif
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
				#ifdef SOFTPARTICLES_ON
				o.projPos = ComputeScreenPos (o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
				#endif
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			sampler2D _CameraDepthTexture;
			float _InvFade;
			
			fixed4 frag (v2f i) : COLOR
			{  
				#ifdef SOFTPARTICLES_ON
				float sceneZ = LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
				float partZ = i.projPos.z;
				float fade = saturate (_InvFade * (sceneZ-partZ));
				i.color.a *= fade;
				#endif
				fixed4 col = tex2D(_MainTex, i.texcoord);
				fixed4 Alpha = tex2D(_Alpha, i.texcoord);
				col.a = Alpha.r*_TintColor.a;
				float4 _ClipNois_var = tex2D(_ClipNois,i.texcoord);
                clip((_ClipNois_var.r-_Clip) - 0.5);
				return 2.0f * i.color * _TintColor * col;
			}
			ENDCG 
		}
	}	
}
}
