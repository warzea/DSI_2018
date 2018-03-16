// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SHA_fogOfWar"
{
	Properties
	{
		_Color1("Color 1", Color) = (0.2794118,0.04930796,0.04930796,0)
		_Color0("Color 0", Color) = (0.5787015,0.0147059,1,0)
		_Tiling("Tiling", Float) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "ForceNoShadowCasting" = "True" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap 
		struct Input
		{
			float3 worldPos;
		};

		uniform float4 _Color0;
		uniform float4 _Color1;
		uniform float _Tiling;


		float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }

		float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }

		float snoise( float3 v )
		{
			const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
			float3 i = floor( v + dot( v, C.yyy ) );
			float3 x0 = v - i + dot( i, C.xxx );
			float3 g = step( x0.yzx, x0.xyz );
			float3 l = 1.0 - g;
			float3 i1 = min( g.xyz, l.zxy );
			float3 i2 = max( g.xyz, l.zxy );
			float3 x1 = x0 - i1 + C.xxx;
			float3 x2 = x0 - i2 + C.yyy;
			float3 x3 = x0 - 0.5;
			i = mod3D289( i);
			float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
			float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
			float4 x_ = floor( j / 7.0 );
			float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
			float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 h = 1.0 - abs( x ) - abs( y );
			float4 b0 = float4( x.xy, y.xy );
			float4 b1 = float4( x.zw, y.zw );
			float4 s0 = floor( b0 ) * 2.0 + 1.0;
			float4 s1 = floor( b1 ) * 2.0 + 1.0;
			float4 sh = -step( h, 0.0 );
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
			float3 g0 = float3( a0.xy, h.x );
			float3 g1 = float3( a0.zw, h.y );
			float3 g2 = float3( a1.xy, h.z );
			float3 g3 = float3( a1.zw, h.w );
			float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
			g0 *= norm.x;
			g1 *= norm.y;
			g2 *= norm.z;
			g3 *= norm.w;
			float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
			m = m* m;
			m = m* m;
			float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
			return 42.0 * dot( m, px);
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 temp_output_90_0 = ( ase_worldPos * _Tiling );
			float mulTime6 = _Time.y * 0.1;
			float3 appendResult13 = (float3(( temp_output_90_0 * 0.03 ).x , ( ( temp_output_90_0 * 0.03 ).y + mulTime6 ) , ( temp_output_90_0 * 0.03 ).z));
			float3 appendResult7 = (float3(( temp_output_90_0 * 0.2 ).x , ( ( temp_output_90_0 * 0.2 ).y + mulTime6 ) , ( temp_output_90_0 * 0.2 ).z));
			float simplePerlin3D1 = snoise( appendResult7 );
			float3 appendResult25 = (float3(( temp_output_90_0 * 0.5 ).x , ( ( temp_output_90_0 * 0.5 ).y + mulTime6 ) , ( temp_output_90_0 * 0.5 ).z));
			float simplePerlin3D23 = snoise( appendResult25 );
			float2 appendResult33 = (float2(temp_output_90_0.x , temp_output_90_0.z));
			float2 panner34 = ( ( appendResult33 * 1.0 ) + 1.0 * _Time.y * float2( 1,1 ));
			float dotResult67 = dot( panner34 , float2( 12.21562,5.2595 ) );
			float3 appendResult75 = (float3(( temp_output_90_0 * 0.03 ).x , ( ( temp_output_90_0 * 0.03 ).y + ( mulTime6 * 1.0 ) ) , ( temp_output_90_0 * 0.03 ).z));
			float simplePerlin3D80 = snoise( appendResult75 );
			float simplePerlin3D11 = snoise( ( appendResult13 + ( simplePerlin3D1 * 5.0 ) + ( simplePerlin3D23 * 1.0 ) + ( frac( ( sin( dotResult67 ) * 1245.0 ) ) * simplePerlin3D80 * 0.7 ) ) );
			float smoothstepResult50 = smoothstep( 0.5 , 0.4 , simplePerlin3D11);
			float clampResult60 = clamp( ( ( 1.0 - smoothstepResult50 ) + ( simplePerlin3D11 * ( 1.0 - step( simplePerlin3D11 , 0.0 ) ) ) + ( ( 1.0 - abs( simplePerlin3D11 ) ) * 0.01 ) ) , 0.0 , 1.0 );
			float4 lerpResult63 = lerp( _Color0 , _Color1 , clampResult60);
			o.Albedo = ( lerpResult63 * pow( clampResult60 , 0.5 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
7;29;1906;1044;3630.975;365.8628;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;92;-3121.981,-16.94921;Float;False;Property;_Tiling;Tiling;2;0;Create;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;2;-3288.496,-211.1154;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;90;-2851.167,-210.0897;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;91;-2614.492,348.522;Float;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;37;-1877.03,701.9773;Float;False;Constant;_Float5;Float 5;1;0;Create;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;33;-1864.233,564.6134;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-1688.897,614.8194;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-2430.713,513.5297;Float;False;Constant;_Float3;Float 3;0;0;Create;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-2554.374,1098.783;Float;False;Constant;_Float6;Float 6;0;0;Create;0.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-2441.112,147.3628;Float;False;Constant;_Float2;Float 2;0;0;Create;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;69;-1550.041,752.8326;Float;False;Constant;_Vector0;Vector 0;2;0;Create;12.21562,5.2595;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;34;-1517.669,608.5322;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;6;-1874.572,217.8582;Float;False;1;0;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;-2335.072,929.6325;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-2185.012,343.2295;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-2195.411,-22.93735;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode;67;-1299.248,631.0284;Float;False;2;0;FLOAT2;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-2445.663,-44.38725;Float;False;Constant;_Float1;Float 1;0;0;Create;0.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-1726.251,1115.014;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;77;-2175.171,929.6325;Float;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.BreakToComponentsNode;28;-2023.163,343.8797;Float;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.BreakToComponentsNode;22;-2033.562,-22.28723;Float;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-1560.762,393.9129;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-2199.962,-214.6872;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SinOpNode;70;-1162.136,634.4594;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;74;-1491.78,987.0326;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-1566.472,153.4582;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;-982.3535,616.3448;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;1245.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;7;-1417.472,72.45811;Float;False;FLOAT3;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;18;-2040.061,-214.6872;Float;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.DynamicAppendNode;75;-1284.277,921.6325;Float;False;FLOAT3;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;25;-1411.762,312.9129;Float;False;FLOAT3;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-1356.665,-157.2872;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;-905.6697,721.7867;Float;False;Constant;_Float7;Float 7;2;0;Create;0.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1183.361,-72.98717;Float;False;Constant;_Float0;Float 0;0;0;Create;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;88;-843.8566,623.8834;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-1156.062,200.663;Float;False;Constant;_Float4;Float 4;0;0;Create;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;1;-1159.771,67.55815;Float;False;Simplex3D;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;80;-1063.539,917.3051;Float;False;Simplex3D;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;23;-1158.662,304.013;Float;False;Simplex3D;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-899.9604,-45.68717;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;13;-1149.162,-222.6872;Float;False;FLOAT3;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-872.6617,227.963;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-606.7602,631.2314;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.3;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-655.5607,-140.5872;Float;False;4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0,0,0;False;2;FLOAT;0,0,0;False;3;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;11;-522.3615,-147.3874;Float;False;Simplex3D;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;55;-287.6089,136.5449;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;39;-300.6089,-62.45511;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;50;-270.6089,-255.4551;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.5;False;2;FLOAT;0.4;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;45;-164.6089,-55.45511;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;59;-153.6089,129.5449;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;0.3911133,116.5449;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-6.608887,-143.4551;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;48;-91.60889,-256.4551;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;49;157.3911,-203.4551;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;64;253.3911,-614.4551;Float;False;Property;_Color0;Color 0;1;0;Create;0.5787015,0.0147059,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;60;344.3911,-187.4551;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;65;257.3911,-451.4551;Float;False;Property;_Color1;Color 1;0;0;Create;0.2794118,0.04930796,0.04930796,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;89;556.7993,-22.65393;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;63;561.3911,-231.4551;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;765.3911,-98.45508;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1047.2,-98.79995;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/SHA_fogOfWar;False;False;False;False;True;True;True;True;True;False;False;False;False;False;True;True;False;Back;0;0;False;0;0;Opaque;0.5;True;False;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;False;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;90;0;2;0
WireConnection;90;1;92;0
WireConnection;91;0;90;0
WireConnection;33;0;91;0
WireConnection;33;1;91;2
WireConnection;36;0;33;0
WireConnection;36;1;37;0
WireConnection;34;0;36;0
WireConnection;76;0;90;0
WireConnection;76;1;78;0
WireConnection;27;0;90;0
WireConnection;27;1;26;0
WireConnection;20;0;90;0
WireConnection;20;1;21;0
WireConnection;67;0;34;0
WireConnection;67;1;69;0
WireConnection;79;0;6;0
WireConnection;77;0;76;0
WireConnection;28;0;27;0
WireConnection;22;0;20;0
WireConnection;24;0;28;1
WireConnection;24;1;6;0
WireConnection;17;0;90;0
WireConnection;17;1;19;0
WireConnection;70;0;67;0
WireConnection;74;0;77;1
WireConnection;74;1;79;0
WireConnection;5;0;22;1
WireConnection;5;1;6;0
WireConnection;87;0;70;0
WireConnection;7;0;22;0
WireConnection;7;1;5;0
WireConnection;7;2;22;2
WireConnection;18;0;17;0
WireConnection;75;0;77;0
WireConnection;75;1;74;0
WireConnection;75;2;77;2
WireConnection;25;0;28;0
WireConnection;25;1;24;0
WireConnection;25;2;28;2
WireConnection;12;0;18;1
WireConnection;12;1;6;0
WireConnection;88;0;87;0
WireConnection;1;0;7;0
WireConnection;80;0;75;0
WireConnection;23;0;25;0
WireConnection;15;0;1;0
WireConnection;15;1;16;0
WireConnection;13;0;18;0
WireConnection;13;1;12;0
WireConnection;13;2;18;2
WireConnection;31;0;23;0
WireConnection;31;1;30;0
WireConnection;73;0;88;0
WireConnection;73;1;80;0
WireConnection;73;2;81;0
WireConnection;14;0;13;0
WireConnection;14;1;15;0
WireConnection;14;2;31;0
WireConnection;14;3;73;0
WireConnection;11;0;14;0
WireConnection;55;0;11;0
WireConnection;39;0;11;0
WireConnection;50;0;11;0
WireConnection;45;0;39;0
WireConnection;59;0;55;0
WireConnection;56;0;59;0
WireConnection;44;0;11;0
WireConnection;44;1;45;0
WireConnection;48;0;50;0
WireConnection;49;0;48;0
WireConnection;49;1;44;0
WireConnection;49;2;56;0
WireConnection;60;0;49;0
WireConnection;89;0;60;0
WireConnection;63;0;64;0
WireConnection;63;1;65;0
WireConnection;63;2;60;0
WireConnection;66;0;63;0
WireConnection;66;1;89;0
WireConnection;0;0;66;0
ASEEND*/
//CHKSM=D604046767464F48B407C4BEE463C0B3515E9D3F