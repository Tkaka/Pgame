// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:True;n:type:ShaderForge.SFN_Final,id:1,x:36277,y:32712,varname:node_1,prsc:2|emission-4724-OUT,alpha-5744-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:34946,y:32605,ptovrint:False,ptlb:MainTex01,ptin:_MainTex01,varname:node_4486,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:2,isnm:False|UVIN-94-OUT;n:type:ShaderForge.SFN_Panner,id:17,x:34157,y:32519,varname:node_17,prsc:2,spu:1,spv:1|DIST-182-OUT;n:type:ShaderForge.SFN_ComponentMask,id:91,x:34389,y:32670,varname:node_91,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-17-UVOUT;n:type:ShaderForge.SFN_Append,id:94,x:34664,y:32729,varname:node_94,prsc:2|A-91-OUT,B-4632-OUT;n:type:ShaderForge.SFN_Tex2d,id:130,x:34855,y:33083,ptovrint:False,ptlb:MainTex02,ptin:_MainTex02,varname:node_2881,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:2,isnm:False|UVIN-4687-OUT;n:type:ShaderForge.SFN_Tex2d,id:132,x:35604,y:32861,ptovrint:False,ptlb:MaskMap,ptin:_MaskMap,varname:node_7609,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:151,x:33454,y:32597,varname:node_151,prsc:2;n:type:ShaderForge.SFN_Multiply,id:182,x:33875,y:32616,varname:node_182,prsc:2|A-151-TSL,B-183-OUT;n:type:ShaderForge.SFN_ValueProperty,id:183,x:33454,y:32762,ptovrint:False,ptlb:USpeed,ptin:_USpeed,varname:node_4109,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:4626,x:33872,y:32838,varname:node_4626,prsc:2|A-151-TSL,B-4628-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4628,x:33454,y:32853,ptovrint:False,ptlb:Vspeed,ptin:_Vspeed,varname:node_2672,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Panner,id:4630,x:34168,y:32778,varname:node_4630,prsc:2,spu:1,spv:1|DIST-4626-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4632,x:34375,y:32834,varname:node_4632,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-4630-UVOUT;n:type:ShaderForge.SFN_Panner,id:4655,x:34157,y:33225,varname:node_4655,prsc:2,spu:1,spv:1|DIST-4665-OUT;n:type:ShaderForge.SFN_Panner,id:4657,x:34146,y:32966,varname:node_4657,prsc:2,spu:1,spv:1|DIST-4663-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4659,x:34378,y:33117,varname:node_4659,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-4657-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:4661,x:34364,y:33281,varname:node_4661,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-4655-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4663,x:33872,y:33062,varname:node_4663,prsc:2|A-151-TSL,B-4667-OUT;n:type:ShaderForge.SFN_Multiply,id:4665,x:33860,y:33287,varname:node_4665,prsc:2|A-151-TSL,B-4669-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4667,x:33462,y:33109,ptovrint:False,ptlb:USpeed2,ptin:_USpeed2,varname:node_550,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:4669,x:33462,y:33226,ptovrint:False,ptlb:Vspeed2,ptin:_Vspeed2,varname:node_5665,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Append,id:4687,x:34602,y:33083,varname:node_4687,prsc:2|A-4659-OUT,B-4661-OUT;n:type:ShaderForge.SFN_Multiply,id:4724,x:36026,y:32807,varname:node_4724,prsc:2|A-5217-OUT,B-132-RGB;n:type:ShaderForge.SFN_Multiply,id:4763,x:35275,y:32483,varname:node_4763,prsc:2|A-4764-RGB,B-2-RGB,C-4765-OUT;n:type:ShaderForge.SFN_Color,id:4764,x:34946,y:32347,ptovrint:False,ptlb:Color01,ptin:_Color01,varname:node_2864,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:4765,x:34946,y:32520,ptovrint:False,ptlb:ColorPower01,ptin:_ColorPower01,varname:node_6515,prsc:2,glob:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:4771,x:35169,y:33044,varname:node_4771,prsc:2|A-4775-RGB,B-4773-OUT,C-130-RGB;n:type:ShaderForge.SFN_ValueProperty,id:4773,x:34783,y:32990,ptovrint:False,ptlb:ColorPower02,ptin:_ColorPower02,varname:node_6093,prsc:2,glob:False,v1:2;n:type:ShaderForge.SFN_Color,id:4775,x:34971,y:32851,ptovrint:False,ptlb:Color02,ptin:_Color02,varname:node_4507,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:5217,x:35718,y:32629,varname:node_5217,prsc:2|A-4763-OUT,B-4771-OUT,T-5219-OUT;n:type:ShaderForge.SFN_Slider,id:5219,x:35234,y:32701,ptovrint:False,ptlb:Lerp,ptin:_Lerp,varname:node_6675,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:2586,x:35133,y:33284,ptovrint:False,ptlb:Alpha1,ptin:_Alpha1,varname:node_2586,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5744,x:35873,y:33204,varname:node_5744,prsc:2|A-132-R,B-2586-R,C-3416-R;n:type:ShaderForge.SFN_Tex2d,id:3416,x:35490,y:33424,ptovrint:False,ptlb:Alpha2,ptin:_Alpha2,varname:node_3416,prsc:2,ntxv:0,isnm:False;proporder:132-4764-4765-2-183-4628-4775-4773-130-4667-4669-5219-2586-3416;pass:END;sub:END;*/

Shader "TF/FX_Panner3001" {
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
        _Alpha1 ("Alpha1", 2D) = "white" {}
        _Alpha2 ("Alpha2", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 2.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex01; uniform float4 _MainTex01_ST;
            uniform sampler2D _MainTex02; uniform float4 _MainTex02_ST;
            uniform sampler2D _MaskMap; uniform float4 _MaskMap_ST;
            uniform float _USpeed;
            uniform float _Vspeed;
            uniform float _USpeed2;
            uniform float _Vspeed2;
            uniform float4 _Color01;
            uniform float _ColorPower01;
            uniform float _ColorPower02;
            uniform float4 _Color02;
            uniform float _Lerp;
            uniform sampler2D _Alpha1; uniform float4 _Alpha1_ST;
            uniform sampler2D _Alpha2; uniform float4 _Alpha2_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_151 = _Time + _TimeEditor;
                float2 node_94 = float2((i.uv0+(node_151.r*_USpeed)*float2(1,1)).r,(i.uv0+(node_151.r*_Vspeed)*float2(1,1)).g);
                float4 _MainTex01_var = tex2D(_MainTex01,TRANSFORM_TEX(node_94, _MainTex01));
                float2 node_4687 = float2((i.uv0+(node_151.r*_USpeed2)*float2(1,1)).r,(i.uv0+(node_151.r*_Vspeed2)*float2(1,1)).g);
                float4 _MainTex02_var = tex2D(_MainTex02,TRANSFORM_TEX(node_4687, _MainTex02));
                float4 _MaskMap_var = tex2D(_MaskMap,TRANSFORM_TEX(i.uv0, _MaskMap));
                float3 emissive = (lerp((_Color01.rgb*_MainTex01_var.rgb*_ColorPower01),(_Color02.rgb*_ColorPower02*_MainTex02_var.rgb),_Lerp)*_MaskMap_var.rgb);
                float3 finalColor = emissive;
                float4 _Alpha1_var = tex2D(_Alpha1,TRANSFORM_TEX(i.uv0, _Alpha1));
                float4 _Alpha2_var = tex2D(_Alpha2,TRANSFORM_TEX(i.uv0, _Alpha2));
                return fixed4(finalColor,(_MaskMap_var.r*_Alpha1_var.r*_Alpha2_var.r));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
