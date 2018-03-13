// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SHA_Beam"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_T_Diform_02("T_Diform_02", 2D) = "white" {}
		_Texture0("Texture 0", 2D) = "white" {}
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
				uniform sampler2D _Texture0;
				uniform sampler2D _T_Diform_02;

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

					float2 uv2 = i.texcoord * float2( 1,1 ) + float2( 0,0 );
					float2 panner38 = ( ( uv2 * 0.7 ) + 1.0 * _Time.y * float2( 0,-0.6 ));
					float4 temp_cast_0 = 1;
					float4 temp_output_33_0 = ( ( tex2D( _T_Diform_02, panner38 ) * 2 ) - temp_cast_0 );
					float2 appendResult19 = (float2(uv2.y , uv2.x));
					float2 panner17 = ( ( ( temp_output_33_0 * 0.15 ) + float4( appendResult19, 0.0 , 0.0 ) ).rg + 1.0 * _Time.y * float2( -2,0 ));
					float smoothstepResult10 = smoothstep( 1.0 , 0.8 , abs( ( ( uv2.y * 2.0 ) + -1.0 ) ));
					float4 temp_cast_3 = 1;
					float temp_output_46_0 = ( uv2.y * 4.0 );
					float2 appendResult47 = (float2(temp_output_46_0 , uv2.x));
					float blendOpSrc50 = ( tex2D( _Texture0, panner17 ).r * smoothstepResult10 );
					float blendOpDest50 = ( tex2D( _Texture0, ( ( temp_output_33_0 * 0.05 ) + float4( appendResult47, 0.0 , 0.0 ) ).rg ).g * step( temp_output_46_0 , 1.0 ) * 2 );
					float temp_output_50_0 = ( saturate( max( blendOpSrc50, blendOpDest50 ) ));
					float4 lerpResult21 = lerp( float4(1,0.3517241,0,0) , float4( 1,1,1,0 ) , temp_output_50_0);
					

					fixed4 col = ( ( lerpResult21 * temp_output_50_0 ) * smoothstepResult10 * ( ( 1.0 - uv2.y ) + 1.0 ) );
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
1927;29;1586;824;2238.393;753.3327;1.801011;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-2228.695,-256.4464;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;40;-2146.943,-430.6194;Float;False;Constant;_Float1;Float 1;3;0;Create;0.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-1976.643,-422.8194;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;38;-1812.843,-511.2195;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-0.6;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;28;-1573.323,-698.4614;Float;True;Property;_T_Diform_02;T_Diform_02;2;0;Create;Assets/Art/FXShader/Texture/T_Diform_02.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;32;-1429.512,-510.9057;Float;False;Constant;_Int0;Int 0;3;0;Create;2;0;1;INT;0
Node;AmplifyShaderEditor.IntNode;34;-1226.512,-565.9056;Float;False;Constant;_Int1;Int 1;3;0;Create;1;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-1250.512,-695.9056;Float;False;2;2;0;COLOR;0.0;False;1;INT;0.007843138,0.007843138,0.007843138,0.007843138;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;33;-1061.511,-678.9056;Float;False;2;0;COLOR;0.0;False;1;INT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-1059.328,-492.4879;Float;False;Constant;_Float0;Float 0;3;0;Create;0.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;-875.6325,-360.0786;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-1014.966,458.0584;Float;False;Constant;_Float2;Float 2;3;0;Create;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-580,34;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;2.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-916.3278,-631.5879;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-1530.994,437.0703;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;4.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;47;-1250.076,380.0723;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-741.0114,-507.6061;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT2;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-383,47;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;-1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-871.9656,318.9584;Float;False;2;2;0;COLOR;0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.AbsOpNode;7;-217,44;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;43;-1103.928,92.90399;Float;True;Property;_Texture0;Texture 0;3;0;Create;Assets/Art/FXShader/Texture/T_Beam.tga;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleAddOpNode;54;-700.33,357.8909;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT2;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;17;-584.5048,-439.7138;Float;False;3;0;FLOAT2;1,1;False;2;FLOAT2;-2,0;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StepOpNode;48;-259.9384,643.0867;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;44;-360.6781,375.3477;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;Assets/Art/FXShader/Texture/T_Beam.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;16;-302.5184,-413.6336;Float;True;Property;_T_Beam;T_Beam;1;0;Create;Assets/Art/FXShader/Texture/T_Beam.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;55;-17.74608,696.4812;Float;False;Constant;_Int2;Int 2;3;0;Create;2;0;1;INT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;10;-57,43;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;1.0;False;2;FLOAT;0.8;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;24.78662,526.0728;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;INT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;87.92046,-263.6285;Float;False;2;2;0;FLOAT;0,0,0,0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;24;-256.8258,-636.8443;Float;False;Constant;_Color0;Color 0;2;0;Create;1,0.3517241,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendOpsNode;50;243.0585,-128.8569;Float;False;Lighten;True;2;0;FLOAT;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;41;-1321.698,-141.9447;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;21;232.0311,-513.1432;Float;False;3;0;COLOR;1,1,1,0;False;1;COLOR;1,1,1,0;False;2;FLOAT;0.0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;-1031.064,-124.2232;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;459.775,-357.7848;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;678.0557,-22.48197;Float;False;3;3;0;COLOR;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMasterNode;18;1046.903,-102.1283;Float;False;True;2;Float;ASEMaterialInspector;0;5;SHA_Beam;0b6a9f8b4f707c74ca64c0be8e590de0;Particles Alpha Blended;4;One;One;0;One;Zero;Off;True;True;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;39;0;2;0
WireConnection;39;1;40;0
WireConnection;38;0;39;0
WireConnection;28;1;38;0
WireConnection;31;0;28;0
WireConnection;31;1;32;0
WireConnection;33;0;31;0
WireConnection;33;1;34;0
WireConnection;19;0;2;2
WireConnection;19;1;2;1
WireConnection;8;0;2;2
WireConnection;36;0;33;0
WireConnection;36;1;37;0
WireConnection;46;0;2;2
WireConnection;47;0;46;0
WireConnection;47;1;2;1
WireConnection;35;0;36;0
WireConnection;35;1;19;0
WireConnection;6;0;8;0
WireConnection;57;0;33;0
WireConnection;57;1;56;0
WireConnection;7;0;6;0
WireConnection;54;0;57;0
WireConnection;54;1;47;0
WireConnection;17;0;35;0
WireConnection;48;0;46;0
WireConnection;44;0;43;0
WireConnection;44;1;54;0
WireConnection;16;0;43;0
WireConnection;16;1;17;0
WireConnection;10;0;7;0
WireConnection;49;0;44;2
WireConnection;49;1;48;0
WireConnection;49;2;55;0
WireConnection;45;0;16;1
WireConnection;45;1;10;0
WireConnection;50;0;45;0
WireConnection;50;1;49;0
WireConnection;41;0;2;2
WireConnection;21;0;24;0
WireConnection;21;2;50;0
WireConnection;42;0;41;0
WireConnection;27;0;21;0
WireConnection;27;1;50;0
WireConnection;11;0;27;0
WireConnection;11;1;10;0
WireConnection;11;2;42;0
WireConnection;18;0;11;0
ASEEND*/
//CHKSM=BAF9479D92EA09BAA11EDA9DA7F17EE98AAE9619