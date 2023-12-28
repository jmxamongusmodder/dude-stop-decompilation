using System;

namespace Steamworks
{
	// Token: 0x02000267 RID: 615
	[Serializable]
	public struct ControllerAnalogActionHandle_t : IEquatable<ControllerAnalogActionHandle_t>, IComparable<ControllerAnalogActionHandle_t>
	{
		// Token: 0x06000E76 RID: 3702 RVA: 0x000113F4 File Offset: 0x0000F7F4
		public ControllerAnalogActionHandle_t(ulong value)
		{
			this.m_ControllerAnalogActionHandle = value;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x000113FD File Offset: 0x0000F7FD
		public override string ToString()
		{
			return this.m_ControllerAnalogActionHandle.ToString();
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00011410 File Offset: 0x0000F810
		public override bool Equals(object other)
		{
			return other is ControllerAnalogActionHandle_t && this == (ControllerAnalogActionHandle_t)other;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00011431 File Offset: 0x0000F831
		public override int GetHashCode()
		{
			return this.m_ControllerAnalogActionHandle.GetHashCode();
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00011444 File Offset: 0x0000F844
		public static bool operator ==(ControllerAnalogActionHandle_t x, ControllerAnalogActionHandle_t y)
		{
			return x.m_ControllerAnalogActionHandle == y.m_ControllerAnalogActionHandle;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00011456 File Offset: 0x0000F856
		public static bool operator !=(ControllerAnalogActionHandle_t x, ControllerAnalogActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00011462 File Offset: 0x0000F862
		public static explicit operator ControllerAnalogActionHandle_t(ulong value)
		{
			return new ControllerAnalogActionHandle_t(value);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0001146A File Offset: 0x0000F86A
		public static explicit operator ulong(ControllerAnalogActionHandle_t that)
		{
			return that.m_ControllerAnalogActionHandle;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00011473 File Offset: 0x0000F873
		public bool Equals(ControllerAnalogActionHandle_t other)
		{
			return this.m_ControllerAnalogActionHandle == other.m_ControllerAnalogActionHandle;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00011484 File Offset: 0x0000F884
		public int CompareTo(ControllerAnalogActionHandle_t other)
		{
			return this.m_ControllerAnalogActionHandle.CompareTo(other.m_ControllerAnalogActionHandle);
		}

		// Token: 0x04000CA0 RID: 3232
		public ulong m_ControllerAnalogActionHandle;
	}
}
