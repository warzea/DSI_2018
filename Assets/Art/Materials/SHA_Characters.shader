// Upgrade NOTE: upgraded instancing buffer 'Sha_Characters' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Sha_Characters"
{
	Properties
	{
		_Force("Force", Range( 0 , 2)) = 0.8791087
		_MainColor("MainColor", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform sampler2D _MainColor;
		uniform float4 _MainColor_ST;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;

		UNITY_INSTANCING_BUFFER_START(Sha_Characters)
			UNITY_DEFINE_INSTANCED_PROP(float, _Force)
#define _Force_arr Sha_Characters
		UNITY_INSTANCING_BUFFER_END(Sha_Characters)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainColor = i.uv_texcoord * _MainColor_ST.xy + _MainColor_ST.zw;
			o.Albedo = tex2D( _MainColor, uv_MainColor ).rgb;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float _Force_Instance = UNITY_ACCESS_INSTANCED_PROP(_Force_arr, _Force);
			float fresnelNDotV2 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode2 = ( -0.52 + _Force_Instance * pow( 1.0 - fresnelNDotV2, 1.0 ) );
			float clampResult15 = clamp( fresnelNode2 , 0.0 , 2.197868 );
			float4 lerpResult5 = lerp( float4( 0,0,0,0 ) , float4(0.8823529,0,0,0) , clampResult15);
			o.Emission = lerpResult5.rgb;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode18 = tex2D( _TextureSample0, uv_TextureSample0 );
			o.Metallic = tex2DNode18.g;
			o.Smoothness = tex2DNode18.r;
			o.Occlusion = tex2DNode18.b;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows exclude_path:forward 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
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
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
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
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
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
1927;29;1906;1004;1361.269;202.3272;1.3;True;True
Node;AmplifyShaderEditor.RangedFloatNode;12;-995.9672,357.9731;Float;False;InstancedProperty;_Force;Force;0;0;Create;0.8791087;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-759.3674,549.0726;Float;False;Constant;_Power;Power;0;0;Create;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-776.2684,182.4729;Float;False;Constant;_Float0;Float 0;2;0;Create;-0.52;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;2;-547.6663,376.4468;Float;False;Tangent;4;0;FLOAT3;0,0,0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;5.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-381.0688,666.0728;Float;False;Constant;_Float1;Float 1;2;0;Create;2.197868;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-561.768,192.8725;Float;False;Constant;_Color0;Color 0;2;0;Create;0.8823529,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;15;-301.7686,448.9729;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;5;-325.1679,157.7724;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;17;-483.769,-60.62719;Float;True;Property;_MainColor;MainColor;1;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-85.96912,567.2729;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;425.0999,175.5;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Sha_Characters;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;DeferredOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;1;9;0
WireConnection;2;2;12;0
WireConnection;2;3;4;0
WireConnection;15;0;2;0
WireConnection;15;2;16;0
WireConnection;5;1;6;0
WireConnection;5;2;15;0
WireConnection;0;0;17;0
WireConnection;0;2;5;0
WireConnection;0;3;18;2
WireConnection;0;4;18;1
WireConnection;0;5;18;3
ASEEND*/
//CHKSM=64AC8BC3AD52C3626998A16162D25C6C63B0461E