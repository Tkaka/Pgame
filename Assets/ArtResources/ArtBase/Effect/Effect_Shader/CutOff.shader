Shader "TF/Cutoff" {
	Properties {
		_MainTex ("MainTex", 2D) = "grey" {}
		_Color ("Color", Color) = (0.4980392,0.4980392,0.4980392,1)
		_Colorpower ("Colorpower", Float) = 2
		_Alpha ("Alpha", 2D) = "white" {}
		_Alphabias ("Alphabias", Range(0,1)) = 0
		_Clipbias ("Clipbias", Range(0,1)) = 0.335
	}
	SubShader {
		Tags { 
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			}
		LOD 200
		Cull Back
		ZWrite On
		alphatest Greater .01
		ZTest LEqual
		Blend SrcAlpha OneMinusSrcAlpha			
			
		CGPROGRAM
		#pragma surface surf Lambert noforwardadd

		sampler2D _MainTex;
		fixed4  _Color;
		fixed _Colorpower;
		sampler2D _Alpha;
		fixed _Clipbias;
		fixed _Alphabias;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Emission = _Color * _Colorpower.xxxx * c;
			o.Alpha = tex2D (_Alpha, IN.uv_MainTex) - _Alphabias;
			clip(tex2D (_Alpha, IN.uv_MainTex) - _Clipbias);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
