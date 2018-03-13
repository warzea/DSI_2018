// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SHA_trail01"
{
	Properties
	{
		_Texture0("Texture 0", 2D) = "white" {}
		_Offset("Offset", Vector) = (0,0,0,0)
		_Int("Int", Float) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "DisableBatching" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 4.6
		struct Input
		{
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _Texture0;
		uniform float2 _Offset;
		uniform float _Int;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float2 appendResult27 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 temp_output_57_0 = ( appendResult27 + _Offset );
			float2 panner40 = ( temp_output_57_0 + 1.0 * _Time.y * float2( 0.1,0.1 ));
			float4 tex2DNode31 = tex2Dlod( _Texture0, float4( ( panner40 * float2( 0.5,0.5 ) ), 0, 0.0) );
			float2 panner50 = ( ( temp_output_57_0 * float2( 0.2,0.23 ) ) + 1.0 * _Time.y * float2( -0.04,-0.3 ));
			float4 tex2DNode47 = tex2Dlod( _Texture0, float4( panner50, 0, 0.0) );
			float2 appendResult52 = (float2(tex2DNode31.r , tex2DNode31.g));
			float2 appendResult53 = (float2(tex2DNode47.b , tex2DNode47.r));
			float3 appendResult67 = (float3(( tex2DNode31.b + 0.0 + tex2DNode47.g ) , ( appendResult52 + appendResult53 )));
			float3 temp_cast_0 = 1;
			float2 uv_TexCoord5 = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
			float clampResult36 = clamp( ( uv_TexCoord5.x * 0.2 ) , 0.0 , 5.0 );
			float mulTime38 = _Time.y * 0.007;
			float3 temp_cast_1 = 1;
			float3 temp_cast_2 = 1;
			float3 appendResult68 = (float3(( ( appendResult67 - temp_cast_0 ) * ( clampResult36 * mulTime38 ) * _Int ).x , ( ( ( appendResult67 - temp_cast_1 ) * ( clampResult36 * mulTime38 ) * _Int ).y * 1.5 ) , ( ( appendResult67 - temp_cast_2 ) * ( clampResult36 * mulTime38 ) * _Int ).z));
			v.vertex.xyz += appendResult68;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Emission = i.vertexColor.rgb;
			o.Alpha = i.vertexColor.a;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.6
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
1927;29;1586;824;3080.467;800.0084;1.809662;True;True
Node;AmplifyShaderEditor.WorldPosInputsNode;9;-3423.225,-611.1757;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector2Node;58;-3221.089,-416.1858;Float;False;Property;_Offset;Offset;1;0;Create;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.DynamicAppendNode;27;-3132.631,-592.9896;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;57;-2937.392,-594.0248;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-2774.463,-263.565;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0.2,0.23;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;40;-2634.113,-593.6631;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;50;-2597.883,-267.0397;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.04,-0.3;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;46;-2527.797,-80.90949;Float;True;Property;_Texture0;Texture 0;0;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-2372.593,-599.2883;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT2;0.5,0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;47;-2134.334,-116.3235;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;31;-2127.247,-622.8156;Float;True;Property;_T_CLoudNoiseRGB;T_CLoudNoiseRGB;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-2282.191,200.5035;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;53;-1804.425,-147.5512;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;52;-1819.325,-496.9512;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-1982.879,219.2441;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;65;-1620.567,-554.3401;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;48;-1617.892,-314.7662;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT2;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.IntNode;25;-1417.165,-23.98088;Float;False;Constant;_Int1;Int 1;1;0;Create;1;0;1;INT;0
Node;AmplifyShaderEditor.SimpleTimeNode;38;-1715.862,395.7354;Float;False;1;0;FLOAT;0.007;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;36;-1722.879,200.244;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;5.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;67;-1397.19,-403.9625;Float;False;FLOAT3;4;0;FLOAT;0.0;False;1;FLOAT2;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-1479.879,290.2441;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;24;-1260.95,-136.5603;Float;False;2;0;FLOAT3;0.0;False;1;INT;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-1252.153,330.4578;Float;False;Property;_Int;Int;2;0;Create;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-1070.711,132.5668;Float;False;3;3;0;FLOAT3;0.0;False;1;FLOAT;0,0,0,0;False;2;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;55;-874.1426,218.9636;Float;False;FLOAT3;1;0;FLOAT3;0.0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-611.1866,339.8456;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;1.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;68;-456.4869,217.9611;Float;False;FLOAT3;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.VertexColorNode;45;-830.2251,-116.1521;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;43;-208.215,-68.34818;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;SHA_trail01;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;False;False;Back;0;0;False;0;0;Transparent;0.5;True;True;0;False;Transparent;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;2;SrcAlpha;OneMinusSrcAlpha;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;0;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;27;0;9;1
WireConnection;27;1;9;3
WireConnection;57;0;27;0
WireConnection;57;1;58;0
WireConnection;49;0;57;0
WireConnection;40;0;57;0
WireConnection;50;0;49;0
WireConnection;28;0;40;0
WireConnection;47;0;46;0
WireConnection;47;1;50;0
WireConnection;31;0;46;0
WireConnection;31;1;28;0
WireConnection;53;0;47;3
WireConnection;53;1;47;1
WireConnection;52;0;31;1
WireConnection;52;1;31;2
WireConnection;39;0;5;1
WireConnection;65;0;31;3
WireConnection;65;2;47;2
WireConnection;48;0;52;0
WireConnection;48;1;53;0
WireConnection;36;0;39;0
WireConnection;67;0;65;0
WireConnection;67;1;48;0
WireConnection;37;0;36;0
WireConnection;37;1;38;0
WireConnection;24;0;67;0
WireConnection;24;1;25;0
WireConnection;26;0;24;0
WireConnection;26;1;37;0
WireConnection;26;2;60;0
WireConnection;55;0;26;0
WireConnection;69;0;55;1
WireConnection;68;0;55;0
WireConnection;68;1;69;0
WireConnection;68;2;55;2
WireConnection;43;2;45;0
WireConnection;43;9;45;4
WireConnection;43;11;68;0
ASEEND*/
//CHKSM=316C8C2DD39B79C009894FF55E7BAC28FC2C0330