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
		_NormalFlatten("NormalFlatten", Range( 0 , 1)) = 0
		_Color("Color", 2D) = "white" {}
		_EmissiveColor("EmissiveColor", Color) = (1,0.4760649,0.07352942,0)
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
		uniform float _NormalFlatten;
		uniform sampler2D _Emissive;
		uniform float4 _Emissive_ST;
		uniform sampler2D _Color;
		uniform float4 _Color_ST;
		uniform float4 _EmissiveColor;
		uniform sampler2D _AOMR;
		uniform float4 _AOMR_ST;
		uniform float _ToggleSwitch0;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 lerpResult9 = lerp( UnpackScaleNormal( tex2D( _Normal, uv_Normal ) ,0.0 ) , float3( 0,0,1 ) , _NormalFlatten);
			o.Normal = lerpResult9;
			float2 uv_Emissive = i.uv_texcoord * _Emissive_ST.xy + _Emissive_ST.zw;
			o.Albedo = tex2D( _Emissive, uv_Emissive ).rgb;
			float2 uv_Color = i.uv_texcoord * _Color_ST.xy + _Color_ST.zw;
			float4 transform26 = mul(unity_WorldToObject,float4(0,0,0,1));
			float mulTime18 = _Time.y * 0.3;
			float temp_output_32_0 = ( ( frac( sin( ( ( transform26.x + transform26.y + transform26.z + transform26.w ) * 14996.2 ) ) ) * 10.0 ) + mulTime18 );
			float clampResult22 = clamp( (0.8 + (frac( ( sin( temp_output_32_0 ) * 4342.157 ) ) - 0.0) * (1.0 - 0.8) / (1.0 - 0.0)) , 0.95 , 1.0 );
			o.Emission = ( tex2D( _Color, uv_Color ).r * _EmissiveColor * 10.0 * ( (0.5 + (sin( temp_output_32_0 ) - -1.0) * (1.0 - 0.5) / (1.0 - -1.0)) * clampResult22 ) ).rgb;
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
-1593;29;1586;864;3451.01;393.0348;1.477272;True;True
Node;AmplifyShaderEditor.CommentaryNode;24;-4050.016,-254.2753;Float;False;1381.768;257.2926;RAND;7;31;30;29;28;27;26;25;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;25;-4000.016,-204.2755;Float;False;Constant;_Vector0;Vector 0;5;0;Create;0,0,0,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldToObjectTransfNode;26;-3789.156,-198.9835;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;27;-3580.081,-199.3805;Float;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;28;-3299.465,-201.7035;Float;False;4;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-3151.084,-184.6306;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;14996.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;30;-2975.904,-161.7125;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;31;-2822.243,-156.7175;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-2558.738,-84.28493;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;10.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;18;-2825.061,99.09302;Float;False;1;0;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-2508.923,160.2678;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;19;-2370.199,225.4102;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-2203.513,220.1501;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;4342.157;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;21;-2014.513,227.1501;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;33;-1923.511,332.3059;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;3;FLOAT;0.8;False;4;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;16;-2012.041,21.49954;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;17;-1844.961,21.48621;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;-1.0;False;2;FLOAT;1.0;False;3;FLOAT;0.5;False;4;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;22;-1708.558,249.3092;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.95;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-857.02,149.4855;Float;True;Property;_AOMR;AO M R;1;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1528.513,92.15015;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-854.318,567.3314;Float;False;Property;_NormalFlatten;NormalFlatten;4;0;Create;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-858.638,364.2913;Float;True;Property;_Normal;Normal;3;0;Create;Assets/Art/Textures/TEX_ground_wood_N.tga;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-1023.455,-50.82071;Float;False;Property;_EmissiveColor;EmissiveColor;6;0;Create;1,0.4760649,0.07352942,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;14;-769.0317,9.446514;Float;False;Constant;_Float1;Float 1;6;0;Create;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-1038.728,-248.1955;Float;True;Property;_Color;Color;5;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;10;-540.6523,271.9459;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;9;-373.8417,393.8011;Float;True;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,1;False;2;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-606.311,-156.2882;Float;False;4;4;0;FLOAT;0.0;False;1;COLOR;0;False;2;FLOAT;0,0,0,0;False;3;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;7;-878.3719,-471.9271;Float;True;Property;_Emissive;Emissive;0;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;8;-347.8515,215.8292;Float;False;Property;_ToggleSwitch0;Toggle Switch0;2;0;Create;1;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/SHA_lantern;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;False;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;26;0;25;0
WireConnection;27;0;26;0
WireConnection;28;0;27;0
WireConnection;28;1;27;1
WireConnection;28;2;27;2
WireConnection;28;3;27;3
WireConnection;29;0;28;0
WireConnection;30;0;29;0
WireConnection;31;0;30;0
WireConnection;34;0;31;0
WireConnection;32;0;34;0
WireConnection;32;1;18;0
WireConnection;19;0;32;0
WireConnection;20;0;19;0
WireConnection;21;0;20;0
WireConnection;33;0;21;0
WireConnection;16;0;32;0
WireConnection;17;0;16;0
WireConnection;22;0;33;0
WireConnection;23;0;17;0
WireConnection;23;1;22;0
WireConnection;10;0;4;3
WireConnection;9;0;5;0
WireConnection;9;2;6;0
WireConnection;12;0;11;1
WireConnection;12;1;13;0
WireConnection;12;2;14;0
WireConnection;12;3;23;0
WireConnection;8;0;4;3
WireConnection;8;1;10;0
WireConnection;0;0;7;0
WireConnection;0;1;9;0
WireConnection;0;2;12;0
WireConnection;0;3;4;2
WireConnection;0;4;8;0
WireConnection;0;5;4;1
ASEEND*/
//CHKSM=7365FD5F2C16DE20B610A74D5DF3C714856E9DB7