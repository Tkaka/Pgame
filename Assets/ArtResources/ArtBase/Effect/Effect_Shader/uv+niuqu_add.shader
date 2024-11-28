// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33647,y:32696,varname:node_4013,prsc:2|emission-7116-OUT;n:type:ShaderForge.SFN_Tex2d,id:4501,x:32607,y:32787,ptovrint:False,ptlb:node_4501,ptin:_node_4501,varname:_node_4501,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2f641eb1c34720e4b9ad184a972b8456,ntxv:3,isnm:False|UVIN-2453-UVOUT;n:type:ShaderForge.SFN_Panner,id:2453,x:32365,y:32803,varname:node_2453,prsc:2,spu:-0.002,spv:-0.01|UVIN-2886-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2886,x:32152,y:32803,varname:node_2886,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:9475,x:32819,y:32830,varname:node_9475,prsc:2|A-4501-R,B-8028-OUT;n:type:ShaderForge.SFN_Slider,id:8028,x:32682,y:33083,ptovrint:False,ptlb:node_8028,ptin:_node_8028,varname:_node_8028,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:6557,x:32848,y:32620,varname:node_6557,prsc:2|A-2020-UVOUT,B-9475-OUT;n:type:ShaderForge.SFN_TexCoord,id:2020,x:32483,y:32492,varname:node_2020,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:98,x:33113,y:32607,ptovrint:False,ptlb:node_98,ptin:_node_98,varname:_node_98,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1eb29a11980cb344eb348322e9569b04,ntxv:0,isnm:False|UVIN-6557-OUT;n:type:ShaderForge.SFN_Multiply,id:4132,x:33304,y:32730,varname:node_4132,prsc:2|A-98-RGB,B-706-RGB;n:type:ShaderForge.SFN_Multiply,id:7116,x:33424,y:32925,varname:node_7116,prsc:2|A-4132-OUT,B-9579-OUT;n:type:ShaderForge.SFN_Color,id:706,x:33070,y:32860,ptovrint:False,ptlb:node_706,ptin:_node_706,varname:_node_706,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Slider,id:9579,x:33106,y:33090,ptovrint:False,ptlb:node_9579,ptin:_node_9579,varname:_node_9579,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;proporder:4501-8028-98-706-9579;pass:END;sub:END;*/

Shader "Shader Forge/uv+niuqu" {
    Properties {
        _node_4501 ("node_4501", 2D) = "bump" {}
        _node_8028 ("node_8028", Range(0, 1)) = 1
        _node_98 ("node_98", 2D) = "white" {}
        _node_706 ("node_706", Color) = (0.5,0.5,0.5,1)
        _node_9579 ("node_9579", Range(0, 1)) = 1
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
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_4501; uniform float4 _node_4501_ST;
            uniform float _node_8028;
            uniform sampler2D _node_98; uniform float4 _node_98_ST;
            uniform float4 _node_706;
            uniform float _node_9579;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_4401 = _Time + _TimeEditor;
                float2 node_2453 = (i.uv0+node_4401.g*float2(-0.002,-0.01));
                float4 _node_4501_var = tex2D(_node_4501,TRANSFORM_TEX(node_2453, _node_4501));
                float2 node_6557 = (i.uv0+(_node_4501_var.r*_node_8028));
                float4 _node_98_var = tex2D(_node_98,TRANSFORM_TEX(node_6557, _node_98));
                float3 emissive = ((_node_98_var.rgb*_node_706.rgb)*_node_9579);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
