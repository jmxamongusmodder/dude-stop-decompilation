using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD
{
	// Token: 0x0200006A RID: 106
	internal static class StringHelper
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x00006EE0 File Offset: 0x000052E0
		public static StringHelper.ThreadSafeEncoding GetFreeHelper()
		{
			object obj = StringHelper.encoders;
			StringHelper.ThreadSafeEncoding result;
			lock (obj)
			{
				StringHelper.ThreadSafeEncoding threadSafeEncoding = null;
				for (int i = 0; i < StringHelper.encoders.Count; i++)
				{
					if (!StringHelper.encoders[i].InUse())
					{
						threadSafeEncoding = StringHelper.encoders[i];
						break;
					}
				}
				if (threadSafeEncoding == null)
				{
					threadSafeEncoding = new StringHelper.ThreadSafeEncoding();
					StringHelper.encoders.Add(threadSafeEncoding);
				}
				threadSafeEncoding.SetInUse();
				result = threadSafeEncoding;
			}
			return result;
		}

		// Token: 0x0400028E RID: 654
		private static List<StringHelper.ThreadSafeEncoding> encoders = new List<StringHelper.ThreadSafeEncoding>(1);

		// Token: 0x0200006B RID: 107
		public class ThreadSafeEncoding : IDisposable
		{
			// Token: 0x06000453 RID: 1107 RVA: 0x00006FB8 File Offset: 0x000053B8
			public bool InUse()
			{
				return this.inUse;
			}

			// Token: 0x06000454 RID: 1108 RVA: 0x00006FC0 File Offset: 0x000053C0
			public void SetInUse()
			{
				this.inUse = true;
			}

			// Token: 0x06000455 RID: 1109 RVA: 0x00006FCC File Offset: 0x000053CC
			private int roundUpPowerTwo(int number)
			{
				int i;
				for (i = 1; i <= number; i *= 2)
				{
				}
				return i;
			}

			// Token: 0x06000456 RID: 1110 RVA: 0x00006FEC File Offset: 0x000053EC
			public byte[] byteFromStringUTF8(string s)
			{
				if (s == null)
				{
					return null;
				}
				int num = this.encoding.GetMaxByteCount(s.Length) + 1;
				if (num > this.encodedBuffer.Length)
				{
					int num2 = this.encoding.GetByteCount(s) + 1;
					if (num2 > this.encodedBuffer.Length)
					{
						this.encodedBuffer = new byte[this.roundUpPowerTwo(num2)];
					}
				}
				int bytes = this.encoding.GetBytes(s, 0, s.Length, this.encodedBuffer, 0);
				this.encodedBuffer[bytes] = 0;
				return this.encodedBuffer;
			}

			// Token: 0x06000457 RID: 1111 RVA: 0x0000707C File Offset: 0x0000547C
			public string stringFromNative(IntPtr nativePtr)
			{
				if (nativePtr == IntPtr.Zero)
				{
					return string.Empty;
				}
				int num = 0;
				while (Marshal.ReadByte(nativePtr, num) != 0)
				{
					num++;
				}
				if (num == 0)
				{
					return string.Empty;
				}
				if (num > this.encodedBuffer.Length)
				{
					this.encodedBuffer = new byte[this.roundUpPowerTwo(num)];
				}
				Marshal.Copy(nativePtr, this.encodedBuffer, 0, num);
				int maxCharCount = this.encoding.GetMaxCharCount(num);
				if (maxCharCount > this.decodedBuffer.Length)
				{
					int charCount = this.encoding.GetCharCount(this.encodedBuffer, 0, num);
					if (charCount > this.decodedBuffer.Length)
					{
						this.decodedBuffer = new char[this.roundUpPowerTwo(charCount)];
					}
				}
				int chars = this.encoding.GetChars(this.encodedBuffer, 0, num, this.decodedBuffer, 0);
				return new string(this.decodedBuffer, 0, chars);
			}

			// Token: 0x06000458 RID: 1112 RVA: 0x00007168 File Offset: 0x00005568
			public void Dispose()
			{
				object encoders = StringHelper.encoders;
				lock (encoders)
				{
					this.inUse = false;
				}
			}

			// Token: 0x0400028F RID: 655
			private UTF8Encoding encoding = new UTF8Encoding();

			// Token: 0x04000290 RID: 656
			private byte[] encodedBuffer = new byte[128];

			// Token: 0x04000291 RID: 657
			private char[] decodedBuffer = new char[128];

			// Token: 0x04000292 RID: 658
			private bool inUse;
		}
	}
}
