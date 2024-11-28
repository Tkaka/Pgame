// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Mobile/Particles/My_2layer_roll_warping_Alpha_yanjiang_nofog" {
Properties {
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_ScendColor("Scend Color", Color) = (1,1,1,1)
	_MainTex ("Main Texture", 2D) = "white" {}
	_ScendTex("ScendTex Texture", 2D) = "white" {}
	_WiggleTex ("WiggleTex", 2D) = "white" {}
	_WiggleStrength ("Wiggle Strength", Range (0, 0.1)) = 0.01
	_Speedxy("speed x y z w",vector)= (0,0,0,0)
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	//Blend SrcAlpha One
	Blend SrcAlpha OneMinusSrcAlpha
	//ColorMask RGB
	 //ZWrite Off 
	//Fog {Mode Off}
	SubShader {
		Pass {		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag	

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _TintColor;
			sampler2D _WiggleTex;
			float4 _WiggleTex_ST;
			sampler2D _ScendTex;
			float4 _ScendTex_ST;
			float _WiggleStrength;
			float4 _Speedxy;
			float4 _ScendColor;
			struct appdata_t {
				half4 vertex : POSITION;
				fixed4 color : COLOR;
				half2 texcoord : TEXCOORD0;
				half4 texcoord1 : TEXCOORD1;
			};

			struct v2f {
				half4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				half4 texcoord1 : TEXCOORD1;

			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				o.texcoord1.xy = TRANSFORM_TEX(v.texcoord,_ScendTex);
				o.texcoord1.zw = TRANSFORM_TEX(v.texcoord,_WiggleTex);
				return o;
			}


			
			fixed4 frag (v2f i) : COLOR
			{
				
				fixed2 tc2 = i.texcoord1.zw;
				tc2.x -= _SinTime;
				tc2.y += _CosTime;
				fixed4 wiggle = tex2D(_WiggleTex, tc2);
				half2 uvpan = half2 (i.texcoord.x - wiggle.r * _WiggleStrength+_Time.x *_Speedxy.x , i.texcoord.y + wiggle.b * _WiggleStrength*1.5f+_Time.y*_Speedxy.y);
				half2 uvpan2 = half2 (i.texcoord1.x - wiggle.r * _WiggleStrength+_Time.x *_Speedxy.z , i.texcoord1.y + wiggle.b * _WiggleStrength*1.5f+_Time.y*_Speedxy.w);
				fixed4 col = tex2D(_MainTex, uvpan);
				fixed4 col2 = tex2D(_ScendTex, uvpan2) *_ScendColor;
				fixed4 col3 = fixed4 (col.rgb +col2.rgb ,col.a);
				
				return   _TintColor *col3;
			}
			ENDCG 
		}
	}	
}
}
