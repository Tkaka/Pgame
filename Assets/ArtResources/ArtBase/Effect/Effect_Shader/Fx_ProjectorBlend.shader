// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TF/FX/FX_Projector_Blend"
{
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "" {}
		_AlphaTex("Alpha (R Channel)", 2D) = "black" {}
	}
	Subshader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent"}
		
		Pass{
			ZWrite Off
			AlphaTest Greater 0
			Blend SrcAlpha OneMinusSrcAlpha
			Offset -1, -1
			Cull Back
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"

			struct v2f {
				float4 uvMain : TEXCOORD0;
				UNITY_FOG_COORDS(2)
				float4 pos : SV_POSITION;
			};

			half4x4 unity_Projector;
			fixed4 _Color;
			sampler2D _MainTex;
			half4 _MainTex_ST;
			sampler2D _AlphaTex;

			v2f vert(half4 vertex : POSITION)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(vertex);
				o.uvMain = mul(unity_Projector, vertex);
				o.uvMain.xy = o.uvMain.xy * (1 / _MainTex_ST.xy) + _MainTex_ST.zw;
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed3 texS = tex2Dproj(_MainTex, UNITY_PROJ_COORD(i.uvMain)).rgb;
				fixed  alpha = tex2D(_AlphaTex, i.uvMain).r;
				texS  *= _Color.rgb;
				alpha *= _Color.a;
				fixed4 res = fixed4(texS, alpha);
				UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(0,0,0,0));
				return res;
			}
		ENDCG
		}
	}
}
