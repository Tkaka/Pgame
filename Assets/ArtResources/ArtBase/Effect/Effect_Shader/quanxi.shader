// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:33153,y:32259,varname:node_9361,prsc:2|custl-3228-OUT,alpha-2278-A;n:type:ShaderForge.SFN_Color,id:4824,x:31969,y:32800,ptovrint:False,ptlb:node_4824,ptin:_node_4824,varname:node_4824,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1591695,c2:0.4406919,c3:0.9411765,c4:1;n:type:ShaderForge.SFN_Tex2d,id:2278,x:31969,y:32611,ptovrint:False,ptlb:node_2278,ptin:_node_2278,varname:node_2278,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:51e98e1a70e550f41bd6e7724d5f4494,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1694,x:32361,y:32431,varname:node_1694,prsc:2|A-4039-OUT,B-4824-RGB;n:type:ShaderForge.SFN_Slider,id:2844,x:31921,y:32971,ptovrint:False,ptlb:node_2844,ptin:_node_2844,varname:node_2844,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3,max:12;n:type:ShaderForge.SFN_Multiply,id:3228,x:32556,y:32431,varname:node_3228,prsc:2|A-1694-OUT,B-2844-OUT;n:type:ShaderForge.SFN_Tex2d,id:8688,x:31969,y:32431,ptovrint:False,ptlb:node_8688,ptin:_node_8688,varname:node_8688,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7a57e4702ad19254e91262ced434060f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:4039,x:32176,y:32431,varname:node_4039,prsc:2|A-8688-RGB,B-2278-RGB;proporder:4824-2278-2844-8688;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        [HDR]_node_4824 ("node_4824", Color) = (0.1591695,0.4406919,0.9411765,1)
        _node_2278 ("node_2278", 2D) = "white" {}
        _node_2844 ("node_2844", Range(0, 12)) = 3
        _node_8688 ("node_8688", 2D) = "white" {}
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
            Blend One One
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
            uniform float4 _node_4824;
            uniform sampler2D _node_2278; uniform float4 _node_2278_ST;
            uniform float _node_2844;
            uniform sampler2D _node_8688; uniform float4 _node_8688_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float4 _node_8688_var = tex2D(_node_8688,TRANSFORM_TEX(i.uv0, _node_8688));
                float4 _node_2278_var = tex2D(_node_2278,TRANSFORM_TEX(i.uv0, _node_2278));
                float3 finalColor = (((_node_8688_var.rgb*_node_2278_var.rgb)*_node_4824.rgb)*_node_2844);
                fixed4 finalRGBA = fixed4(finalColor,_node_2278_var.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
