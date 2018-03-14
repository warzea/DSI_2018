// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SHA_YWorldProjection"
{
	Properties
	{
		_Texture("Texture", 2D) = "white" {}
		_NormalFlatten("NormalFlatten", Range( 0 , 1)) = 0
		_Tiling("Tiling", Float) = 1
		_Metalness("Metalness", Range( 0 , 1)) = 0
		_TEX_ground_wood_N("TEX_ground_wood_N", 2D) = "bump" {}
		_Float1("Float 1", Float) = 0.3
		_Float2("Float 2", Float) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "ForceNoShadowCasting" = "True" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha 
		struct Input
		{
			float3 worldPos;
		};

		uniform sampler2D _TEX_ground_wood_N;
		uniform float _Tiling;
		uniform float _NormalFlatten;
		uniform sampler2D _Texture;
		uniform float _Metalness;
		uniform float _Float1;
		uniform float _Float2;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float2 appendResult2 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 temp_output_5_0 = ( appendResult2 * _Tiling );
			float3 lerpResult10 = lerp( UnpackNormal( tex2D( _TEX_ground_wood_N, temp_output_5_0 ) ) , float3(0,0,1) , _NormalFlatten);
			o.Normal = lerpResult10;
			float4 tex2DNode3 = tex2D( _Texture, temp_output_5_0 );
			o.Albedo = tex2DNode3.rgb;
			o.Metallic = _Metalness;
			o.Smoothness = (0.0 + (tex2DNode3.r - 0.0) * (_Float2 - 0.0) / (_Float1 - 0.0));
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
1927;29;1378;824;958.381;398.5627;1.3;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;1;-1277.633,40.38269;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;2;-957.6332,55.38269;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-950.6332,203.3827;Float;False;Property;_Tiling;Tiling;2;0;Create;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-718.6332,78.38269;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector3Node;11;-214.6331,331.3827;Float;False;Constant;_Vector0;Vector 0;5;0;Create;0,0,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;9;-492.6331,251.3827;Float;True;Property;_TEX_ground_wood_N;TEX_ground_wood_N;4;0;Create;Assets/Art/Textures/TEX_ground_wood_N.tga;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-511.6331,665.3827;Float;False;Property;_NormalFlatten;NormalFlatten;1;0;Create;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-489.6332,37.38269;Float;True;Property;_Texture;Texture;0;0;Create;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-373.6331,-81.61728;Float;False;Property;_Float2;Float 2;6;0;Create;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-378.6331,-222.6173;Float;False;Constant;_Float0;Float 0;6;0;Create;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-369.6331,-158.6173;Float;False;Property;_Float1;Float 1;5;0;Create;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;17;-116.6331,-164.6173;Float;True;5;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-505.6332,508.3827;Float;False;Property;_Metalness;Metalness;3;0;Create;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;10;-9.633057,216.3827;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;216,35;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/SHA_YWorldProjection;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;Back;0;0;False;0;0;Opaque;0.5;True;False;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;1
WireConnection;2;1;1;3
WireConnection;5;0;2;0
WireConnection;5;1;6;0
WireConnection;9;1;5;0
WireConnection;3;1;5;0
WireConnection;17;0;3;1
WireConnection;17;1;18;0
WireConnection;17;2;19;0
WireConnection;17;3;18;0
WireConnection;17;4;20;0
WireConnection;10;0;9;0
WireConnection;10;1;11;0
WireConnection;10;2;12;0
WireConnection;0;0;3;0
WireConnection;0;1;10;0
WireConnection;0;3;7;0
WireConnection;0;4;17;0
ASEEND*/
//CHKSM=594D3E2D134AB5766FB9F51F632E4A14AC1F7E36