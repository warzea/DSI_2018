// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SHA_PulsingCircle"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
	}

	Category 
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane"  }

		
		SubShader
		{
			Blend One One
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
				#include "UnityShaderVariables.cginc"


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
					float temp_output_4_0 = distance( uv3 , float2( 0.5,0.5 ) );
					float mulTime10 = _Time.y * ( i.color.a * 4.0 );
					float temp_output_5_0 = step( temp_output_4_0 , (0.3 + (sin( mulTime10 ) - -1.0) * (0.5 - 0.3) / (1.0 - -1.0)) );
					float mulTime14 = _Time.y * ( i.color.a * 3.156 );
					float4 appendResult19 = (float4(( (i.color).rgb * temp_output_5_0 * ( temp_output_5_0 - step( temp_output_4_0 , (0.1 + (sin( mulTime14 ) - -1.0) * (0.28 - 0.1) / (1.0 - -1.0)) ) ) ) , 1.0));
					

					fixed4 col = appendResult19;
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
1927;29;1586;824;2948.08;224.5397;1.429839;True;True
Node;AmplifyShaderEditor.VertexColorNode;6;-2112.999,-91.87477;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-1811.358,397.4402;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;4.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1854.253,604.7668;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;3.156;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;14;-1613.692,680.0956;Float;False;1;0;FLOAT;3.156;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;10;-1610.313,471.215;Float;False;1;0;FLOAT;4.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1407.734,111.7333;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;2;-1382.734,255.7334;Float;False;Constant;_Vector0;Vector 0;0;0;Create;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SinOpNode;12;-1372.514,470.215;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;15;-1411.092,679.0956;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;13;-1207.18,460.3358;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;-1.0;False;2;FLOAT;1.0;False;3;FLOAT;0.3;False;4;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;4;-1087.233,138.4333;Float;False;2;0;FLOAT2;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;16;-1245.758,669.2165;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;-1.0;False;2;FLOAT;1.0;False;3;FLOAT;0.1;False;4;FLOAT;0.28;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;5;-866.1329,136.9333;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;8;-891.1857,262.5517;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.4;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;18;-1752.687,-88.24816;Float;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;9;-720.2863,230.1516;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-385.6333,-3.166664;Float;True;3;3;0;FLOAT3;0;False;1;FLOAT;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;-79.61856,17.27206;Float;False;FLOAT4;4;0;FLOAT3;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;1.0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMasterNode;1;257.7459,-15.56855;Float;False;True;2;Float;ASEMaterialInspector;0;5;Custom/SHA_PulsingCircle;0b6a9f8b4f707c74ca64c0be8e590de0;Particles Alpha Blended;4;One;One;0;One;Zero;Off;True;True;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;20;0;6;4
WireConnection;22;0;6;4
WireConnection;14;0;22;0
WireConnection;10;0;20;0
WireConnection;12;0;10;0
WireConnection;15;0;14;0
WireConnection;13;0;12;0
WireConnection;4;0;3;0
WireConnection;4;1;2;0
WireConnection;16;0;15;0
WireConnection;5;0;4;0
WireConnection;5;1;13;0
WireConnection;8;0;4;0
WireConnection;8;1;16;0
WireConnection;18;0;6;0
WireConnection;9;0;5;0
WireConnection;9;1;8;0
WireConnection;7;0;18;0
WireConnection;7;1;5;0
WireConnection;7;2;9;0
WireConnection;19;0;7;0
WireConnection;1;0;19;0
ASEEND*/
//CHKSM=3985877523175429BDF23481EB712DB131B0A958