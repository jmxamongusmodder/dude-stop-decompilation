using System;

namespace Steamworks
{
	// Token: 0x02000269 RID: 617
	[Serializable]
	public struct ControllerHandle_t : IEquatable<ControllerHandle_t>, IComparable<ControllerHandle_t>
	{
		// Token: 0x06000E8A RID: 3722 RVA: 0x0001153C File Offset: 0x0000F93C
		public ControllerHandle_t(ulong value)
		{
			this.m_ControllerHandle = value;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00011545 File Offset: 0x0000F945
		public override string ToString()
		{
			return this.m_ControllerHandle.ToString();
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00011558 File Offset: 0x0000F958
		public override bool Equals(object other)
		{
			return other is ControllerHandle_t && this == (ControllerHandle_t)other;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00011579 File Offset: 0x0000F979
		public override int GetHashCode()
		{
			return this.m_ControllerHandle.GetHashCode();
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0001158C File Offset: 0x0000F98C
		public static bool operator ==(ControllerHandle_t x, ControllerHandle_t y)
		{
			return x.m_ControllerHandle == y.m_ControllerHandle;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0001159E File Offset: 0x0000F99E
		public static bool operator !=(ControllerHandle_t x, ControllerHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x000115AA File Offset: 0x0000F9AA
		public static explicit operator ControllerHandle_t(ulong value)
		{
			return new ControllerHandle_t(value);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000115B2 File Offset: 0x0000F9B2
		public static explicit operator ulong(ControllerHandle_t that)
		{
			return that.m_ControllerHandle;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000115BB File Offset: 0x0000F9BB
		public bool Equals(ControllerHandle_t other)
		{
			return this.m_ControllerHandle == other.m_ControllerHandle;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000115CC File Offset: 0x0000F9CC
		public int CompareTo(ControllerHandle_t other)
		{
			return this.m_ControllerHandle.CompareTo(other.m_ControllerHandle);
		}

		// Token: 0x04000CA2 RID: 3234
		public ulong m_ControllerHandle;
	}
}
