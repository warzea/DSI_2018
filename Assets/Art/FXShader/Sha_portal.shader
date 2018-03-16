// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SHA_portal"
{
	Properties
	{
		_baseColor("baseColor", 2D) = "white" {}
		_AO_R_MT("AO_R_MT", 2D) = "white" {}
		[Toggle]_invert_Roughness("invert_Roughness", Float) = 1
		_TEX_ground_wood_N("TEX_ground_wood_N", 2D) = "bump" {}
		_Normal_Flatten("Normal_Flatten", Range( 0 , 1)) = 0
		_Emissive("Emissive", 2D) = "white" {}
		_Emissive_Color1("Emissive_Color1", Color) = (0,0,0,0)
		_Emissive_Color2("Emissive_Color2", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
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
		uniform float4 _Emissive_Color2;
		uniform float4 _Emissive_Color1;
		uniform sampler2D _Emissive;
		uniform float4 _Emissive_ST;
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
			float4 transform20 = mul(unity_WorldToObject,float4(0,0,0,1));
			float4 lerpResult27 = lerp( _Emissive_Color2 , _Emissive_Color1 , frac( sin( ( ( transform20.x + transform20.y + transform20.z + transform20.w ) * 14996.2 ) ) ));
			float2 uv_Emissive = i.uv_texcoord * _Emissive_ST.xy + _Emissive_ST.zw;
			o.Emission = ( lerpResult27 * tex2D( _Emissive, uv_Emissive ) ).rgb;
			float2 uv_AO_R_MT = i.uv_texcoord * _AO_R_MT_ST.xy + _AO_R_MT_ST.zw;
			float4 tex2DNode2 = tex2D( _AO_R_MT, uv_AO_R_MT );
			o.Metallic = tex2DNode2.b;
			o.Smoothness = lerp(tex2DNode2.g,( 1.0 - tex2DNode2.b ),_invert_Roughness);
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
7;29;1906;1044;1189.134;542.613;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;18;-2381.269,-894.8215;Float;False;1381.768;257.2926;RAND;7;25;24;23;22;21;20;19;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;19;-2331.269,-844.8217;Float;False;Constant;_Vector0;Vector 0;5;0;Create;0,0,0,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldToObjectTransfNode;20;-2120.409,-839.5297;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;21;-1911.334,-839.9268;Float;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-1630.718,-842.2498;Float;False;4;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1482.337,-825.1768;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;14996.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;24;-1307.157,-802.2589;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;26;-854.3739,-804.0852;Float;False;Property;_Emissive_Color2;Emissive_Color2;7;0;Create;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;17;-850.7047,-627.1422;Float;False;Property;_Emissive_Color1;Emissive_Color1;6;0;Create;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FractNode;25;-1153.496,-797.2639;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-874.6378,-17.1451;Float;True;Property;_AO_R_MT;AO_R_MT;1;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;9;-377.125,292.196;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-887.7716,-433.8839;Float;True;Property;_Emissive;Emissive;5;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-804.1357,516.7149;Float;False;Property;_Normal_Flatten;Normal_Flatten;4;0;Create;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-871.7365,277.5148;Float;True;Property;_TEX_ground_wood_N;TEX_ground_wood_N;3;0;Create;Assets/Art/Textures/TEX_ground_wood_N.tga;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;27;-505.8887,-662.1892;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-883.2001,-223.4589;Float;True;Property;_baseColor;baseColor;0;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-362.2446,-325.8608;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;10;-280.6161,561.6622;Float;True;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,1;False;2;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ToggleSwitchNode;12;-369.9363,203.4146;Float;False;Property;_invert_Roughness;invert_Roughness;2;0;Create;1;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;SHA_portal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;20;0;19;0
WireConnection;21;0;20;0
WireConnection;22;0;21;0
WireConnection;22;1;21;1
WireConnection;22;2;21;2
WireConnection;22;3;21;3
WireConnection;23;0;22;0
WireConnection;24;0;23;0
WireConnection;25;0;24;0
WireConnection;9;0;2;3
WireConnection;27;0;26;0
WireConnection;27;1;17;0
WireConnection;27;2;25;0
WireConnection;15;0;27;0
WireConnection;15;1;14;0
WireConnection;10;0;13;0
WireConnection;10;2;11;0
WireConnection;12;0;2;2
WireConnection;12;1;9;0
WireConnection;0;0;1;0
WireConnection;0;1;10;0
WireConnection;0;2;15;0
WireConnection;0;3;2;3
WireConnection;0;4;12;0
WireConnection;0;5;2;1
ASEEND*/
//CHKSM=AC8F161BE4D49B98FCB3434E6552201E972ADC4B