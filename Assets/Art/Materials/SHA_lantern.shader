// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SHA_lantern"
{
	Properties
	{
		_Emissive("Emissive", 2D) = "white" {}
		_AOMR("AO M R", 2D) = "white" {}
		[Toggle]_ToggleSwitch0("Toggle Switch0", Float) = 1
		_Normal("Normal", 2D) = "bump" {}
		_Float0("Float 0", Range( 0 , 1)) = 0
		_Color("Color", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform float _Float0;
		uniform sampler2D _Emissive;
		uniform float4 _Emissive_ST;
		uniform sampler2D _Color;
		uniform float4 _Color_ST;
		uniform sampler2D _AOMR;
		uniform float4 _AOMR_ST;
		uniform float _ToggleSwitch0;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 lerpResult9 = lerp( UnpackScaleNormal( tex2D( _Normal, uv_Normal ) ,0.0 ) , float3( 0,0,1 ) , _Float0);
			o.Normal = lerpResult9;
			float2 uv_Emissive = i.uv_texcoord * _Emissive_ST.xy + _Emissive_ST.zw;
			o.Albedo = tex2D( _Emissive, uv_Emissive ).rgb;
			float2 uv_Color = i.uv_texcoord * _Color_ST.xy + _Color_ST.zw;
			float clampResult22 = clamp( frac( ( sin( _Time.y ) * 4342.157 ) ) , 0.8 , 1.0 );
			o.Emission = ( tex2D( _Color, uv_Color ).r * float4(1,0.9637931,0.8455882,0) * 4.0 * ( (0.5 + (sin( _Time.y ) - -1.0) * (1.0 - 0.5) / (1.0 - -1.0)) * clampResult22 ) ).rgb;
			float2 uv_AOMR = i.uv_texcoord * _AOMR_ST.xy + _AOMR_ST.zw;
			float4 tex2DNode4 = tex2D( _AOMR, uv_AOMR );
			o.Metallic = tex2DNode4.g;
			o.Smoothness = lerp(tex2DNode4.b,( 1.0 - tex2DNode4.b ),_ToggleSwitch0);
			o.Occlusion = tex2DNode4.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
7;29;1660;855;3082.221;701.4537;1.395838;True;True
Node;AmplifyShaderEditor.SimpleTimeNode;18;-2558.827,223.8901;Float;False;1;0;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;19;-2370.199,225.4102;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-2203.513,220.1501;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;4342.157;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;15;-2266.669,16.97952;Float;False;1;0;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;21;-2014.513,227.1501;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;16;-2012.041,21.49954;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;22;-1841.513,227.1501;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.8;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-1020.629,176.7537;Float;True;Property;_AOMR;AO M R;1;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;17;-1844.961,21.48621;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;-1.0;False;2;FLOAT;1.0;False;3;FLOAT;0.5;False;4;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-1022.247,391.5595;Float;True;Property;_Normal;Normal;3;0;Create;Assets/Art/Textures/TEX_ground_wood_N.tga;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;10;-704.2613,299.2142;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1017.927,594.5995;Float;False;Property;_Float0;Float 0;4;0;Create;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1528.513,92.15015;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-1038.728,-248.1955;Float;True;Property;_Color;Color;5;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-1013.114,-50.82071;Float;False;Constant;_Color0;Color 0;6;0;Create;1,0.9637931,0.8455882,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;14;-769.0317,9.446514;Float;False;Constant;_Float1;Float 1;6;0;Create;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;8;-511.4605,243.0974;Float;False;Property;_ToggleSwitch0;Toggle Switch0;2;0;Create;1;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-606.311,-156.2882;Float;False;4;4;0;FLOAT;0.0;False;1;COLOR;0;False;2;FLOAT;0,0,0,0;False;3;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;9;-537.4507,421.0694;Float;True;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,1;False;2;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;7;-1041.981,-444.6588;Float;True;Property;_Emissive;Emissive;0;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/SHA_lantern;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;False;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;0;18;0
WireConnection;20;0;19;0
WireConnection;21;0;20;0
WireConnection;16;0;15;0
WireConnection;22;0;21;0
WireConnection;17;0;16;0
WireConnection;10;0;4;3
WireConnection;23;0;17;0
WireConnection;23;1;22;0
WireConnection;8;0;4;3
WireConnection;8;1;10;0
WireConnection;12;0;11;1
WireConnection;12;1;13;0
WireConnection;12;2;14;0
WireConnection;12;3;23;0
WireConnection;9;0;5;0
WireConnection;9;2;6;0
WireConnection;0;0;7;0
WireConnection;0;1;9;0
WireConnection;0;2;12;0
WireConnection;0;3;4;2
WireConnection;0;4;8;0
WireConnection;0;5;4;1
ASEEND*/
//CHKSM=09B0A8A29589EA5681A8167A7169BDBD0F4238B0