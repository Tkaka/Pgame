// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge Beta 0.35 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.35;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:2,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:True;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|emission-4724-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:34119,y:32605,ptlb:MainTex01,ptin:_MainTex01,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:2,isnm:False|UVIN-94-OUT;n:type:ShaderForge.SFN_Panner,id:17,x:34908,y:32519,spu:1,spv:1|DIST-182-OUT;n:type:ShaderForge.SFN_ComponentMask,id:91,x:34676,y:32670,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-17-UVOUT;n:type:ShaderForge.SFN_Append,id:94,x:34401,y:32729|A-91-OUT,B-4632-OUT;n:type:ShaderForge.SFN_Tex2d,id:130,x:34210,y:33083,ptlb:MainTex02,ptin:_MainTex02,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:2,isnm:False|UVIN-4687-OUT;n:type:ShaderForge.SFN_Tex2d,id:132,x:33461,y:32861,ptlb:MaskMap,ptin:_MaskMap,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:151,x:35611,y:32597;n:type:ShaderForge.SFN_Multiply,id:182,x:35190,y:32616|A-151-TSL,B-183-OUT;n:type:ShaderForge.SFN_ValueProperty,id:183,x:35611,y:32762,ptlb:USpeed,ptin:_USpeed,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:4626,x:35193,y:32838|A-151-TSL,B-4628-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4628,x:35611,y:32853,ptlb:Vspeed,ptin:_Vspeed,glob:False,v1:1;n:type:ShaderForge.SFN_Panner,id:4630,x:34897,y:32778,spu:1,spv:1|DIST-4626-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4632,x:34690,y:32834,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-4630-UVOUT;n:type:ShaderForge.SFN_Panner,id:4655,x:34908,y:33225,spu:1,spv:1|DIST-4665-OUT;n:type:ShaderForge.SFN_Panner,id:4657,x:34919,y:32966,spu:1,spv:1|DIST-4663-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4659,x:34687,y:33117,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-4657-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:4661,x:34701,y:33281,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-4655-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4663,x:35193,y:33062|A-151-TSL,B-4667-OUT;n:type:ShaderForge.SFN_Multiply,id:4665,x:35205,y:33287|A-151-TSL,B-4669-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4667,x:35603,y:33109,ptlb:USpeed2,ptin:_USpeed2,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:4669,x:35603,y:33226,ptlb:Vspeed2,ptin:_Vspeed2,glob:False,v1:1;n:type:ShaderForge.SFN_Append,id:4687,x:34463,y:33083|A-4659-OUT,B-4661-OUT;n:type:ShaderForge.SFN_Multiply,id:4724,x:33039,y:32807|A-5217-OUT,B-132-RGB;n:type:ShaderForge.SFN_Multiply,id:4763,x:33790,y:32483|A-4764-RGB,B-2-RGB,C-4765-OUT;n:type:ShaderForge.SFN_Color,id:4764,x:34119,y:32347,ptlb:Color01,ptin:_Color01,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:4765,x:34119,y:32520,ptlb:ColorPower01,ptin:_ColorPower01,glob:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:4771,x:33896,y:33044|A-4775-RGB,B-4773-OUT,C-130-RGB;n:type:ShaderForge.SFN_ValueProperty,id:4773,x:34282,y:32990,ptlb:ColorPower02,ptin:_ColorPower02,glob:False,v1:2;n:type:ShaderForge.SFN_Color,id:4775,x:34094,y:32851,ptlb:Color02,ptin:_Color02,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:5217,x:33347,y:32629|A-4763-OUT,B-4771-OUT,T-5219-OUT;n:type:ShaderForge.SFN_Slider,id:5219,x:33674,y:32701,ptlb:Lerp,ptin:_Lerp,min:0,cur:0,max:1;proporder:132-4764-4765-2-183-4628-4775-4773-130-4667-4669-5219;pass:END;sub:END;*/

Shader "TF/ScenePanner3001" {
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
            "Queue"="Transparent+1"
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
            uniform sampler2D _MainTex01; uniform float4 _MainTex01_ST;
            uniform sampler2D _MainTex02; uniform float4 _MainTex02_ST;
            uniform sampler2D _MaskMap; uniform float4 _MaskMap_ST;
            uniform float _USpeed;
            uniform float _Vspeed;
            uniform float _USpeed2;
            uniform float _Vspeed2;
            uniform fixed4 _Color01;
            uniform float _ColorPower01;
            uniform float _ColorPower02;
            uniform fixed4 _Color02;
            uniform float _Lerp;
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
