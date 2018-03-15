// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SHA_enviro"
{
	Properties
	{
		_baseColor("baseColor", 2D) = "white" {}
		_AO_R_MT("AO_R_MT", 2D) = "white" {}
		[Toggle]_invert_Roughness("invert_Roughness", Float) = 0
		_TEX_ground_wood_N("TEX_ground_wood_N", 2D) = "bump" {}
		_Normal_Flatten("Normal_Flatten", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TEX_ground_wood_N;
		uniform float4 _TEX_ground_wood_N_ST;
		uniform float _Normal_Flatten;
		uniform sampler2D _baseColor;
		uniform float4 _baseColor_ST;
		uniform sampler2D _AO_R_MT;
		uniform float4 _AO_R_MT_ST;
		uniform float _invert_Roughness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TEX_ground_wood_N = i.uv_texcoord * _TEX_ground_wood_N_ST.xy + _TEX_ground_wood_N_ST.zw;
			float3 lerpResult10 = lerp( UnpackScaleNormal( tex2D( _TEX_ground_wood_N, uv_TEX_ground_wood_N ) ,0.0 ) , float3( 0,0,1 ) , _Normal_Flatten);
			o.Normal = lerpResult10;
			float2 uv_baseColor = i.uv_texcoord * _baseColor_ST.xy + _baseColor_ST.zw;
			o.Albedo = tex2D( _baseColor, uv_baseColor ).rgb;
			float2 uv_AO_R_MT = i.uv_texcoord * _AO_R_MT_ST.xy + _AO_R_MT_ST.zw;
			float4 tex2DNode2 = tex2D( _AO_R_MT, uv_AO_R_MT );
			o.Metallic = tex2DNode2.g;
			o.Smoothness = lerp(tex2DNode2.b,( 1.0 - tex2DNode2.b ),_invert_Roughness);
			o.Occlusion = tex2DNode2.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
-1593;29;1586;717;1504.837;77.38531;1.3;True;True
Node;AmplifyShaderEditor.SamplerNode;2;-874.6378,-17.1451;Float;True;Property;_AO_R_MT;AO_R_MT;1;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;9;-526.2442,139.349;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-871.7365,277.5148;Float;True;Property;_TEX_ground_wood_N;TEX_ground_wood_N;4;0;Create;Assets/Art/Textures/TEX_ground_wood_N.tga;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-804.1357,516.7149;Float;False;Property;_Normal_Flatten;Normal_Flatten;4;0;Create;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-509.2379,-252.545;Float;True;Property;_baseColor;baseColor;0;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;12;-369.9363,203.4146;Float;False;Property;_invert_Roughness;invert_Roughness;3;0;Create;0;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;10;-429.7353,408.815;Float;True;3;0;FLOAT3;0.0;False;1;FLOAT3;0,0,1;False;2;FLOAT;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;SHA_enviro;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;2;3
WireConnection;12;0;2;3
WireConnection;12;1;9;0
WireConnection;10;0;13;0
WireConnection;10;2;11;0
WireConnection;0;0;1;0
WireConnection;0;1;10;0
WireConnection;0;3;2;2
WireConnection;0;4;12;0
WireConnection;0;5;2;1
ASEEND*/
//CHKSM=1BE984FA75897974FF6992AC9309A3018292283E