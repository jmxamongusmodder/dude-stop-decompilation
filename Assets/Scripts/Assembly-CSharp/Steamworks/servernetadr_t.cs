using System;

namespace Steamworks
{
	// Token: 0x0200025D RID: 605
	[Serializable]
	public struct servernetadr_t
	{
		// Token: 0x06000DF1 RID: 3569 RVA: 0x000107F1 File Offset: 0x0000EBF1
		public void Init(uint ip, ushort usQueryPort, ushort usConnectionPort)
		{
			this.m_unIP = ip;
			this.m_usQueryPort = usQueryPort;
			this.m_usConnectionPort = usConnectionPort;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00010808 File Offset: 0x0000EC08
		public ushort GetQueryPort()
		{
			return this.m_usQueryPort;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00010810 File Offset: 0x0000EC10
		public void SetQueryPort(ushort usPort)
		{
			this.m_usQueryPort = usPort;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00010819 File Offset: 0x0000EC19
		public ushort GetConnectionPort()
		{
			return this.m_usConnectionPort;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00010821 File Offset: 0x0000EC21
		public void SetConnectionPort(ushort usPort)
		{
			this.m_usConnectionPort = usPort;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0001082A File Offset: 0x0000EC2A
		public uint GetIP()
		{
			return this.m_unIP;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00010832 File Offset: 0x0000EC32
		public void SetIP(uint unIP)
		{
			this.m_unIP = unIP;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0001083B File Offset: 0x0000EC3B
		public string GetConnectionAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usConnectionPort);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0001084E File Offset: 0x0000EC4E
		public string GetQueryAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usQueryPort);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00010864 File Offset: 0x0000EC64
		public static string ToString(uint unIP, ushort usPort)
		{
			return string.Format("{0}.{1}.{2}.{3}:{4}", new object[]
			{
				(ulong)(unIP >> 24) & 255UL,
				(ulong)(unIP >> 16) & 255UL,
				(ulong)(unIP >> 8) & 255UL,
				(ulong)unIP & 255UL,
				usPort
			});
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x000108D6 File Offset: 0x0000ECD6
		public static bool operator <(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP < y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort < y.m_usQueryPort);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00010914 File Offset: 0x0000ED14
		public static bool operator >(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP > y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort > y.m_usQueryPort);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00010952 File Offset: 0x0000ED52
		public override bool Equals(object other)
		{
			return other is servernetadr_t && this == (servernetadr_t)other;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00010973 File Offset: 0x0000ED73
		public override int GetHashCode()
		{
			return this.m_unIP.GetHashCode() + this.m_usQueryPort.GetHashCode() + this.m_usConnectionPort.GetHashCode();
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000109AA File Offset: 0x0000EDAA
		public static bool operator ==(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP == y.m_unIP && x.m_usQueryPort == y.m_usQueryPort && x.m_usConnectionPort == y.m_usConnectionPort;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000109E5 File Offset: 0x0000EDE5
		public static bool operator !=(servernetadr_t x, servernetadr_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x000109F1 File Offset: 0x0000EDF1
		public bool Equals(servernetadr_t other)
		{
			return this.m_unIP == other.m_unIP && this.m_usQueryPort == other.m_usQueryPort && this.m_usConnectionPort == other.m_usConnectionPort;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00010A29 File Offset: 0x0000EE29
		public int CompareTo(servernetadr_t other)
		{
			return this.m_unIP.CompareTo(other.m_unIP) + this.m_usQueryPort.CompareTo(other.m_usQueryPort) + this.m_usConnectionPort.CompareTo(other.m_usConnectionPort);
		}

		// Token: 0x04000C8C RID: 3212
		private ushort m_usConnectionPort;

		// Token: 0x04000C8D RID: 3213
		private ushort m_usQueryPort;

		// Token: 0x04000C8E RID: 3214
		private uint m_unIP;
	}
}
