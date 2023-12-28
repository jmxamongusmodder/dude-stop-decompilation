using System;

namespace Steamworks
{
	// Token: 0x0200010D RID: 269
	public static class SteamHTTP
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x0000BEBC File Offset: 0x0000A2BC
		public static HTTPRequestHandle CreateHTTPRequest(EHTTPMethod eHTTPRequestMethod, string pchAbsoluteURL)
		{
			InteropHelp.TestIfAvailableClient();
			HTTPRequestHandle result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchAbsoluteURL))
			{
				result = (HTTPRequestHandle)NativeMethods.ISteamHTTP_CreateHTTPRequest(eHTTPRequestMethod, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0000BF08 File Offset: 0x0000A308
		public static bool SetHTTPRequestContextValue(HTTPRequestHandle hRequest, ulong ulContextValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SetHTTPRequestContextValue(hRequest, ulContextValue);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0000BF16 File Offset: 0x0000A316
		public static bool SetHTTPRequestNetworkActivityTimeout(HTTPRequestHandle hRequest, uint unTimeoutSeconds)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SetHTTPRequestNetworkActivityTimeout(hRequest, unTimeoutSeconds);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0000BF24 File Offset: 0x0000A324
		public static bool SetHTTPRequestHeaderValue(HTTPRequestHandle hRequest, string pchHeaderName, string pchHeaderValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchHeaderName))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchHeaderValue))
				{
					result = NativeMethods.ISteamHTTP_SetHTTPRequestHeaderValue(hRequest, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0000BF8C File Offset: 0x0000A38C
		public static bool SetHTTPRequestGetOrPostParameter(HTTPRequestHandle hRequest, string pchParamName, string pchParamValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchParamName))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchParamValue))
				{
					result = NativeMethods.ISteamHTTP_SetHTTPRequestGetOrPostParameter(hRequest, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0000BFF4 File Offset: 0x0000A3F4
		public static bool SendHTTPRequest(HTTPRequestHandle hRequest, out SteamAPICall_t pCallHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SendHTTPRequest(hRequest, out pCallHandle);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0000C002 File Offset: 0x0000A402
		public static bool SendHTTPRequestAndStreamResponse(HTTPRequestHandle hRequest, out SteamAPICall_t pCallHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SendHTTPRequestAndStreamResponse(hRequest, out pCallHandle);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0000C010 File Offset: 0x0000A410
		public static bool DeferHTTPRequest(HTTPRequestHandle hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_DeferHTTPRequest(hRequest);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0000C01D File Offset: 0x0000A41D
		public static bool PrioritizeHTTPRequest(HTTPRequestHandle hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_PrioritizeHTTPRequest(hRequest);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0000C02C File Offset: 0x0000A42C
		public static bool GetHTTPResponseHeaderSize(HTTPRequestHandle hRequest, string pchHeaderName, out uint unResponseHeaderSize)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchHeaderName))
			{
				result = NativeMethods.ISteamHTTP_GetHTTPResponseHeaderSize(hRequest, utf8StringHandle, out unResponseHeaderSize);
			}
			return result;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0000C074 File Offset: 0x0000A474
		public static bool GetHTTPResponseHeaderValue(HTTPRequestHandle hRequest, string pchHeaderName, byte[] pHeaderValueBuffer, uint unBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchHeaderName))
			{
				result = NativeMethods.ISteamHTTP_GetHTTPResponseHeaderValue(hRequest, utf8StringHandle, pHeaderValueBuffer, unBufferSize);
			}
			return result;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0000C0BC File Offset: 0x0000A4BC
		public static bool GetHTTPResponseBodySize(HTTPRequestHandle hRequest, out uint unBodySize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_GetHTTPResponseBodySize(hRequest, out unBodySize);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0000C0CA File Offset: 0x0000A4CA
		public static bool GetHTTPResponseBodyData(HTTPRequestHandle hRequest, byte[] pBodyDataBuffer, uint unBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_GetHTTPResponseBodyData(hRequest, pBodyDataBuffer, unBufferSize);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0000C0D9 File Offset: 0x0000A4D9
		public static bool GetHTTPStreamingResponseBodyData(HTTPRequestHandle hRequest, uint cOffset, byte[] pBodyDataBuffer, uint unBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_GetHTTPStreamingResponseBodyData(hRequest, cOffset, pBodyDataBuffer, unBufferSize);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0000C0E9 File Offset: 0x0000A4E9
		public static bool ReleaseHTTPRequest(HTTPRequestHandle hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_ReleaseHTTPRequest(hRequest);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0000C0F6 File Offset: 0x0000A4F6
		public static bool GetHTTPDownloadProgressPct(HTTPRequestHandle hRequest, out float pflPercentOut)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_GetHTTPDownloadProgressPct(hRequest, out pflPercentOut);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0000C104 File Offset: 0x0000A504
		public static bool SetHTTPRequestRawPostBody(HTTPRequestHandle hRequest, string pchContentType, byte[] pubBody, uint unBodyLen)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchContentType))
			{
				result = NativeMethods.ISteamHTTP_SetHTTPRequestRawPostBody(hRequest, utf8StringHandle, pubBody, unBodyLen);
			}
			return result;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0000C14C File Offset: 0x0000A54C
		public static HTTPCookieContainerHandle CreateCookieContainer(bool bAllowResponsesToModify)
		{
			InteropHelp.TestIfAvailableClient();
			return (HTTPCookieContainerHandle)NativeMethods.ISteamHTTP_CreateCookieContainer(bAllowResponsesToModify);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0000C15E File Offset: 0x0000A55E
		public static bool ReleaseCookieContainer(HTTPCookieContainerHandle hCookieContainer)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_ReleaseCookieContainer(hCookieContainer);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0000C16C File Offset: 0x0000A56C
		public static bool SetCookie(HTTPCookieContainerHandle hCookieContainer, string pchHost, string pchUrl, string pchCookie)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchHost))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchUrl))
				{
					using (InteropHelp.UTF8StringHandle utf8StringHandle3 = new InteropHelp.UTF8StringHandle(pchCookie))
					{
						result = NativeMethods.ISteamHTTP_SetCookie(hCookieContainer, utf8StringHandle, utf8StringHandle2, utf8StringHandle3);
					}
				}
			}
			return result;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0000C1F4 File Offset: 0x0000A5F4
		public static bool SetHTTPRequestCookieContainer(HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SetHTTPRequestCookieContainer(hRequest, hCookieContainer);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0000C204 File Offset: 0x0000A604
		public static bool SetHTTPRequestUserAgentInfo(HTTPRequestHandle hRequest, string pchUserAgentInfo)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchUserAgentInfo))
			{
				result = NativeMethods.ISteamHTTP_SetHTTPRequestUserAgentInfo(hRequest, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0000C248 File Offset: 0x0000A648
		public static bool SetHTTPRequestRequiresVerifiedCertificate(HTTPRequestHandle hRequest, bool bRequireVerifiedCertificate)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate(hRequest, bRequireVerifiedCertificate);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0000C256 File Offset: 0x0000A656
		public static bool SetHTTPRequestAbsoluteTimeoutMS(HTTPRequestHandle hRequest, uint unMilliseconds)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS(hRequest, unMilliseconds);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0000C264 File Offset: 0x0000A664
		public static bool GetHTTPRequestWasTimedOut(HTTPRequestHandle hRequest, out bool pbWasTimedOut)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_GetHTTPRequestWasTimedOut(hRequest, out pbWasTimedOut);
		}
	}
}
