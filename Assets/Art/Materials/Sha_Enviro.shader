// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SHA_Enviro"
{
	Properties
	{
		_MainColor("MainColor", 2D) = "white" {}
		_AO_R_M("AO_R_M", 2D) = "white" {}
		_Normale("Normale", 2D) = "white" {}
		[ToggleOff] _Normal("Normal", Float) = 1.0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma shader_feature _NORMAL_OFF
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normale;
		uniform float4 _Normale_ST;
		uniform sampler2D _MainColor;
		uniform float4 _MainColor_ST;
		uniform sampler2D _AO_R_M;
		uniform float4 _AO_R_M_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normale = i.uv_texcoord * _Normale_ST.xy + _Normale_ST.zw;
			#ifdef _NORMAL_OFF
				float4 staticSwitch5 = tex2D( _Normale, uv_Normale );
			#else
				float4 staticSwitch5 = float4( float3(128,128,255) , 0.0 );
			#endif
			o.Normal = staticSwitch5.rgb;
			float2 uv_MainColor = i.uv_texcoord * _MainColor_ST.xy + _MainColor_ST.zw;
			o.Albedo = tex2D( _MainColor, uv_MainColor ).rgb;
			float2 uv_AO_R_M = i.uv_texcoord * _AO_R_M_ST.xy + _AO_R_M_ST.zw;
			float4 tex2DNode2 = tex2D( _AO_R_M, uv_AO_R_M );
			o.Metallic = tex2DNode2.b;
			o.Smoothness = ( 1.0 - tex2DNode2.g );
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
2328;194;955;495;1165.562;24.09473;1.675;True;False
Node;AmplifyShaderEditor.SamplerNode;2;-488.8378,174.3549;Float;True;Property;_AO_R_M;AO_R_M;1;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-526.5376,378.4553;Float;True;Property;_Normale;Normale;2;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;6;-249.3377,511.905;Float;True;Constant;_Vector0;Vector 0;4;0;Create;128,128,255;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;1;-486.2379,-11.54502;Float;True;Property;_MainColor;MainColor;0;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;3;-176.8378,226.355;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;5;-187.2381,342.8553;Float;False;Property;_Normal;Normal;3;0;Create;0;True;True;;ToggleOff;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;SHA_Enviro;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;2
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;0;0;1;0
WireConnection;0;1;5;0
WireConnection;0;3;2;3
WireConnection;0;4;3;0
WireConnection;0;5;2;1
ASEEND*/
//CHKSM=5D705DD2EFE376D6C23358566A68F0FFDE740BA6