Shader "Custom/Mask/Box" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Bottom ("Bottom", Range(0, 1)) = 0
		_Left ("Left", Range(0, 1)) = 0
		_Width ("Width", Range(0, 1)) = 0
		_Height ("Height", Range(0, 1)) = 0
		_Angle ("Angle", Range(0, 3.14)) = 0
		[MaterialToggle] _AlphaOnly ("Alpha only", Float) = 0
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
			GpuProgramID 30193
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
						float _Bottom;
						float _Left;
						float _Angle;
						float _Width;
						float _Height;
						float _AlphaOnly;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2[2];
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					bool u_xlatb2;
					vec3 u_xlat3;
					vec3 u_xlat5;
					bool u_xlatb5;
					float u_xlat9;
					bool u_xlatb9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlatb0 = vec4(0.0, 0.0, 0.0, 0.0)!=vec4(_AlphaOnly);
					    if(u_xlatb0){
					        u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					        u_xlat0.w = u_xlat0.w * vs_COLOR0.w;
					        u_xlat0.xyz = vs_COLOR0.xyz;
					    } else {
					        u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					        u_xlat0 = u_xlat1 * vs_COLOR0;
					    }
					    u_xlat1.x = sin(_Angle);
					    u_xlat2 = cos(_Angle);
					    u_xlat3.x = (-u_xlat1.x);
					    u_xlat5.xyz = vec3(_Left, _Bottom, _Width) * _ScreenParams.xyx;
					    u_xlat5.xy = vs_TEXCOORD1.xy * _ScreenParams.xy + (-u_xlat5.xy);
					    u_xlat3.y = u_xlat2;
					    u_xlat3.z = u_xlat1.x;
					    u_xlat1.x = dot(u_xlat5.xy, u_xlat3.yz);
					    u_xlat5.x = dot(u_xlat5.xy, u_xlat3.xy);
					    u_xlat9 = u_xlat5.z * 0.5;
					    u_xlat13 = _Height * _ScreenParams.y;
					    u_xlat13 = u_xlat13 * 0.5;
					    u_xlatb2 = u_xlat1.x<u_xlat9;
					    u_xlatb1 = (-u_xlat9)<u_xlat1.x;
					    u_xlatb1 = u_xlatb1 && u_xlatb2;
					    u_xlatb9 = u_xlat5.x<u_xlat13;
					    u_xlatb1 = u_xlatb9 && u_xlatb1;
					    u_xlatb5 = (-u_xlat13)<u_xlat5.x;
					    u_xlatb1 = u_xlatb5 && u_xlatb1;
					    u_xlat12 = u_xlatb1 ? u_xlat0.w : float(0.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
					    SV_Target0.w = u_xlat12;
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
						float _Bottom;
						float _Left;
						float _Angle;
						float _Width;
						float _Height;
						float _AlphaOnly;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2[2];
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					bool u_xlatb2;
					vec3 u_xlat3;
					vec3 u_xlat5;
					bool u_xlatb5;
					float u_xlat9;
					bool u_xlatb9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlatb0 = vec4(0.0, 0.0, 0.0, 0.0)!=vec4(_AlphaOnly);
					    if(u_xlatb0){
					        u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					        u_xlat0.w = u_xlat0.w * vs_COLOR0.w;
					        u_xlat0.xyz = vs_COLOR0.xyz;
					    } else {
					        u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					        u_xlat0 = u_xlat1 * vs_COLOR0;
					    }
					    u_xlat1.x = sin(_Angle);
					    u_xlat2 = cos(_Angle);
					    u_xlat3.x = (-u_xlat1.x);
					    u_xlat5.xyz = vec3(_Left, _Bottom, _Width) * _ScreenParams.xyx;
					    u_xlat5.xy = vs_TEXCOORD1.xy * _ScreenParams.xy + (-u_xlat5.xy);
					    u_xlat3.y = u_xlat2;
					    u_xlat3.z = u_xlat1.x;
					    u_xlat1.x = dot(u_xlat5.xy, u_xlat3.yz);
					    u_xlat5.x = dot(u_xlat5.xy, u_xlat3.xy);
					    u_xlat9 = u_xlat5.z * 0.5;
					    u_xlat13 = _Height * _ScreenParams.y;
					    u_xlat13 = u_xlat13 * 0.5;
					    u_xlatb2 = u_xlat1.x<u_xlat9;
					    u_xlatb1 = (-u_xlat9)<u_xlat1.x;
					    u_xlatb1 = u_xlatb1 && u_xlatb2;
					    u_xlatb9 = u_xlat5.x<u_xlat13;
					    u_xlatb1 = u_xlatb9 && u_xlatb1;
					    u_xlatb5 = (-u_xlat13)<u_xlat5.x;
					    u_xlatb1 = u_xlatb5 && u_xlatb1;
					    u_xlat12 = u_xlatb1 ? u_xlat0.w : float(0.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
					    SV_Target0.w = u_xlat12;
					    return;
					}"
				}
			}
		}
	}
}