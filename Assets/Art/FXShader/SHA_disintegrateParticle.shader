// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SHA_disintegrateParticle"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Texture0("Texture 0", 2D) = "white" {}
		_Tiling("Tiling", Float) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			fixed ASEVFace : VFACE;
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _Texture0;
		uniform float _Tiling;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord2 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 temp_output_31_0 = ( uv_TexCoord2 * _Tiling );
			float2 panner18 = ( temp_output_31_0 + 0.2 * _Time.y * float2( 1,1 ));
			float2 panner20 = ( temp_output_31_0 + 0.2 * _Time.y * float2( -1,-0.3 ));
			float2 panner3 = ( ( ( temp_output_31_0 + tex2D( _Texture0, panner18 ).b + tex2D( _Texture0, panner20 ).g ) * 2 ) + 1.0 * _Time.y * float2( 1,1 ));
			float2 panner13 = ( ( temp_output_31_0 * 3 ) + 1.5 * _Time.y * float2( 0.2,-1 ));
			float temp_output_12_0 = ( ( tex2D( _Texture0, panner3 ).r + tex2D( _Texture0, panner13 ).g ) * 0.5 );
			float clampResult32 = clamp( ( i.ASEVFace * temp_output_12_0 ) , 0.0 , 1.0 );
			float4 lerpResult23 = lerp( float4(0.3219067,0.04227941,0.3382353,0) , float4(0.5808823,1,0.9653144,0) , clampResult32);
			float3 appendResult30 = (float3(i.vertexColor.r , i.vertexColor.g , i.vertexColor.b));
			o.Emission = ( lerpResult23 * float4( appendResult30 , 0.0 ) ).rgb;
			o.Alpha = 1;
			clip( step( i.vertexColor.a , temp_output_12_0 ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
1927;29;1586;824;3344.364;386.8861;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;33;-2849.364,105.1139;Float;False;Property;_Tiling;Tiling;2;0;Create;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-2932.027,-46.6;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-2640.746,-47.53928;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT;0.5,0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;18;-2599.146,-341.3953;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;0.2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;9;-2719.701,276.2001;Float;True;Property;_Texture0;Texture 0;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;20;-2603.348,-618.0492;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,-0.3;False;1;FLOAT;0.2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;16;-2334.947,-446.695;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;19;-2311.237,-661.5792;Float;True;Property;_TextureSample2;Texture Sample 2;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;8;-1896.6,108.0999;Float;False;Constant;_Int0;Int 0;3;0;Create;2;0;1;INT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-1921.846,-185.3953;Float;False;3;3;0;FLOAT2;0,0;False;1;FLOAT;0,0;False;2;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.IntNode;15;-1883.845,536.5546;Float;False;Constant;_Int1;Int 1;3;0;Create;3;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1686.245,387.4549;Float;False;2;2;0;FLOAT2;0,0;False;1;INT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1699,-41;Float;False;2;2;0;FLOAT2;0,0;False;1;INT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;13;-1370.346,416.5049;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.2,-1;False;1;FLOAT;1.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;3;-1418,-45;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;10;-1027.145,285.2048;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1027,-78;Float;True;Property;_T_CLoudNoiseRGB;T_CLoudNoiseRGB;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-604.6445,276.1051;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FaceVariableNode;22;-909.1132,-357.6147;Float;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-452.2834,256.259;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-743.5106,-334.9684;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;24;-737.8489,-766.6667;Float;False;Constant;_Color0;Color 0;3;0;Create;0.3219067,0.04227941,0.3382353,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;32;-589.178,-277.3182;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;28;-614.9714,-97.94754;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;25;-733.6027,-591.1566;Float;False;Constant;_Color1;Color 1;3;0;Create;0.5808823,1,0.9653144,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;23;-451.9372,-426.9696;Float;False;3;0;COLOR;0.0,0,0,0;False;1;COLOR;0.0;False;2;FLOAT;0.0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;30;-388.9714,-110.9475;Float;False;FLOAT3;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StepOpNode;5;-404.6001,45.69999;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-248.9714,-273.9475;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT3;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/SHA_disintegrateParticle;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;0;False;0;0;Custom;0.5;True;True;0;True;TransparentCutout;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;31;0;2;0
WireConnection;31;1;33;0
WireConnection;18;0;31;0
WireConnection;20;0;31;0
WireConnection;16;0;9;0
WireConnection;16;1;18;0
WireConnection;19;0;9;0
WireConnection;19;1;20;0
WireConnection;17;0;31;0
WireConnection;17;1;16;3
WireConnection;17;2;19;2
WireConnection;14;0;31;0
WireConnection;14;1;15;0
WireConnection;7;0;17;0
WireConnection;7;1;8;0
WireConnection;13;0;14;0
WireConnection;3;0;7;0
WireConnection;10;0;9;0
WireConnection;10;1;13;0
WireConnection;1;0;9;0
WireConnection;1;1;3;0
WireConnection;11;0;1;1
WireConnection;11;1;10;2
WireConnection;12;0;11;0
WireConnection;26;0;22;0
WireConnection;26;1;12;0
WireConnection;32;0;26;0
WireConnection;23;0;24;0
WireConnection;23;1;25;0
WireConnection;23;2;32;0
WireConnection;30;0;28;1
WireConnection;30;1;28;2
WireConnection;30;2;28;3
WireConnection;5;0;28;4
WireConnection;5;1;12;0
WireConnection;29;0;23;0
WireConnection;29;1;30;0
WireConnection;0;2;29;0
WireConnection;0;10;5;0
ASEEND*/
//CHKSM=081C1BD4F37BE136515F7E90313EF56ED51E56A7