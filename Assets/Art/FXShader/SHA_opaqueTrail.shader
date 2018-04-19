// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SHA_opaqueTrail"
{
	Properties
	{
		_Texture0("Texture 0", 2D) = "white" {}
		_T_Trail("T_Trail", 2D) = "white" {}
		_Tilling("Tilling", Float) = 0.2
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "ForceNoShadowCasting" = "True" "IsEmissive" = "true"  }
		Cull Back
		ZWrite Off
		Blend DstColor Zero
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows noambient novertexlights nolightmap  nodynlightmap nodirlightmap 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _T_Trail;
		uniform sampler2D _Texture0;
		uniform float _Tilling;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_TexCoord54 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 smoothstepResult58 = smoothstep( float2( 1,1 ) , float2( 0.9,0.7 ) , abs( ( ( uv_TexCoord54 * float2( 2,2 ) ) + float2( -1,-1 ) ) ));
			float3 ase_worldPos = i.worldPos;
			float2 appendResult44 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 temp_output_4_0 = ( appendResult44 * _Tilling );
			float2 panner7 = ( temp_output_4_0 + 0.1 * _Time.y * float2( -1,-0.3 ));
			float2 panner5 = ( temp_output_4_0 + 0.1 * _Time.y * float2( 1,1 ));
			float temp_output_11_0 = ( tex2D( _Texture0, panner7 ).r + tex2D( _Texture0, panner5 ).g + -1.0 );
			float2 uv_TexCoord49 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 panner16 = ( ( ( temp_output_11_0 * 1.5 ) + temp_output_4_0 ) + 0.1 * _Time.y * float2( 1,1 ));
			float2 panner15 = ( ( temp_output_4_0 * 2 ) + 0.2 * _Time.y * float2( 0.2,-1 ));
			float temp_output_41_0 = ( tex2D( _T_Trail, ( ( temp_output_11_0 * 0.5 ) + uv_TexCoord49 ) ).r * ( tex2D( _Texture0, panner16 ).r + tex2D( _Texture0, panner15 ).g ) );
			float clampResult40 = clamp( ( ( temp_output_41_0 * 0.7 ) + step( 0.7 , temp_output_41_0 ) ) , 0.0 , 1.0 );
			o.Emission = ( 1.0 - ( smoothstepResult58.x * smoothstepResult58.y * clampResult40 * ( 1.0 - i.vertexColor ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
1927;29;1586;824;3499.941;198.2722;1;True;True
Node;AmplifyShaderEditor.WorldPosInputsNode;43;-3319.265,-113.4356;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;45;-2974.61,269.5137;Float;False;Property;_Tilling;Tilling;3;0;Create;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;44;-2974.609,-73.66783;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-2639.003,91.74568;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0.0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;5;-2311.77,-165.3113;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;0.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;6;-2432.325,452.2841;Float;True;Property;_Texture0;Texture 0;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;7;-2342.258,-424.4417;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,-0.3;False;1;FLOAT;0.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;8;-2047.571,-270.611;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;65;-1880.009,-16.32558;Float;False;Constant;_Float3;Float 3;4;0;Create;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;9;-2050.146,-467.9718;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;36;-1749.309,93.02962;Float;False;Constant;_Float1;Float 1;5;0;Create;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-1683.138,-97.13642;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1491.493,124.3323;Float;False;2;2;0;FLOAT;0,0;False;1;FLOAT;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;12;-1596.469,712.6386;Float;False;Constant;_Int1;Int 1;3;0;Create;2;0;1;INT;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-1448.186,-356.5268;Float;False;Constant;_Float2;Float 2;5;0;Create;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-1398.869,563.5389;Float;False;2;2;0;FLOAT2;0,0;False;1;INT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-1297.42,152.9316;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT2;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-1216.881,-342.8986;Float;False;2;2;0;FLOAT;0,0;False;1;FLOAT;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;49;-1397.293,-528.5024;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;16;-1118.852,110.2425;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;0.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;15;-1082.97,592.5889;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.2,-1;False;1;FLOAT;0.2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;54;-77.09581,-69.49123;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-940.5332,-367.2941;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT2;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;17;-739.7693,461.2888;Float;True;Property;_TextureSample2;Texture Sample 2;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-739.6243,98.08398;Float;True;Property;_TextureSample3;Texture Sample 3;1;0;Create;Assets/Art/FXShader/Texture/T_CLoudNoiseRGB.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;33;-510.5265,-225.8213;Float;True;Property;_T_Trail;T_Trail;2;0;Create;Assets/Art/FXShader/Texture/T_Trail.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-400.1593,386.6788;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;162.5194,-65.77642;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT2;2,2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-99.09266,372.6443;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;56;322.262,-58.34644;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT2;-1,-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;266.2533,262.8752;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.7;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;29;337.8807,508.4555;Float;True;2;0;FLOAT;0.7;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;59;452.2861,-62.06146;Float;False;1;0;FLOAT2;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexColorNode;61;648.4127,-326.9482;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;39;591.6031,367.9206;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;58;600.886,-73.20621;Float;False;3;0;FLOAT2;0.0;False;1;FLOAT2;1,1;False;2;FLOAT2;0.9,0.7;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;63;916.3555,-252.3723;Float;False;1;0;COLOR;0.0;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;57;816.3505,-71.34882;Float;False;FLOAT2;1;0;FLOAT2;0.0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ClampOpNode;40;870.8221,285.3744;Float;True;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;1159.987,27.09761;Float;True;4;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;COLOR;0.0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;50;1412.838,271.361;Float;False;1;0;COLOR;0.0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;32;1729.327,229.0719;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Custom/SHA_opaqueTrail;False;False;False;False;True;True;True;True;True;False;False;False;False;False;True;True;False;Back;2;0;False;0;0;Custom;0.5;True;True;0;False;Opaque;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;6;DstColor;Zero;0;DstColor;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;44;0;43;1
WireConnection;44;1;43;3
WireConnection;4;0;44;0
WireConnection;4;1;45;0
WireConnection;5;0;4;0
WireConnection;7;0;4;0
WireConnection;8;0;6;0
WireConnection;8;1;5;0
WireConnection;9;0;6;0
WireConnection;9;1;7;0
WireConnection;11;0;9;1
WireConnection;11;1;8;2
WireConnection;11;2;65;0
WireConnection;14;0;11;0
WireConnection;14;1;36;0
WireConnection;13;0;4;0
WireConnection;13;1;12;0
WireConnection;35;0;14;0
WireConnection;35;1;4;0
WireConnection;48;0;11;0
WireConnection;48;1;47;0
WireConnection;16;0;35;0
WireConnection;15;0;13;0
WireConnection;46;0;48;0
WireConnection;46;1;49;0
WireConnection;17;0;6;0
WireConnection;17;1;15;0
WireConnection;18;0;6;0
WireConnection;18;1;16;0
WireConnection;33;1;46;0
WireConnection;19;0;18;1
WireConnection;19;1;17;2
WireConnection;55;0;54;0
WireConnection;41;0;33;1
WireConnection;41;1;19;0
WireConnection;56;0;55;0
WireConnection;21;0;41;0
WireConnection;29;1;41;0
WireConnection;59;0;56;0
WireConnection;39;0;21;0
WireConnection;39;1;29;0
WireConnection;58;0;59;0
WireConnection;63;0;61;0
WireConnection;57;0;58;0
WireConnection;40;0;39;0
WireConnection;60;0;57;0
WireConnection;60;1;57;1
WireConnection;60;2;40;0
WireConnection;60;3;63;0
WireConnection;50;0;60;0
WireConnection;32;2;50;0
ASEEND*/
//CHKSM=77FC30ACD09F7BAC64E8630F472929D330A6F7DB