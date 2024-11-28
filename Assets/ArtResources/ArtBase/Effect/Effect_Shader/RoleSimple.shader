// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Shader created with Shader Forge Beta 0.35 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.35;sub:START;pass:START;ps:flbk:VertexLit,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:True,lprd:True,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:True,tesm:0,blpr:5,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:True,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1280277,fgcg:0.1953466,fgcb:0.2352941,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:10,ofsu:0,f2p0:True;n:type:ShaderForge.SFN_Final,id:1,x:31045,y:32454|emission-2610-OUT,alpha-3690-OUT,clip-132-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32423,y:31774,ptlb:MainTex,ptin:_MainTex,tex:66321cc856b03e245ac41ed8a53e0ecc,ntxv:1,isnm:False;n:type:ShaderForge.SFN_Clamp01,id:132,x:31974,y:32662|IN-2656-OUT;n:type:ShaderForge.SFN_Cubemap,id:1178,x:33995,y:32561,ptlb:CubeMap,ptin:_CubeMap,cube:6472564b09fac74498ba9b4fa1252e3a,pvfc:0|DIR-3150-XYZ;n:type:ShaderForge.SFN_Multiply,id:1372,x:33768,y:32654|A-1178-RGB,B-1373-RGB,C-3037-OUT;n:type:ShaderForge.SFN_Color,id:1373,x:33995,y:32716,ptlb:MainColor,ptin:_MainColor,glob:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Multiply,id:1560,x:31699,y:31940|A-2-RGB,B-1372-OUT,C-3732-OUT;n:type:ShaderForge.SFN_Desaturate,id:2610,x:31338,y:32497|COL-3707-OUT,DES-2626-OUT;n:type:ShaderForge.SFN_Slider,id:2626,x:31624,y:32446,ptlb:Desaturate,ptin:_Desaturate,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Subtract,id:2656,x:32190,y:32636|A-2-A,B-3008-OUT;n:type:ShaderForge.SFN_Slider,id:3008,x:32439,y:32723,ptlb:ClipBias,ptin:_ClipBias,min:0,cur:0,max:1;n:type:ShaderForge.SFN_ValueProperty,id:3037,x:33995,y:32878,ptlb:ColorPower,ptin:_ColorPower,glob:False,v1:2;n:type:ShaderForge.SFN_Transform,id:3150,x:34205,y:32561,tffrom:0,tfto:3|IN-3151-OUT;n:type:ShaderForge.SFN_NormalVector,id:3151,x:34450,y:32523,pt:False;n:type:ShaderForge.SFN_Fresnel,id:3535,x:33646,y:32126;n:type:ShaderForge.SFN_Slider,id:3537,x:33485,y:32279,ptlb:FresnelPower,ptin:_FresnelPower,min:0,cur:2,max:5;n:type:ShaderForge.SFN_Power,id:3539,x:33224,y:32011|VAL-3535-OUT,EXP-3537-OUT;n:type:ShaderForge.SFN_Color,id:3541,x:32975,y:32187,ptlb:FresnelColor,ptin:_FresnelColor,glob:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Vector1,id:3543,x:32975,y:32329,v1:2;n:type:ShaderForge.SFN_Multiply,id:3545,x:32629,y:32041|A-3539-OUT,B-3541-RGB,C-3543-OUT;n:type:ShaderForge.SFN_Clamp01,id:3690,x:32019,y:32503|IN-3692-OUT;n:type:ShaderForge.SFN_Subtract,id:3692,x:32194,y:32349|A-2-A,B-3694-OUT;n:type:ShaderForge.SFN_Slider,id:3694,x:32429,y:32533,ptlb:Alphabias,ptin:_Alphabias,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:3707,x:31703,y:32226|A-1560-OUT,B-3545-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3732,x:32078,y:32106,ptlb:Hit,ptin:_Hit,glob:False,v1:1;proporder:2626-3732-1373-3037-2-3694-3008-1178-3537-3541;pass:END;sub:END;*/

Shader "TF/RoleSmiple" {
    Properties {
        //_Desaturate ("Desaturate", Range(0, 1)) = 0
        _Hit ("Hit", Float ) = 1
        _MainColor ("MainColor", Color) = (0.5019608,0.5019608,0.5019608,1)
        _ColorPower ("ColorPower", Float ) = 2
        _MainTex ("MainTex", 2D) = "gray" {}
        _Alpha ("Alpha", 2D) = "white" {}
        _Alphabias ("Alphabias", Range(0, 1)) = 0
        //_ClipBias ("ClipBias", Range(0, 1)) = 0
        _CubeMap ("CubeMap", Cube) = "_Skybox" {}
        _FresnelPower ("FresnelPower", Range(0, 5)) = 2
        _FresnelColor ("FresnelColor", Color) = (0,0,0,1)
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
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            //#include "Lighting.cginc"
            #pragma multi_compile_fwdbase
            //#pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 2.0
            //#ifndef LIGHTMAP_OFF
                
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform samplerCUBE _CubeMap;
            uniform fixed4 _MainColor;
            //uniform fixed _Desaturate;
            //uniform fixed _ClipBias;
            uniform fixed _ColorPower;
            uniform fixed _FresnelPower;
            uniform fixed4 _FresnelColor;
            uniform fixed _Alphabias;
            uniform fixed _Hit;
            uniform sampler2D _Alpha; uniform fixed4 _Alpha_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                //float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                //float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                //float3 tangentDir : TEXCOORD3;
                //float3 binormalDir : TEXCOORD4;
                
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                //o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                //o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);

                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                fixed4 Alpha = tex2D(_Alpha,TRANSFORM_TEX(i.uv0, _Alpha));
                fixed4 d = tex2D(_MainTex,TRANSFORM_TEX(i.uv0.rg, _MainTex));
                clip(saturate(Alpha.r) - 0.8);
                i.normalDir = normalize(i.normalDir);
                //fixed3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                fixed3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                fixed3 normalDirection =  i.normalDir;
                
                fixed nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                
////// Lighting:
////// Emissive:
				fixed3 shorted = ((d.rgb*(texCUBE(_CubeMap,mul( UNITY_MATRIX_V, float4(i.normalDir,0) ).xyz.rgb).rgb*_MainColor*_ColorPower)*_Hit)+(pow((1.0-max(0,dot(normalDirection, viewDirection))),_FresnelPower)*_FresnelColor.rgb*2.0));
                //fixed3 emissive = lerp(shorted,dot(shorted,float3(0.3,0.59,0.11)),_Desaturate);
                fixed3 finalColor = shorted;
/// Final Color:
                return fixed4(finalColor,saturate((Alpha.r-_Alphabias)));
            }
            ENDCG
        }

    }
    FallBack "VertexLit"
    CustomEditor "ShaderForgeMaterialInspector"
}
