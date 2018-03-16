// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SHA_cauldronContent"
{
	Properties
	{
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _Color1 = float4(0.7095514,0.7941176,0.1810121,0);
			float2 uv_TexCoord12 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float temp_output_16_0 = ( 1.0 - ( distance( uv_TexCoord12 , float2( 0.5,0.5 ) ) * (1.0 + (sin( ( _Time.y * 4 ) ) - 0.0) * (2.0 - 1.0) / (1.0 - 0.0)) ) );
			float4 lerpResult3 = lerp( float4(0.1036347,0.1911765,0.04357699,0) , _Color1 , temp_output_16_0);
			o.Albedo = lerpResult3.rgb;
			o.Emission = ( _Color1 * temp_output_16_0 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
-1593;29;1586;717;2702.628;298.785;1.733215;True;True
Node;AmplifyShaderEditor.IntNode;22;-1681.765,673.5486;Float;False;Constant;_Int0;Int 0;0;0;Create;4;0;1;INT;0
Node;AmplifyShaderEditor.SimpleTimeNode;18;-1937.308,574.6171;Float;False;1;0;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1513.642,588.6212;Float;False;2;2;0;FLOAT;0.0;False;1;INT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;14;-1691.16,348.378;Float;False;Constant;_Vector1;Vector 1;0;0;Create;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-1909.877,170.488;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;19;-1225.48,494.9735;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;20;-996.9271,549.3146;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;3;FLOAT;1.0;False;4;FLOAT;2.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;13;-1387.872,212.7732;Float;False;2;0;FLOAT2;0.0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1070.004,279.8466;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;2.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-624,-225.5;Float;False;Constant;_Color0;Color 0;0;0;Create;0.1036347,0.1911765,0.04357699,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;16;-864.4104,282.7628;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-628,-51.5;Float;False;Constant;_Color1;Color 1;0;0;Create;0.7095514,0.7941176,0.1810121,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-140.377,217.7092;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;3;-357,-117.5;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;279.9578,-34.99474;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/SHA_cauldronContent;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;21;0;18;0
WireConnection;21;1;22;0
WireConnection;19;0;21;0
WireConnection;20;0;19;0
WireConnection;13;0;12;0
WireConnection;13;1;14;0
WireConnection;15;0;13;0
WireConnection;15;1;20;0
WireConnection;16;0;15;0
WireConnection;17;0;2;0
WireConnection;17;1;16;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;3;2;16;0
WireConnection;0;0;3;0
WireConnection;0;2;17;0
ASEEND*/
//CHKSM=F024BCFE5BD71AD1ED4127A63E40D104B3EBF729