Shader "Custom/CardShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_NoiseTex("NoiseTex", 2D) = "grey" {}
		_NoiseMaskTex("NoiseMaskTex", 2D) = "black" {}
		_NoiseStrength("NoiseStrength", Range(1,10)) = 2.5
		_FlickerColor("FlickerColor", Color) = (1,1,1,1)
		_FlickerSpeed("FlickerSpeed", Range(0,1000)) = 100
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _NoiseTex;
			sampler2D _NoiseMaskTex;

			float4 _MainTex_ST;
			fixed4 _FlickerColor;
			float _NoiseStrength;
			float _FlickerSpeed;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 distScroll = float2(_Time.x, _Time.x);
				fixed2 dist = (tex2D(_NoiseTex, i.uv + distScroll).rg - 0.5) * 2;
				fixed2 noiseTex = tex2D(_NoiseMaskTex, i.uv);
				fixed distMask = noiseTex.r;

				fixed4 col = tex2D(_MainTex, i.uv + dist * distMask * _NoiseStrength * 0.01);
				//fixed4 col = tex2D(_MainTex, i.uv + dist * distMask * 0.025);

				if (noiseTex.r > 0.1)
				{
					float level = 1 + sin(_Time.x * _FlickerSpeed);
					col = lerp(col, _FlickerColor, level*0.15);
				}

				return col;
			}
			ENDCG
		}
	}
}
