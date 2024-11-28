Shader "Fairy Tails/S_LightMap_Diffuse"
{
	Properties{
		_Color("Color Tint", Color) = (1, 1, 1, 1)
		_ColorPower("RColorPower", Float) = 1
		_LightMapPower("LightMapPower", Float) = 2
		_MainTex("Main Tex", 2D) = "white" {}
		_NoiseTex("NoiseTex (R)",2D) = "white"{}
		_Desaturate("Desaturate", Range(0, 1)) = 0
		//_DissolveSpeed("DissolveSpeed (Second)",Float) = 1
		_DissolveFactor("DissolveFactor",Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend("Src Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend("Dst Blend Mode", Float) = 0

		[Toggle(ENABLE_TRANSDISSOLVE)] _trans_dissolve("_trans_dissolve", Float) = 0
	}
		SubShader{
		LOD 200
		Tags
		{
			"Queue" = "Geometry"
			// 不接受投影吧， 统一把投影到导航上（Fairy Tails/S_OnProjectorMesh），也就是只有可行走路面才有影子
			// 解决多次投影的问题，还省
			"IgnoreProjector" = "True"
			"RenderType" = "Opaque"
		}
		Pass{
		Tags{ "LightMode" = "ForwardBase" }
		//Cull Off
		//Lighting Off
		//ZWrite Off
		//Fog{ Mode Off }
		//Offset - 1, -1
		Blend[_SrcBlend][_DstBlend]
		CGPROGRAM
#pragma multi_compile ENABLE_TRANSDISSOLVE
		//#pragma shader_feature ENABLE_OUTCOLLIDER
#pragma multi_compile_fog
#pragma multi_compile_fwdbase
#pragma fragmentoption ARB_precision_hint_fastest
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#include "AutoLight.cginc"
#include "Lighting.cginc"
	fixed4 _Color;
	sampler2D _MainTex;
	half4 _MainTex_ST;
	uniform fixed _ColorPower;
	uniform fixed _LightMapPower;
	uniform fixed _DissolveFactor;
	uniform fixed _Desaturate;

	uniform sampler2D _NoiseTex;
	//uniform fixed _DissolveSpeed;

	struct a2v {
		half4 vertex : POSITION;
		half4 texcoord : TEXCOORD0;
#ifdef LIGHTMAP_ON
		half4 texcoord1 : TEXCOORD1;//LightMap UV coord
#endif
	};
	struct v2f {
		half4 pos : SV_POSITION;
		half4 uv : TEXCOORD0;
		UNITY_FOG_COORDS(1)
	};

	v2f vert(a2v v) {
		v2f o;
		UNITY_INITIALIZE_OUTPUT(v2f, o);//多平台初始化，还可以消除警告
										//o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
#ifdef LIGHTMAP_ON
		o.uv.zw = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
#endif
		UNITY_TRANSFER_FOG(o, o.pos);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target{

#if ENABLE_TRANSDISSOLVE
	float noiseValue = 0;
	noiseValue = tex2D(_NoiseTex, i.uv).r;
	if (noiseValue <= _DissolveFactor)
	{
		discard;
	}
#endif

	fixed3 albedo = tex2D(_MainTex,i.uv.xy).rgb * _Color.rgb * _ColorPower;
	albedo.rgb = lerp(albedo, dot(albedo.rgb, float3(0.3, 0.59, 0.11)), _Desaturate);
#ifdef LIGHTMAP_ON
	half4 lm = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv.zw);
	fixed3 lmcolor = DecodeLightmap(lm);
	albedo.rgb = albedo.rgb * lmcolor * _LightMapPower;
#endif
	UNITY_APPLY_FOG(i.fogCoord, albedo);
#if ENABLE_TRANSDISSOLVE
	return  fixed4(albedo.rgb, lerp(1, 0, _DissolveFactor));
#endif
	return  fixed4(albedo.rgb, 1);
	}
		ENDCG
	}
	}
		FallBack "Legacy Shaders/Lightmapped/VertexLit"
}
