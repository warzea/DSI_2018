// Upgrade NOTE: upgraded instancing buffer 'CustomSHA_LightningInit' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SHA_LightningInit"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_T_Lightning("T_Lightning", 2D) = "white" {}
		_Color1("Color 1", Color) = (0.3382353,0.9178501,1,1)
	}

	Category 
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane"  }

		
		SubShader
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off

			Pass {
			
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#pragma multi_compile_instancing


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					UNITY_VERTEX_OUTPUT_STEREO
				};
				
				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform sampler2D_float _CameraDepthTexture;
				uniform float _InvFade;
				uniform sampler2D _T_Lightning;
				UNITY_INSTANCING_BUFFER_START(CustomSHA_LightningInit)
					UNITY_DEFINE_INSTANCED_PROP(float4, _Color1)
#define _Color1_arr CustomSHA_LightningInit
				UNITY_INSTANCING_BUFFER_END(CustomSHA_LightningInit)

				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = v.texcoord;
					o.texcoord.xy = TRANSFORM_TEX(v.texcoord,_MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif

					float2 uv3 = i.texcoord * float2( 1,1 ) + float2( 0,0 );
					float2 appendResult4 = (float2(( 1.0 - uv3.y ) , uv3.x));
					float4 tex2DNode1 = tex2D( _T_Lightning, appendResult4 );
					float4 _Color1_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color1_arr, _Color1);
					float4 lerpResult9 = lerp( _Color1_Instance , float4(1,1,1,1) , tex2DNode1.g);
					

					fixed4 col = ( step( tex2DNode1.r , i.color.r ) * tex2DNode1.b * lerpResult9 );
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
2328;194;955;495;1888.068;845.7421;2.045263;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1529,16;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;6;-1268,105;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;4;-1080,37;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;11;-581,-234;Float;False;InstancedProperty;_Color1;Color 1;1;0;Create;0.3382353,0.9178501,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;2;-694,235;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-574,-421;Float;False;Constant;_Color0;Color 0;1;0;Create;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-718,-11;Float;True;Property;_T_Lightning;T_Lightning;0;0;Create;Assets/Art/FXShader/Texture/T_Lightning.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;7;-359,39;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;9;-330,-148;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-143,48;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;COLOR;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMasterNode;5;116,53;Float;False;True;2;Float;ASEMaterialInspector;0;5;Custom/SHA_LightningInit;0b6a9f8b4f707c74ca64c0be8e590de0;Particles Alpha Blended;2;SrcAlpha;OneMinusSrcAlpha;0;One;Zero;Off;True;True;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;6;0;3;2
WireConnection;4;0;6;0
WireConnection;4;1;3;1
WireConnection;1;1;4;0
WireConnection;7;0;1;1
WireConnection;7;1;2;1
WireConnection;9;0;11;0
WireConnection;9;1;10;0
WireConnection;9;2;1;2
WireConnection;8;0;7;0
WireConnection;8;1;1;3
WireConnection;8;2;9;0
WireConnection;5;0;8;0
ASEEND*/
//CHKSM=204CF2637A762AA6A830330E71BBC9A3FF6D894B