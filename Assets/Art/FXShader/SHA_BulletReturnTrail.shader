// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32235,y:32601,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4625b8e7008af9c469f84b056718038b,ntxv:0,isnm:False|UVIN-5973-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_TexCoord,id:2289,x:31047,y:32164,varname:node_2289,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:3247,x:30604,y:32560,varname:node_3247,prsc:2,spu:1,spv:0.5|UVIN-4610-OUT;n:type:ShaderForge.SFN_Divide,id:9394,x:31333,y:32599,varname:node_9394,prsc:2|A-3376-OUT,B-2282-OUT;n:type:ShaderForge.SFN_Vector1,id:2282,x:31278,y:32802,varname:node_2282,prsc:2,v1:2;n:type:ShaderForge.SFN_Lerp,id:5973,x:31958,y:32408,varname:node_5973,prsc:2|A-2289-UVOUT,B-9394-OUT,T-2067-OUT;n:type:ShaderForge.SFN_Multiply,id:4610,x:30172,y:32567,varname:node_4610,prsc:2|A-2771-UVOUT,B-757-OUT;n:type:ShaderForge.SFN_ValueProperty,id:757,x:30068,y:32806,ptovrint:False,ptlb:Tile,ptin:_Tile,varname:node_757,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ScreenPos,id:2771,x:30172,y:32318,varname:node_2771,prsc:2,sctp:1;n:type:ShaderForge.SFN_Tex2d,id:6858,x:30838,y:32560,varname:node_6858,prsc:2,tex:e069a468d23da854bb29e51cfd86b2d7,ntxv:0,isnm:False|UVIN-3247-UVOUT,TEX-5174-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:5174,x:30604,y:32753,ptovrint:False,ptlb:Diform,ptin:_Diform,varname:node_5174,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e069a468d23da854bb29e51cfd86b2d7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:447,x:31729,y:32408,varname:node_447,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:8890,x:31542,y:32281,varname:node_8890,prsc:2|A-2289-U,B-5624-OUT;n:type:ShaderForge.SFN_Vector1,id:5624,x:31365,y:32397,varname:node_5624,prsc:2,v1:2;n:type:ShaderForge.SFN_Clamp01,id:3059,x:31729,y:32281,varname:node_3059,prsc:2|IN-8890-OUT;n:type:ShaderForge.SFN_OneMinus,id:3401,x:31365,y:32281,varname:node_3401,prsc:2|IN-2289-U;n:type:ShaderForge.SFN_Clamp01,id:2067,x:32081,y:32281,varname:node_2067,prsc:2|IN-2917-OUT;n:type:ShaderForge.SFN_Subtract,id:2917,x:31910,y:32281,varname:node_2917,prsc:2|A-3059-OUT,B-447-OUT;n:type:ShaderForge.SFN_Tex2d,id:9797,x:30838,y:32753,varname:node_9797,prsc:2,tex:e069a468d23da854bb29e51cfd86b2d7,ntxv:0,isnm:False|UVIN-3889-UVOUT,TEX-5174-TEX;n:type:ShaderForge.SFN_Panner,id:3889,x:30604,y:32924,varname:node_3889,prsc:2,spu:-0.1,spv:-0.1|UVIN-9816-OUT;n:type:ShaderForge.SFN_Vector1,id:589,x:30068,y:32958,varname:node_589,prsc:2,v1:2.1;n:type:ShaderForge.SFN_Multiply,id:9816,x:30423,y:32924,varname:node_9816,prsc:2|A-4610-OUT,B-2888-OUT;n:type:ShaderForge.SFN_Divide,id:2888,x:30256,y:32924,varname:node_2888,prsc:2|A-757-OUT,B-589-OUT;n:type:ShaderForge.SFN_Add,id:3376,x:31089,y:32634,varname:node_3376,prsc:2|A-6858-G,B-9797-R;proporder:6074-797-757-5174;pass:END;sub:END;*/

Shader "Shader Forge/SH_BulletReturnTrail" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _Tile ("Tile", Float ) = 1
        _Diform ("Diform", 2D) = "white" {}
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
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _Tile;
            uniform sampler2D _Diform; uniform float4 _Diform_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
////// Lighting:
////// Emissive:
                float4 node_2422 = _Time + _TimeEditor;
                float2 node_4610 = (float2(i.screenPos.x*(_ScreenParams.r/_ScreenParams.g), i.screenPos.y).rg*_Tile);
                float2 node_3247 = (node_4610+node_2422.g*float2(1,0.5));
                float4 node_6858 = tex2D(_Diform,TRANSFORM_TEX(node_3247, _Diform));
                float2 node_3889 = ((node_4610*(_Tile/2.1))+node_2422.g*float2(-0.1,-0.1));
                float4 node_9797 = tex2D(_Diform,TRANSFORM_TEX(node_3889, _Diform));
                float node_9394 = ((node_6858.g+node_9797.r)/2.0);
                float2 node_5973 = lerp(i.uv0,float2(node_9394,node_9394),saturate((saturate((i.uv0.r*2.0))-0.5)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_5973, _MainTex));
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*2.0);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
