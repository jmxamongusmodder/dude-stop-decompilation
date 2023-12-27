Shader "Custom/Mask Fill" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_SpriteColor ("Sprite color", Vector) = (1,1,1,1)
		_CutoutMask ("Mask", 2D) = "white" {}
		_Fill ("Fill", Range(0, 1)) = 0
		[MaterialToggle] _Reverse ("Reverse", Float) = 0
		[HideInInspector] _Left ("", Float) = 0
		[HideInInspector] _Right ("", Float) = 0
		[HideInInspector] _Top ("", Float) = 0
		[HideInInspector] _Bottom ("", Float) = 0
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
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
			GpuProgramID 48520
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
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_0_1[6];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
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
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_COLOR0 = in_COLOR0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
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
						vec4 unused_0_0[6];
						vec4 _ScreenParams;
						vec4 unused_0_2[2];
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
					    u_xlat0.xy = u_xlat0.xy / u_xlat0.ww;
					    u_xlat1.xy = _ScreenParams.xy * vec2(0.5, 0.5);
					    u_xlat0.xy = u_xlat0.xy * u_xlat1.xy;
					    u_xlat0.xy = roundEven(u_xlat0.xy);
					    u_xlat0.xy = u_xlat0.xy / u_xlat1.xy;
					    gl_Position.xy = u_xlat0.ww * u_xlat0.xy;
					    gl_Position.zw = u_xlat0.zw;
					    vs_COLOR0 = in_COLOR0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
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
						vec4 _SpriteColor;
						vec4 _MainTex_TexelSize;
						float _Fill;
						float _Reverse;
						float _Left;
						float _Right;
						float _Bottom;
						float _Top;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _CutoutMask;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					int u_xlati0;
					bool u_xlatb0;
					vec4 u_xlat1;
					int u_xlati1;
					vec4 u_xlat2;
					float u_xlat3;
					int u_xlati3;
					bool u_xlatb3;
					float u_xlat6;
					int u_xlati6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x * _MainTex_TexelSize.z + (-_Left);
					    u_xlat3 = (-_Left) + _Right;
					    u_xlat0.x = u_xlat0.x / u_xlat3;
					    u_xlat6 = vs_TEXCOORD0.y * _MainTex_TexelSize.w + (-_Bottom);
					    u_xlat9 = (-_Bottom) + _Top;
					    u_xlat0.y = u_xlat6 / u_xlat9;
					    u_xlat0 = texture(_CutoutMask, u_xlat0.xy);
					    u_xlat3 = (-_Fill) + 1.0;
					    u_xlatb3 = u_xlat3<u_xlat0.x;
					    u_xlati6 = int((vec4(0.0, 0.0, 0.0, 0.0)!=vec4(_Reverse)) ? 0xFFFFFFFFu : uint(0));
					    u_xlati1 = ~(u_xlati6);
					    u_xlati3 = u_xlatb3 ? u_xlati1 : int(0);
					    u_xlatb0 = _Fill>=u_xlat0.x;
					    u_xlati0 = u_xlatb0 ? u_xlati6 : int(0);
					    u_xlati0 = int(uint(u_xlati0) | uint(u_xlati3));
					    u_xlat3 = (-u_xlat0.w) + 1.0;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat1 * vs_COLOR0;
					    u_xlat2 = vec4(u_xlat3) * u_xlat1;
					    u_xlat2 = _SpriteColor * u_xlat0.wwww + u_xlat2;
					    u_xlat0 = (int(u_xlati0) != 0) ? u_xlat2 : u_xlat1;
					    SV_Target0 = u_xlat0.wwww * u_xlat0;
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
						vec4 _SpriteColor;
						vec4 _MainTex_TexelSize;
						float _Fill;
						float _Reverse;
						float _Left;
						float _Right;
						float _Bottom;
						float _Top;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _CutoutMask;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					int u_xlati0;
					bool u_xlatb0;
					vec4 u_xlat1;
					int u_xlati1;
					vec4 u_xlat2;
					float u_xlat3;
					int u_xlati3;
					bool u_xlatb3;
					float u_xlat6;
					int u_xlati6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x * _MainTex_TexelSize.z + (-_Left);
					    u_xlat3 = (-_Left) + _Right;
					    u_xlat0.x = u_xlat0.x / u_xlat3;
					    u_xlat6 = vs_TEXCOORD0.y * _MainTex_TexelSize.w + (-_Bottom);
					    u_xlat9 = (-_Bottom) + _Top;
					    u_xlat0.y = u_xlat6 / u_xlat9;
					    u_xlat0 = texture(_CutoutMask, u_xlat0.xy);
					    u_xlat3 = (-_Fill) + 1.0;
					    u_xlatb3 = u_xlat3<u_xlat0.x;
					    u_xlati6 = int((vec4(0.0, 0.0, 0.0, 0.0)!=vec4(_Reverse)) ? 0xFFFFFFFFu : uint(0));
					    u_xlati1 = ~(u_xlati6);
					    u_xlati3 = u_xlatb3 ? u_xlati1 : int(0);
					    u_xlatb0 = _Fill>=u_xlat0.x;
					    u_xlati0 = u_xlatb0 ? u_xlati6 : int(0);
					    u_xlati0 = int(uint(u_xlati0) | uint(u_xlati3));
					    u_xlat3 = (-u_xlat0.w) + 1.0;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat1 * vs_COLOR0;
					    u_xlat2 = vec4(u_xlat3) * u_xlat1;
					    u_xlat2 = _SpriteColor * u_xlat0.wwww + u_xlat2;
					    u_xlat0 = (int(u_xlati0) != 0) ? u_xlat2 : u_xlat1;
					    SV_Target0 = u_xlat0.wwww * u_xlat0;
					    return;
					}"
				}
			}
		}
	}
}