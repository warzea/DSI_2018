// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SHA_refractionParticle"
{
	Properties
	{
		[HideInInspector]_SpecColor("SpecularColor",Color)=(1,1,1,1)
		[Header(Refraction)]
		_T_DifformRadial("T_DifformRadial", 2D) = "bump" {}
		_ChromaticAberration("Chromatic Aberration", Range( 0 , 0.3)) = 0.1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+1" "IgnoreProjector" = "True" }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile _ALPHAPREMULTIPLY_ON
		#pragma surface surf BlinnPhong keepalpha finalcolor:RefractionF noshadow exclude_path:deferred noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
			float4 screenPos;
			float3 worldPos;
		};

		uniform sampler2D _T_DifformRadial;
		uniform float4 _T_DifformRadial_ST;
		uniform sampler2D _GrabTexture;
		uniform float _ChromaticAberration;

		inline float4 Refraction( Input i, SurfaceOutput o, float indexOfRefraction, float chomaticAberration ) {
			float3 worldNormal = o.Normal;
			float4 screenPos = i.screenPos;
			#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
			#else
				float scale = 1.0;
			#endif
			float halfPosW = screenPos.w * 0.5;
			screenPos.y = ( screenPos.y - halfPosW ) * _ProjectionParams.x * scale + halfPosW;
			#if SHADER_API_D3D9 || SHADER_API_D3D11
				screenPos.w += 0.00000000001;
			#endif
			float2 projScreenPos = ( screenPos / screenPos.w ).xy;
			float3 worldViewDir = normalize( UnityWorldSpaceViewDir( i.worldPos ) );
			float3 refractionOffset = ( ( ( ( indexOfRefraction - 1.0 ) * mul( UNITY_MATRIX_V, float4( worldNormal, 0.0 ) ) ) * ( 1.0 / ( screenPos.z + 1.0 ) ) ) * ( 1.0 - dot( worldNormal, worldViewDir ) ) );
			float2 cameraRefraction = float2( refractionOffset.x, -( refractionOffset.y * _ProjectionParams.x ) );
			float4 redAlpha = tex2D( _GrabTexture, ( projScreenPos + cameraRefraction ) );
			float green = tex2D( _GrabTexture, ( projScreenPos + ( cameraRefraction * ( 1.0 - chomaticAberration ) ) ) ).g;
			float blue = tex2D( _GrabTexture, ( projScreenPos + ( cameraRefraction * ( 1.0 + chomaticAberration ) ) ) ).b;
			return float4( redAlpha.r, green, blue, redAlpha.a );
		}

		void RefractionF( Input i, SurfaceOutput o, inout fixed4 color )
		{
			#ifdef UNITY_PASS_FORWARDBASE
			float2 uv_TexCoord9 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float smoothstepResult12 = smoothstep( 0.47 , 0.3 , distance( uv_TexCoord9 , float2( 0.5,0.5 ) ));
			float lerpResult8 = lerp( 1.0 , ( i.vertexColor.r + 1.0 ) , smoothstepResult12);
			color.rgb = color.rgb + Refraction( i, o, lerpResult8, _ChromaticAberration ) * ( 1 - color.a );
			color.a = 1;
			#endif
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Normal = float3(0,0,1);
			float2 uv_T_DifformRadial = i.uv_texcoord * _T_DifformRadial_ST.xy + _T_DifformRadial_ST.zw;
			float3 tex2DNode1 = UnpackNormal( tex2D( _T_DifformRadial, uv_T_DifformRadial ) );
			float3 appendResult14 = (float3(tex2DNode1.r , tex2DNode1.g , i.vertexColor.r));
			o.Normal = appendResult14;
			o.Alpha = 0.0;
			o.Normal = o.Normal + 0.00001 * i.screenPos * i.worldPos;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14101
1927;29;1586;824;1172;182;1;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-1157,467;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DistanceOpNode;10;-844,478;Float;False;2;0;FLOAT2;0,0,0,0;False;1;FLOAT2;0.5,0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;2;-825,210;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-813,-102;Float;True;Property;_T_DifformRadial;T_DifformRadial;1;0;Create;Assets/Art/FXShader/Texture/T_DifformRadial.png;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-392,231;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;12;-640,456;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.47;False;2;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;8;-220,204;Float;False;3;0;FLOAT;1.0;False;1;FLOAT;1.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-299,373;Float;False;Constant;_Float0;Float 0;2;0;Create;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;14;-292,-55;Float;False;FLOAT3;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;BlinnPhong;Custom/SHA_refractionParticle;False;False;False;False;True;True;True;True;True;True;True;True;False;False;True;False;False;Back;0;0;False;0;0;Translucent;0.5;True;False;1;False;Opaque;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;False;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;0;-1;0;0;0;False;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;9;0
WireConnection;5;0;2;1
WireConnection;12;0;10;0
WireConnection;8;1;5;0
WireConnection;8;2;12;0
WireConnection;14;0;1;1
WireConnection;14;1;1;2
WireConnection;14;2;2;1
WireConnection;0;1;14;0
WireConnection;0;8;8;0
WireConnection;0;9;3;0
ASEEND*/
//CHKSM=7754B53FE49E6287CE38988BC1DC448903CB490F