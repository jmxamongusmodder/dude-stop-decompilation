Shader "Custom/Mask/CupLine" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_LineTex ("Line Texture", 2D) = "white" {}
		_Alpha ("Alpha", Range(0, 1)) = 0.7
		_Distance ("Distance", Range(-2, 2)) = 0
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
			GpuProgramID 41783
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
						vec4 _MainTex_TexelSize;
						vec4 _LineTex_TexelSize;
						float _Left;
						float _Right;
						float _Bottom;
						float _Top;
						float _Alpha;
						float _Distance;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _LineTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec2 u_xlat2;
					float u_xlat3;
					float u_xlat6;
					bool u_xlatb6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xy = vec2(_Left, _Right) + vec2(-5.0, 10.0);
					    u_xlat0.x = vs_TEXCOORD0.x * _MainTex_TexelSize.z + (-u_xlat0.x);
					    u_xlat3 = u_xlat0.y + (-_Left);
					    u_xlat0.x = u_xlat0.x / u_xlat3;
					    u_xlat6 = vs_TEXCOORD0.y * _MainTex_TexelSize.w + (-_Bottom);
					    u_xlat1.xy = (-vec2(_Bottom, _Left)) + vec2(_Top, _Right);
					    u_xlat2.y = u_xlat6 / u_xlat1.x;
					    u_xlat6 = u_xlat1.y / u_xlat1.x;
					    u_xlat9 = _LineTex_TexelSize.z / _LineTex_TexelSize.w;
					    u_xlat1.x = u_xlat6 / u_xlat9;
					    u_xlatb6 = u_xlat9<u_xlat6;
					    u_xlat9 = (-u_xlat1.x) * 0.5 + 0.5;
					    u_xlat9 = (-u_xlat9) + u_xlat2.y;
					    u_xlat9 = u_xlat9 + (-_Distance);
					    u_xlat0.y = u_xlat9 / u_xlat1.x;
					    u_xlat0.y = clamp(u_xlat0.y, 0.0, 1.0);
					    u_xlat9 = float(1.0) / u_xlat1.x;
					    u_xlat1.x = (-u_xlat9) * 0.5 + 0.5;
					    u_xlat1.x = u_xlat0.x + (-u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_Distance);
					    u_xlat2.x = u_xlat1.x / u_xlat9;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat0.xy = (bool(u_xlatb6)) ? u_xlat2.xy : u_xlat0.xy;
					    u_xlat0 = texture(_LineTex, u_xlat0.xy);
					    u_xlatb0 = u_xlat0.w==0.0;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3 = u_xlat1.w * vs_COLOR0.w;
					    u_xlat3 = u_xlat3 * _Alpha;
					    SV_Target0 = (bool(u_xlatb0)) ? vec4(0.0, 0.0, 0.0, 0.0) : vec4(u_xlat3);
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
						vec4 _LineTex_TexelSize;
						float _Left;
						float _Right;
						float _Bottom;
						float _Top;
						float _Alpha;
						float _Distance;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _LineTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec2 u_xlat2;
					float u_xlat3;
					float u_xlat6;
					bool u_xlatb6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xy = vec2(_Left, _Right) + vec2(-5.0, 10.0);
					    u_xlat0.x = vs_TEXCOORD0.x * _MainTex_TexelSize.z + (-u_xlat0.x);
					    u_xlat3 = u_xlat0.y + (-_Left);
					    u_xlat0.x = u_xlat0.x / u_xlat3;
					    u_xlat6 = vs_TEXCOORD0.y * _MainTex_TexelSize.w + (-_Bottom);
					    u_xlat1.xy = (-vec2(_Bottom, _Left)) + vec2(_Top, _Right);
					    u_xlat2.y = u_xlat6 / u_xlat1.x;
					    u_xlat6 = u_xlat1.y / u_xlat1.x;
					    u_xlat9 = _LineTex_TexelSize.z / _LineTex_TexelSize.w;
					    u_xlat1.x = u_xlat6 / u_xlat9;
					    u_xlatb6 = u_xlat9<u_xlat6;
					    u_xlat9 = (-u_xlat1.x) * 0.5 + 0.5;
					    u_xlat9 = (-u_xlat9) + u_xlat2.y;
					    u_xlat9 = u_xlat9 + (-_Distance);
					    u_xlat0.y = u_xlat9 / u_xlat1.x;
					    u_xlat0.y = clamp(u_xlat0.y, 0.0, 1.0);
					    u_xlat9 = float(1.0) / u_xlat1.x;
					    u_xlat1.x = (-u_xlat9) * 0.5 + 0.5;
					    u_xlat1.x = u_xlat0.x + (-u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_Distance);
					    u_xlat2.x = u_xlat1.x / u_xlat9;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat0.xy = (bool(u_xlatb6)) ? u_xlat2.xy : u_xlat0.xy;
					    u_xlat0 = texture(_LineTex, u_xlat0.xy);
					    u_xlatb0 = u_xlat0.w==0.0;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3 = u_xlat1.w * vs_COLOR0.w;
					    u_xlat3 = u_xlat3 * _Alpha;
					    SV_Target0 = (bool(u_xlatb0)) ? vec4(0.0, 0.0, 0.0, 0.0) : vec4(u_xlat3);
					    return;
					}"
				}
			}
		}
	}
}