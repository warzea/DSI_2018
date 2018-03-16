// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SHA_chest"
{
	Properties
	{
		_AORM("AO/R/M", 2D) = "white" {}
		_Albedo("Albedo", 2D) = "white" {}
		[Toggle]_InvertRoughness("Invert Roughness", Float) = 0
		_GoldTransition("GoldTransition", Range( 0 , 1)) = 0
		_Desaturate("Desaturate", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "DisableBatching" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _GoldTransition;
		uniform float _Desaturate;
		uniform float _InvertRoughness;
		uniform sampler2D _AORM;
		uniform float4 _AORM_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode1 = tex2D( _Albedo, uv_Albedo );
			float4 lerpResult16 = lerp( tex2DNode1 , float4(0.8602941,0.4805781,0,0) , _GoldTransition);
			float3 desaturateVar26 = lerp( tex2DNode1.rgb,dot(tex2DNode1.rgb,float3(0.299,0.587,0.114)).xxx,_Desaturate);
			float4 lerpResult27 = lerp( lerpResult16 , float4( desaturateVar26 , 0.0 ) , _Desaturate);
			o.Albedo = lerpResult27.rgb;
			float3 ase_worldPos = i.worldPos;
			float4 transform62 = mul(unity_WorldToObject,float4(0,0,0,1));
			float3 temp_cast_3 = (( ( 1.0 - step( sin( ( ( ( ase_worldPos * 0.01 ).z + ( ase_worldPos * 0.01 ).y + ( ase_worldPos * 0.01 ).x + ( _Time.y * 0.5 ) + frac( sin( ( ( transform62.x + transform62.y + transform62.z + transform62.w ) * 14996.2 ) ) ) ) + 0.0 ) ) , 0.99999 ) ) * 0.1 * ( 1.0 - _Desaturate ) )).xxx;
			o.Emission = temp_cast_3;
			float2 uv_AORM = i.uv_texcoord * _AORM_ST.xy + _AORM_ST.zw;
			float4 tex2DNode6 = tex2D( _AORM, uv_AORM );
			float2 appendResult21 = (float2(( lerp(tex2DNode6.g,( 1.0 - tex2DNode6.g ),_InvertRoughness) * 1.0 ) , ( tex2DNode6.b * 1.0 )));
			float2 lerpResult19 = lerp( appendResult21 , float2( 0.7,1 ) , _GoldTransition);
			o.Metallic = ( ( 1.0 - _Desaturate ) * lerpResult19 ).y;
			o.Smoothness = ( ( 1.0 - _Desaturate ) * lerpResult19 ).x;
			o.Occlusion = tex2DNode6.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
-1593;29;1586;717;2810.257;-289.0014;1.831077;True;True
Node;AmplifyShaderEditor.CommentaryNode;71;-3043.56,1272.526;Float;False;1381.768;257.2926;RAND;7;70;64;65;69;68;62;63;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;63;-2993.56,1322.526;Float;False;Constant;_Vector1;Vector 1;5;0;Create;0,0,0,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldToObjectTransfNode;62;-2782.701,1327.818;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;68;-2573.628,1327.421;Float;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;69;-2293.013,1325.098;Float;False;4;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;48;-2005.612,638.7521;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;56;-2012.013,904.3522;Float;False;Constant;_Float0;Float 0;5;0;Create;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-2144.632,1342.171;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;14996.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;6;-1073.104,183.223;Float;True;Property;_AORM;AO/R/M;0;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;64;-1969.452,1365.089;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;42;-1721.029,1033.183;Float;False;1;0;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-1760.813,753.9522;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-1443.03,1011.983;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;7;-712.1035,326.223;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;70;-1815.791,1370.084;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;57;-1589.613,670.7523;Float;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;49;-1279.214,669.152;Float;False;5;5;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;13;-541.1035,430.223;Float;False;Property;_InvertRoughness;Invert Roughness;2;0;Create;0;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-1029.104,413.223;Float;False;Constant;_Metallic;Metallic;1;0;Create;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1031.104,485.223;Float;False;Constant;_Roughness;Roughness;1;0;Create;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-492.1035,263.223;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-677.1035,215.223;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-1020.013,665.9524;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;21;-500.7825,85.03229;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;22;-509.7825,-268.9677;Float;False;Constant;_Vector0;Vector 0;4;0;Create;0.7,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;25;-502.0227,-419.3337;Float;False;Property;_Desaturate;Desaturate;4;0;Create;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-891.7825,-134.9677;Float;False;Property;_GoldTransition;GoldTransition;3;0;Create;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;52;-706.4138,649.9518;Float;True;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;17;-972.7825,-334.9677;Float;False;Constant;_Color0;Color 0;3;0;Create;0.8602941,0.4805781,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1080.105,-8.777011;Float;True;Property;_Albedo;Albedo;1;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;19;-319.7825,25.03229;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;77;-205.6541,-255.222;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;33;-207.214,670.752;Float;True;2;0;FLOAT;0.0;False;1;FLOAT;0.99999;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;16;-406.8472,-119.9173;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;74;193.9127,506.4365;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;31.34595,-16.22198;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT2;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;73;157.5816,922.4725;Float;False;Constant;_Float1;Float 1;5;0;Create;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;54;46.25731,672.0267;Float;True;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;26;-162.8227,-410.2338;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;23;203.2175,48.03229;Float;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;359.5909,687.1816;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.2;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;27;40.78632,-199.6478;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;766.1235,-7.519197;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/SHA_chest;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;62;0;63;0
WireConnection;68;0;62;0
WireConnection;69;0;68;0
WireConnection;69;1;68;1
WireConnection;69;2;68;2
WireConnection;69;3;68;3
WireConnection;65;0;69;0
WireConnection;64;0;65;0
WireConnection;55;0;48;0
WireConnection;55;1;56;0
WireConnection;53;0;42;0
WireConnection;7;0;6;2
WireConnection;70;0;64;0
WireConnection;57;0;55;0
WireConnection;49;0;57;2
WireConnection;49;1;57;1
WireConnection;49;2;57;0
WireConnection;49;3;53;0
WireConnection;49;4;70;0
WireConnection;13;0;6;2
WireConnection;13;1;7;0
WireConnection;15;0;13;0
WireConnection;15;1;5;0
WireConnection;14;0;6;3
WireConnection;14;1;4;0
WireConnection;34;0;49;0
WireConnection;21;0;15;0
WireConnection;21;1;14;0
WireConnection;52;0;34;0
WireConnection;19;0;21;0
WireConnection;19;1;22;0
WireConnection;19;2;18;0
WireConnection;77;0;25;0
WireConnection;33;0;52;0
WireConnection;16;0;1;0
WireConnection;16;1;17;0
WireConnection;16;2;18;0
WireConnection;74;0;25;0
WireConnection;76;0;77;0
WireConnection;76;1;19;0
WireConnection;54;0;33;0
WireConnection;26;0;1;0
WireConnection;26;1;25;0
WireConnection;23;0;76;0
WireConnection;72;0;54;0
WireConnection;72;1;73;0
WireConnection;72;2;74;0
WireConnection;27;0;16;0
WireConnection;27;1;26;0
WireConnection;27;2;25;0
WireConnection;0;0;27;0
WireConnection;0;2;72;0
WireConnection;0;3;23;1
WireConnection;0;4;23;0
WireConnection;0;5;6;1
ASEEND*/
//CHKSM=F3A2632376CF4DF797DE20A57D05D4698B43D5EF