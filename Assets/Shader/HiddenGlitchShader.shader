Shader "Hidden/GlitchShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Pass {
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode Off
			}
			GpuProgramID 13667
			Program "vp" {
				SubProgram "d3d11 " {
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
					in  vec2 in_TEXCOORD0;
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
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}"
				}
			}
			Program "fp" {
				SubProgram "d3d11 " {
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
					vec4 ImmCB_0_0_0[4];
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[2];
						float displace;
						float scale;
						float originalAlpha;
						vec4 addColor;
						float shift;
						int _X;
						int _Y;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _DispTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					int u_xlati0;
					vec4 u_xlat1;
					int u_xlati1;
					vec3 u_xlat2;
					vec2 u_xlat3;
					void main()
					{
						ImmCB_0_0_0[0] = vec4(1.0, 0.0, 0.0, 0.0);
						ImmCB_0_0_0[1] = vec4(0.0, 1.0, 0.0, 0.0);
						ImmCB_0_0_0[2] = vec4(0.0, 0.0, 1.0, 0.0);
						ImmCB_0_0_0[3] = vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlati0 = _X;
					    u_xlat0.x = dot(vs_TEXCOORD0.xy, ImmCB_0_0_0[u_xlati0].xy);
					    u_xlat0.x = u_xlat0.x * scale + shift;
					    u_xlat0 = texture(_DispTex, u_xlat0.xx);
					    u_xlati1 = _Y;
					    u_xlat0.x = dot(u_xlat0, ImmCB_0_0_0[u_xlati1]);
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat3.x + 1.0;
					    u_xlat0.x = u_xlat0.x * displace;
					    u_xlat3.xy = vec2(ivec2(_X, _Y));
					    u_xlat0.xy = u_xlat0.xx * u_xlat3.xy + vs_TEXCOORD0.xy;
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0 = u_xlat0 + (-u_xlat1);
					    u_xlat0 = vec4(vec4(originalAlpha, originalAlpha, originalAlpha, originalAlpha)) * u_xlat0 + u_xlat1;
					    u_xlat1.x = abs(u_xlat2.y) + abs(u_xlat2.x);
					    u_xlat1.x = abs(u_xlat2.z) + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x + 0.999000013;
					    u_xlat1.x = floor(u_xlat1.x);
					    SV_Target0.xyz = addColor.xyz * u_xlat1.xxx + u_xlat0.xyz;
					    SV_Target0.w = u_xlat0.w;
					    return;
					}"
				}
			}
		}
	}
}