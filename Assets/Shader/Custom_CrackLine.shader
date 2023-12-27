Shader "Custom/_CrackLine" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Alpha ("Alpha", Range(0, 1)) = 1
		_Range ("Range", Float) = 0
		_PixelSizeX ("Pixel size X", Float) = 0
		_PixelSizeY ("Pixel size Y", Float) = 0
		_Positions ("Positions", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend One OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZWrite Off
			Cull Off
			Fog {
				Mode Off
			}
			GpuProgramID 20797
			Program "vp" {
				SubProgram "d3d11 " {
					Keywords { "DUMMY" }
					"vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform UnityPerCamera {
						vec4 unused_0_0[5];
						vec4 _ProjectionParams;
						vec4 unused_0_2[3];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[6];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec4 vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    vs_COLOR0 = in_COLOR0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0.y = u_xlat0.y * _ProjectionParams.x;
					    u_xlat1.xzw = u_xlat0.xwy * vec3(0.5, 0.5, 0.5);
					    vs_TEXCOORD1.zw = u_xlat0.zw;
					    vs_TEXCOORD1.xy = u_xlat1.zz + u_xlat1.xw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "PIXELSNAP_ON" }
					"vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform UnityPerCamera {
						vec4 unused_0_0[5];
						vec4 _ProjectionParams;
						vec4 _ScreenParams;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[6];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec4 vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat1.xy = u_xlat0.xy / u_xlat0.ww;
					    u_xlat5.xy = _ScreenParams.xy * vec2(0.5, 0.5);
					    u_xlat1.xy = u_xlat5.xy * u_xlat1.xy;
					    u_xlat1.xy = roundEven(u_xlat1.xy);
					    u_xlat1.xy = u_xlat1.xy / u_xlat5.xy;
					    gl_Position.xy = u_xlat0.ww * u_xlat1.xy;
					    gl_Position.zw = u_xlat0.zw;
					    vs_TEXCOORD1.zw = u_xlat0.zw;
					    vs_COLOR0 = in_COLOR0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat2 = u_xlat0.y * _ProjectionParams.x;
					    u_xlat0.xz = u_xlat0.xw * vec2(0.5, 0.5);
					    u_xlat0.w = u_xlat2 * 0.5;
					    vs_TEXCOORD1.xy = u_xlat0.zz + u_xlat0.xw;
					    return;
					}"
				}
			}
			Program "fp" {
				SubProgram "d3d11 " {
					Keywords { "DUMMY" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[2];
						vec4 _MainTex_TexelSize;
						float _Alpha;
						float _PixelSizeX;
						float _PixelSizeY;
						float _Range;
						vec4 _Positions;
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bvec2 u_xlatb1;
					vec4 u_xlat2;
					vec2 u_xlat7;
					void main()
					{
					    u_xlat0.y = vs_TEXCOORD0.y + (-_MainTex_TexelSize.y);
					    u_xlat0.x = vs_TEXCOORD0.x;
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat0 = u_xlat0 * vec4(0.800000012, 0.800000012, 0.800000012, 0.800000012);
					    u_xlat0 = u_xlat0.wwww * u_xlat0;
					    u_xlat0 = u_xlat0 * vs_COLOR0;
					    u_xlat0 = u_xlat0 * vec4(_Alpha);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat1.wwww * u_xlat1;
					    u_xlat1 = u_xlat1 * vec4(_Alpha);
					    u_xlat0 = u_xlat1 * vs_COLOR0 + u_xlat0;
					    u_xlat1.xyz = _MainTex_TexelSize.yyy * vec3(-2.0, -3.0, -4.0) + vs_TEXCOORD0.yyy;
					    u_xlat1.w = vs_TEXCOORD0.x;
					    u_xlat2 = texture(_MainTex, u_xlat1.wx);
					    u_xlat2 = u_xlat2 * vec4(0.600000024, 0.600000024, 0.600000024, 0.600000024);
					    u_xlat2 = u_xlat2.wwww * u_xlat2;
					    u_xlat2 = u_xlat2 * vs_COLOR0;
					    u_xlat0 = u_xlat2 * vec4(_Alpha) + u_xlat0;
					    u_xlat2 = texture(_MainTex, u_xlat1.wy);
					    u_xlat1 = texture(_MainTex, u_xlat1.wz);
					    u_xlat1 = u_xlat1 * vec4(0.200000003, 0.200000003, 0.200000003, 0.200000003);
					    u_xlat1 = u_xlat1.wwww * u_xlat1;
					    u_xlat1 = u_xlat1 * vs_COLOR0;
					    u_xlat2 = u_xlat2 * vec4(0.400000006, 0.400000006, 0.400000006, 0.400000006);
					    u_xlat2 = u_xlat2.wwww * u_xlat2;
					    u_xlat2 = u_xlat2 * vs_COLOR0;
					    u_xlat0 = u_xlat2 * vec4(_Alpha) + u_xlat0;
					    u_xlat0 = u_xlat1 * vec4(_Alpha) + u_xlat0;
					    u_xlat1.xy = vs_TEXCOORD1.xy * vec2(_PixelSizeX, _PixelSizeY);
					    u_xlatb1.xy = greaterThanEqual(u_xlat1.xyxx, (-u_xlat1.xyxx)).xy;
					    u_xlat1.x = (u_xlatb1.x) ? float(_PixelSizeX) : (-float(_PixelSizeX));
					    u_xlat1.y = (u_xlatb1.y) ? float(_PixelSizeY) : (-float(_PixelSizeY));
					    u_xlat7.xy = vec2(1.0, 1.0) / u_xlat1.xy;
					    u_xlat7.xy = u_xlat7.xy * vs_TEXCOORD1.xy;
					    u_xlat7.xy = fract(u_xlat7.xy);
					    u_xlat1.xy = (-u_xlat1.xy) * u_xlat7.xy + vs_TEXCOORD1.xy;
					    u_xlat1.xy = u_xlat1.xy + (-_Positions.xy);
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlatb1.x = u_xlat1.x>=_Range;
					    u_xlat1.x = (u_xlatb1.x) ? 0.0 : 1.0;
					    SV_Target0 = u_xlat0 * u_xlat1.xxxx;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "PIXELSNAP_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[2];
						vec4 _MainTex_TexelSize;
						float _Alpha;
						float _PixelSizeX;
						float _PixelSizeY;
						float _Range;
						vec4 _Positions;
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bvec2 u_xlatb1;
					vec4 u_xlat2;
					vec2 u_xlat7;
					void main()
					{
					    u_xlat0.y = vs_TEXCOORD0.y + (-_MainTex_TexelSize.y);
					    u_xlat0.x = vs_TEXCOORD0.x;
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat0 = u_xlat0 * vec4(0.800000012, 0.800000012, 0.800000012, 0.800000012);
					    u_xlat0 = u_xlat0.wwww * u_xlat0;
					    u_xlat0 = u_xlat0 * vs_COLOR0;
					    u_xlat0 = u_xlat0 * vec4(_Alpha);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat1.wwww * u_xlat1;
					    u_xlat1 = u_xlat1 * vec4(_Alpha);
					    u_xlat0 = u_xlat1 * vs_COLOR0 + u_xlat0;
					    u_xlat1.xyz = _MainTex_TexelSize.yyy * vec3(-2.0, -3.0, -4.0) + vs_TEXCOORD0.yyy;
					    u_xlat1.w = vs_TEXCOORD0.x;
					    u_xlat2 = texture(_MainTex, u_xlat1.wx);
					    u_xlat2 = u_xlat2 * vec4(0.600000024, 0.600000024, 0.600000024, 0.600000024);
					    u_xlat2 = u_xlat2.wwww * u_xlat2;
					    u_xlat2 = u_xlat2 * vs_COLOR0;
					    u_xlat0 = u_xlat2 * vec4(_Alpha) + u_xlat0;
					    u_xlat2 = texture(_MainTex, u_xlat1.wy);
					    u_xlat1 = texture(_MainTex, u_xlat1.wz);
					    u_xlat1 = u_xlat1 * vec4(0.200000003, 0.200000003, 0.200000003, 0.200000003);
					    u_xlat1 = u_xlat1.wwww * u_xlat1;
					    u_xlat1 = u_xlat1 * vs_COLOR0;
					    u_xlat2 = u_xlat2 * vec4(0.400000006, 0.400000006, 0.400000006, 0.400000006);
					    u_xlat2 = u_xlat2.wwww * u_xlat2;
					    u_xlat2 = u_xlat2 * vs_COLOR0;
					    u_xlat0 = u_xlat2 * vec4(_Alpha) + u_xlat0;
					    u_xlat0 = u_xlat1 * vec4(_Alpha) + u_xlat0;
					    u_xlat1.xy = vs_TEXCOORD1.xy * vec2(_PixelSizeX, _PixelSizeY);
					    u_xlatb1.xy = greaterThanEqual(u_xlat1.xyxx, (-u_xlat1.xyxx)).xy;
					    u_xlat1.x = (u_xlatb1.x) ? float(_PixelSizeX) : (-float(_PixelSizeX));
					    u_xlat1.y = (u_xlatb1.y) ? float(_PixelSizeY) : (-float(_PixelSizeY));
					    u_xlat7.xy = vec2(1.0, 1.0) / u_xlat1.xy;
					    u_xlat7.xy = u_xlat7.xy * vs_TEXCOORD1.xy;
					    u_xlat7.xy = fract(u_xlat7.xy);
					    u_xlat1.xy = (-u_xlat1.xy) * u_xlat7.xy + vs_TEXCOORD1.xy;
					    u_xlat1.xy = u_xlat1.xy + (-_Positions.xy);
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlatb1.x = u_xlat1.x>=_Range;
					    u_xlat1.x = (u_xlatb1.x) ? 0.0 : 1.0;
					    SV_Target0 = u_xlat0 * u_xlat1.xxxx;
					    return;
					}"
				}
			}
		}
	}
}