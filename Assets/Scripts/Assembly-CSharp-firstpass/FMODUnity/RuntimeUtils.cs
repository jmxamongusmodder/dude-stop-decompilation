using System;
using System.IO;
using FMOD;
using FMOD.Studio;
using UnityEngine;

namespace FMODUnity
{
	// Token: 0x02000014 RID: 20
	public static class RuntimeUtils
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000362C File Offset: 0x00001A2C
		public static VECTOR ToFMODVector(this Vector3 vec)
		{
			VECTOR result;
			result.x = vec.x;
			result.y = vec.y;
			result.z = vec.z;
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003664 File Offset: 0x00001A64
		public static ATTRIBUTES_3D To3DAttributes(this Vector3 pos)
		{
			return new ATTRIBUTES_3D
			{
				forward = Vector3.forward.ToFMODVector(),
				up = Vector3.up.ToFMODVector(),
				position = pos.ToFMODVector()
			};
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000036AC File Offset: 0x00001AAC
		public static ATTRIBUTES_3D To3DAttributes(this Transform transform)
		{
			return new ATTRIBUTES_3D
			{
				forward = transform.forward.ToFMODVector(),
				up = transform.up.ToFMODVector(),
				position = transform.position.ToFMODVector()
			};
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000036F8 File Offset: 0x00001AF8
		public static ATTRIBUTES_3D To3DAttributes(Transform transform, Rigidbody rigidbody = null)
		{
			ATTRIBUTES_3D result = transform.To3DAttributes();
			if (rigidbody)
			{
				result.velocity = rigidbody.velocity.ToFMODVector();
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000372C File Offset: 0x00001B2C
		public static ATTRIBUTES_3D To3DAttributes(GameObject go, Rigidbody rigidbody = null)
		{
			ATTRIBUTES_3D result = go.transform.To3DAttributes();
			if (rigidbody)
			{
				result.velocity = rigidbody.velocity.ToFMODVector();
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003764 File Offset: 0x00001B64
		public static ATTRIBUTES_3D To3DAttributes(Transform transform, Rigidbody2D rigidbody)
		{
			ATTRIBUTES_3D result = transform.To3DAttributes();
			if (rigidbody)
			{
				VECTOR velocity;
				velocity.x = rigidbody.velocity.x;
				velocity.y = rigidbody.velocity.y;
				velocity.z = 0f;
				result.velocity = velocity;
			}
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000037C4 File Offset: 0x00001BC4
		public static ATTRIBUTES_3D To3DAttributes(GameObject go, Rigidbody2D rigidbody)
		{
			ATTRIBUTES_3D result = go.transform.To3DAttributes();
			if (rigidbody)
			{
				VECTOR velocity;
				velocity.x = rigidbody.velocity.x;
				velocity.y = rigidbody.velocity.y;
				velocity.z = 0f;
				result.velocity = velocity;
			}
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003827 File Offset: 0x00001C27
		internal static FMODPlatform GetCurrentPlatform()
		{
			return FMODPlatform.Windows;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000382C File Offset: 0x00001C2C
		internal static string GetBankPath(string bankName)
		{
			string streamingAssetsPath = Application.streamingAssetsPath;
			if (Path.GetExtension(bankName) != ".bank")
			{
				return string.Format("{0}/{1}.bank", streamingAssetsPath, bankName);
			}
			return string.Format("{0}/{1}", streamingAssetsPath, bankName);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003870 File Offset: 0x00001C70
		internal static string GetPluginPath(string pluginName)
		{
			string str = pluginName + ".dll";
			string str2 = Application.dataPath + "/Plugins/";
			return str2 + str;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000038A0 File Offset: 0x00001CA0
		public static void EnforceLibraryOrder()
		{
			int num;
			int num2;
			Memory.GetStats(out num, out num2);
			Guid guid;
			Util.ParseID(string.Empty, out guid);
		}

		// Token: 0x04000040 RID: 64
		public const string LogFileName = "fmod.log";

		// Token: 0x04000041 RID: 65
		private const string BankExtension = ".bank";
	}
}
