// Upgrade NOTE: upgraded instancing buffer 'SHA_SmokeCauldron02' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SHA_SmokeCauldron02"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.4
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_transparence("transparence", Range( 0 , 1)) = 0
		_Sub("Sub", Range( 0 , 2)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" }
		Cull Off
		Blend One One , SrcAlpha DstAlpha
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha noshadow noambient nodirlightmap nofog 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float _Cutoff = 0.4;

		UNITY_INSTANCING_BUFFER_START(SHA_SmokeCauldron02)
			UNITY_DEFINE_INSTANCED_PROP(float, _transparence)
#define _transparence_arr SHA_SmokeCauldron02
			UNITY_DEFINE_INSTANCED_PROP(float, _Sub)
#define _Sub_arr SHA_SmokeCauldron02
		UNITY_INSTANCING_BUFFER_END(SHA_SmokeCauldron02)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord3 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			// *** BEGIN Flipbook UV Animation vars ***
			// Total tiles of Flipbook Texture
			float fbtotaltiles1 = 2.0 * 2.0;
			// Offsets for cols and rows of Flipbook Texture
			float fbcolsoffset1 = 1.0f / 2.0;
			float fbrowsoffset1 = 1.0f / 2.0;
			// Speed of animation
			float fbspeed1 = _Time[ 1 ] * 6.0;
			// UV Tiling (col and row offset)
			float2 fbtiling1 = float2(fbcolsoffset1, fbrowsoffset1);
			// UV Offset - calculate current tile linear index, and convert it to (X * coloffset, Y * rowoffset)
			// Calculate current tile linear index
			float fbcurrenttileindex1 = round( fmod( fbspeed1 + 0.0, fbtotaltiles1) );
			fbcurrenttileindex1 += ( fbcurrenttileindex1 < 0) ? fbtotaltiles1 : 0;
			// Obtain Offset X coordinate from current tile linear index
			float fblinearindextox1 = round ( fmod ( fbcurrenttileindex1, 2.0 ) );
			// Multiply Offset X by coloffset
			float fboffsetx1 = fblinearindextox1 * fbcolsoffset1;
			// Obtain Offset Y coordinate from current tile linear index
			float fblinearindextoy1 = round( fmod( ( fbcurrenttileindex1 - fblinearindextox1 ) / 2.0, 2.0 ) );
			// Reverse Y to get tiles from Top to Bottom
			fblinearindextoy1 = (int)(2.0-1) - fblinearindextoy1;
			// Multiply Offset Y by rowoffset
			float fboffsety1 = fblinearindextoy1 * fbrowsoffset1;
			// UV Offset
			float2 fboffset1 = float2(fboffsetx1, fboffsety1);
			// Flipbook UV
			half2 fbuv1 = uv_TexCoord3 * fbtiling1 + fboffset1;
			// *** END Flipbook UV Animation vars ***
			float _transparence_Instance = UNITY_ACCESS_INSTANCED_PROP(_transparence_arr, _transparence);
			float _Sub_Instance = UNITY_ACCESS_INSTANCED_PROP(_Sub_arr, _Sub);
			float clampResult14 = clamp( ( ( tex2D( _TextureSample0, fbuv1 ).a + _transparence_Instance ) - ( 0.0 + _Sub_Instance ) ) , 0.0 , 1.0 );
			o.Albedo = ( i.vertexColor * clampResult14 ).rgb;
			o.Alpha = clampResult14;
			clip( clampResult14 - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
544;135;923;722;808.2666;335.5154;2.165415;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-791.2247,84.01248;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCFlipBookUVAnimation;1;-622.0739,484.0401;Float;False;0;0;6;0;FLOAT2;0,0;False;1;FLOAT;2.0;False;2;FLOAT;2.0;False;3;FLOAT;6.0;False;4;FLOAT;0.0;False;5;FLOAT;2.0;False;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;4;-255.8301,48.4183;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;Assets/Art/FXShader/Texture/SmokeCartoon_01.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-277.1282,257.1894;Float;False;InstancedProperty;_transparence;transparence;2;0;Create;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-245.5037,-50.70097;Float;False;InstancedProperty;_Sub;Sub;3;0;Create;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;42.99712,225.5635;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;112.4267,-187.7985;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;13;206.3959,-27.9919;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;16;-220.1038,-251.7003;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;14;123.4344,664.6756;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;502.6686,-153.4594;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;19;534.5628,16.83005;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;SHA_SmokeCauldron02;False;False;False;False;True;False;False;False;True;True;False;False;False;False;False;False;False;Off;0;0;False;0;0;Custom;0.4;True;False;0;True;TransparentCutout;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;False;4;One;One;1;SrcAlpha;DstAlpha;OFF;Add;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1;0;3;0
WireConnection;4;1;1;0
WireConnection;8;0;4;4
WireConnection;8;1;7;0
WireConnection;18;1;15;0
WireConnection;13;0;8;0
WireConnection;13;1;18;0
WireConnection;14;0;13;0
WireConnection;17;0;16;0
WireConnection;17;1;14;0
WireConnection;19;0;17;0
WireConnection;19;9;14;0
WireConnection;19;10;14;0
ASEEND*/
//CHKSM=DA64B7A3B35AC51876BA6834FEB9A3C9A79CFA8F