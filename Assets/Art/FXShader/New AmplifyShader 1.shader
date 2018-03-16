// Upgrade NOTE: upgraded instancing buffer 'FX_Pentacle' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FX_Pentacle"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Color0("Color 0", Color) = (0,0,0,0)
		_Color1("Color 1", Color) = (0,0,0,0)
		_TimePower("TimePower", Float) = 10
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_Float0("Float 0", Range( 0 , 20)) = 20
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		Blend One Zero , One One
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;
		uniform float _Cutoff = 0.5;

		UNITY_INSTANCING_BUFFER_START(FX_Pentacle)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color1)
#define _Color1_arr FX_Pentacle
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color0)
#define _Color0_arr FX_Pentacle
			UNITY_DEFINE_INSTANCED_PROP(float, _TimePower)
#define _TimePower_arr FX_Pentacle
			UNITY_DEFINE_INSTANCED_PROP(float, _Float0)
#define _Float0_arr FX_Pentacle
		UNITY_INSTANCING_BUFFER_END(FX_Pentacle)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _Color1_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color1_arr, _Color1);
			float4 _Color0_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color0_arr, _Color0);
			float _TimePower_Instance = UNITY_ACCESS_INSTANCED_PROP(_TimePower_arr, _TimePower);
			o.Albedo = ( _Color1_Instance + ( _Color0_Instance * sin( ( _Time.y * _TimePower_Instance ) ) ) ).rgb;
			o.Emission = _Color1_Instance.rgb;
			o.Alpha = 1;
			float2 uv_TexCoord5 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float cos6 = cos( 1.0 * _Time.y );
			float sin6 = sin( 1.0 * _Time.y );
			float2 rotator6 = mul( uv_TexCoord5 - float2( 0.5,0.5 ) , float2x2( cos6 , -sin6 , sin6 , cos6 )) + float2( 0.5,0.5 );
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			float _Float0_Instance = UNITY_ACCESS_INSTANCED_PROP(_Float0_arr, _Float0);
			float4 temp_cast_2 = (_Float0_Instance).xxxx;
			float lerpResult28 = lerp( tex2D( _TextureSample0, rotator6 ).r , 0.0 , pow( tex2D( _TextureSample1, uv_TextureSample1 ) , temp_cast_2 ).r);
			clip( lerpResult28 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
2328;194;955;495;1351.756;205.2394;1.993245;True;False
Node;AmplifyShaderEditor.RangedFloatNode;25;-1015.85,-47.15819;Float;False;InstancedProperty;_TimePower;TimePower;4;0;Create;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;27;-946.9992,-319.4982;Float;False;1;0;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-773.9373,-236.8781;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-1393.487,404.6808;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;26;-647.1185,-109.8882;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;7;-777.8163,-468.7311;Float;False;InstancedProperty;_Color0;Color 0;2;0;Create;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;6;-1063.759,170.5262;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;29;-485.899,440.912;Float;True;Property;_TextureSample1;Texture Sample 1;5;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;31;-559.339,625.6822;Float;False;InstancedProperty;_Float0;Float 0;6;0;Create;20;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;30;-51.37909,457.742;Float;True;2;0;COLOR;0.0;False;1;FLOAT;0.0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-656.8576,209.6267;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;Assets/Art/FXShader/Texture/pentacle_1.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-260.7334,-100.2103;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;21;-529.5663,-631.4163;Float;False;InstancedProperty;_Color1;Color 1;3;0;Create;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-176.2954,-367.5945;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;28;-176.8391,254.252;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;222.52,-253.9799;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;FX_Pentacle;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;0;False;0;0;Custom;0.5;True;True;0;True;TransparentCutout;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;4;One;One;OFF;Add;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;24;0;27;0
WireConnection;24;1;25;0
WireConnection;26;0;24;0
WireConnection;6;0;5;0
WireConnection;30;0;29;0
WireConnection;30;1;31;0
WireConnection;1;1;6;0
WireConnection;20;0;7;0
WireConnection;20;1;26;0
WireConnection;22;0;21;0
WireConnection;22;1;20;0
WireConnection;28;0;1;1
WireConnection;28;2;30;0
WireConnection;0;0;22;0
WireConnection;0;2;21;0
WireConnection;0;10;28;0
ASEEND*/
//CHKSM=63C103E66564E8950A88CD22ED88A0DA29A3D9BF