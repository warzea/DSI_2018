// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SHA_Lightning"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_T_AvatarTrail("T_AvatarTrail", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _T_AvatarTrail;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 temp_cast_0 = (1.0).xxx;
			o.Emission = temp_cast_0;
			o.Alpha = 1;
			float2 uv_TexCoord3 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			// *** BEGIN Flipbook UV Animation vars ***
			// Total tiles of Flipbook Texture
			float fbtotaltiles2 = 4.0 * 4.0;
			// Offsets for cols and rows of Flipbook Texture
			float fbcolsoffset2 = 1.0f / 4.0;
			float fbrowsoffset2 = 1.0f / 4.0;
			// Speed of animation
			float fbspeed2 = _Time[ 1 ] * 0.0;
			// UV Tiling (col and row offset)
			float2 fbtiling2 = float2(fbcolsoffset2, fbrowsoffset2);
			// UV Offset - calculate current tile linear index, and convert it to (X * coloffset, Y * rowoffset)
			// Calculate current tile linear index
			float fbcurrenttileindex2 = round( fmod( fbspeed2 + ( i.vertexColor.r * 16.0 ), fbtotaltiles2) );
			fbcurrenttileindex2 += ( fbcurrenttileindex2 < 0) ? fbtotaltiles2 : 0;
			// Obtain Offset X coordinate from current tile linear index
			float fblinearindextox2 = round ( fmod ( fbcurrenttileindex2, 4.0 ) );
			// Multiply Offset X by coloffset
			float fboffsetx2 = fblinearindextox2 * fbcolsoffset2;
			// Obtain Offset Y coordinate from current tile linear index
			float fblinearindextoy2 = round( fmod( ( fbcurrenttileindex2 - fblinearindextox2 ) / 4.0, 4.0 ) );
			// Reverse Y to get tiles from Top to Bottom
			fblinearindextoy2 = (int)(4.0-1) - fblinearindextoy2;
			// Multiply Offset Y by rowoffset
			float fboffsety2 = fblinearindextoy2 * fbrowsoffset2;
			// UV Offset
			float2 fboffset2 = float2(fboffsetx2, fboffsety2);
			// Flipbook UV
			half2 fbuv2 = uv_TexCoord3 * fbtiling2 + fboffset2;
			// *** END Flipbook UV Animation vars ***
			float4 tex2DNode1 = tex2D( _T_AvatarTrail, fbuv2 );
			clip( ( tex2DNode1.g * step( tex2DNode1.r , i.vertexColor.g ) ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
1927;29;1586;824;608.345;512.9443;1;True;True
Node;AmplifyShaderEditor.VertexColorNode;5;-1227.696,276.03;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1325.2,-87.59999;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-979.3971,140.83;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;16.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCFlipBookUVAnimation;2;-750.7,-37.3;Float;False;0;0;6;0;FLOAT2;0,0;False;1;FLOAT;4.0;False;2;FLOAT;4.0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;1;-428.3,-52.8;Float;True;Property;_T_AvatarTrail;T_AvatarTrail;1;0;Create;Assets/Art/FXShader/Texture/T_AvatarTrail.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;10;13.80258,170.73;Float;False;2;0;FLOAT;2.0;False;1;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;211.4028,109.63;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-1058.696,-8.6698;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;4,4;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;20;115.655,-105.9443;Float;False;Constant;_Float0;Float 0;2;0;Create;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;14;570.7,-5.200006;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/SHA_Lightning;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Custom;0.5;True;True;0;True;TransparentCutout;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;5;1
WireConnection;2;0;3;0
WireConnection;2;4;6;0
WireConnection;1;1;2;0
WireConnection;10;0;1;1
WireConnection;10;1;5;2
WireConnection;9;0;1;2
WireConnection;9;1;10;0
WireConnection;12;0;3;0
WireConnection;14;2;20;0
WireConnection;14;10;9;0
ASEEND*/
//CHKSM=739A1490E049AAA5B56C4714C959F1FF3D70F0D4