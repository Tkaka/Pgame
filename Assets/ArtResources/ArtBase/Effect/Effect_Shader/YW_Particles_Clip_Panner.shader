// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.13 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.13;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:0,nrsp:0,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7921,x:33890,y:32716,varname:node_7921,prsc:2|emission-9762-OUT,alpha-4671-OUT,clip-4428-OUT;n:type:ShaderForge.SFN_Tex2d,id:7134,x:32798,y:32814,ptovrint:False,ptlb:Diffuse_Map,ptin:_Diffuse_Map,varname:node_7134,prsc:2,tex:b38e79366536c8749bd1222b335b5db1,ntxv:0,isnm:False|UVIN-1861-OUT;n:type:ShaderForge.SFN_Panner,id:2424,x:32085,y:32819,varname:node_2424,prsc:2,spu:1,spv:0|DIST-9819-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5862,x:31605,y:32778,ptovrint:False,ptlb:U_Speed,ptin:_U_Speed,varname:node_5862,prsc:2,glob:False,v1:5;n:type:ShaderForge.SFN_Time,id:2714,x:31605,y:32947,varname:node_2714,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9819,x:31852,y:32840,varname:node_9819,prsc:2|A-5862-OUT,B-2714-TSL;n:type:ShaderForge.SFN_Multiply,id:352,x:31871,y:33139,varname:node_352,prsc:2|A-2241-OUT,B-2714-TSL;n:type:ShaderForge.SFN_ValueProperty,id:2241,x:31621,y:33199,ptovrint:False,ptlb:V_Speed,ptin:_V_Speed,varname:_U_Speed_copy,prsc:2,glob:False,v1:0;n:type:ShaderForge.SFN_Panner,id:7048,x:32099,y:33125,varname:node_7048,prsc:2,spu:0,spv:1|DIST-352-OUT;n:type:ShaderForge.SFN_ComponentMask,id:7138,x:32329,y:32782,varname:node_7138,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-2424-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:3025,x:32346,y:33203,varname:node_3025,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-7048-UVOUT;n:type:ShaderForge.SFN_Append,id:1861,x:32575,y:32969,varname:node_1861,prsc:2|A-7138-OUT,B-3025-OUT;n:type:ShaderForge.SFN_Tex2d,id:6296,x:32871,y:33358,ptovrint:False,ptlb:Noise_Map,ptin:_Noise_Map,varname:node_6296,prsc:2,tex:b7900aa76d6ee5e418f94d0b9ac572ce,ntxv:0,isnm:False|UVIN-1861-OUT;n:type:ShaderForge.SFN_Subtract,id:4428,x:33379,y:33389,varname:node_4428,prsc:2|A-843-OUT,B-6296-B;n:type:ShaderForge.SFN_RemapRange,id:843,x:33330,y:33128,varname:node_843,prsc:2,frmn:0,frmx:1,tomn:0.45,tomx:1.5|IN-9987-R;n:type:ShaderForge.SFN_VertexColor,id:9987,x:32862,y:33074,varname:node_9987,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9762,x:33239,y:32757,varname:node_9762,prsc:2|A-8713-OUT,B-9987-RGB;n:type:ShaderForge.SFN_Multiply,id:4671,x:33330,y:32925,varname:node_4671,prsc:2|A-7134-A,B-9987-A,C-2063-B;n:type:ShaderForge.SFN_Multiply,id:8713,x:33163,y:32611,varname:node_8713,prsc:2|A-317-OUT,B-504-RGB,C-5644-OUT;n:type:ShaderForge.SFN_Color,id:504,x:32864,y:32460,ptovrint:False,ptlb:Diffuse_Col,ptin:_Diffuse_Col,varname:node_504,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:5644,x:32864,y:32382,ptovrint:False,ptlb:Diffuse_Pow,ptin:_Diffuse_Pow,varname:node_5644,prsc:2,glob:False,v1:2;n:type:ShaderForge.SFN_Tex2d,id:2063,x:32798,y:32638,ptovrint:False,ptlb:Mask_Map,ptin:_Mask_Map,varname:node_2063,prsc:2,tex:2c0c812bc4e60c84db16020011226d55,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:317,x:32975,y:32655,varname:node_317,prsc:2|A-2063-RGB,B-7134-RGB;proporder:2063-5644-504-7134-5862-2241-6296;pass:END;sub:END;*/

Shader "TF/YW_Particles_Clip_Panner" {
    Properties {
        _Mask_Map ("Mask_Map", 2D) = "white" {}
        _Diffuse_Pow ("Diffuse_Pow", Float ) = 2
        _Diffuse_Col ("Diffuse_Col", Color) = (0.5,0.5,0.5,1)
        _Diffuse_Map ("Diffuse_Map", 2D) = "white" {}
        _U_Speed ("U_Speed", Float ) = 5
        _V_Speed ("V_Speed", Float ) = 0
        _Noise_Map ("Noise_Map", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Diffuse_Map; uniform float4 _Diffuse_Map_ST;
            uniform float _U_Speed;
            uniform float _V_Speed;
            uniform sampler2D _Noise_Map; uniform float4 _Noise_Map_ST;
            uniform float4 _Diffuse_Col;
            uniform float _Diffuse_Pow;
            uniform sampler2D _Mask_Map; uniform float4 _Mask_Map_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float4 node_2714 = _Time + _TimeEditor;
                float2 node_2424 = (i.uv0+(_U_Speed*node_2714.r)*float2(1,0));
                float2 node_1861 = float2(node_2424.r,(i.uv0+(_V_Speed*node_2714.r)*float2(0,1)).g);
                float4 _Noise_Map_var = tex2D(_Noise_Map,TRANSFORM_TEX(node_1861, _Noise_Map));
                clip(((i.vertexColor.r*1.05+0.45)-_Noise_Map_var.b) - 0.5);
////// Lighting:
////// Emissive:
                float4 _Mask_Map_var = tex2D(_Mask_Map,TRANSFORM_TEX(i.uv0, _Mask_Map));
                float4 _Diffuse_Map_var = tex2D(_Diffuse_Map,TRANSFORM_TEX(node_1861, _Diffuse_Map));
                float3 emissive = (((_Mask_Map_var.rgb*_Diffuse_Map_var.rgb)*_Diffuse_Col.rgb*_Diffuse_Pow)*i.vertexColor.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_Diffuse_Map_var.a*i.vertexColor.a*_Mask_Map_var.b));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _U_Speed;
            uniform float _V_Speed;
            uniform sampler2D _Noise_Map; uniform float4 _Noise_Map_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float4 node_2714 = _Time + _TimeEditor;
                float2 node_2424 = (i.uv0+(_U_Speed*node_2714.r)*float2(1,0));
                float2 node_1861 = float2(node_2424.r,(i.uv0+(_V_Speed*node_2714.r)*float2(0,1)).g);
                float4 _Noise_Map_var = tex2D(_Noise_Map,TRANSFORM_TEX(node_1861, _Noise_Map));
                clip(((i.vertexColor.r*1.05+0.45)-_Noise_Map_var.b) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
