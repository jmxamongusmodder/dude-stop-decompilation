using System;

namespace Steamworks
{
	// Token: 0x02000262 RID: 610
	[Serializable]
	public struct CGameID : IEquatable<CGameID>, IComparable<CGameID>
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x00010BAB File Offset: 0x0000EFAB
		public CGameID(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00010BB4 File Offset: 0x0000EFB4
		public CGameID(AppId_t nAppID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00010BC5 File Offset: 0x0000EFC5
		public CGameID(AppId_t nAppID, uint nModID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
			this.SetType(CGameID.EGameIDType.k_EGameIDTypeGameMod);
			this.SetModID(nModID);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00010BE4 File Offset: 0x0000EFE4
		public bool IsSteamApp()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeApp;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00010BEF File Offset: 0x0000EFEF
		public bool IsMod()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeGameMod;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00010BFA File Offset: 0x0000EFFA
		public bool IsShortcut()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeShortcut;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00010C05 File Offset: 0x0000F005
		public bool IsP2PFile()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeP2P;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00010C10 File Offset: 0x0000F010
		public AppId_t AppID()
		{
			return new AppId_t((uint)(this.m_GameID & 16777215UL));
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00010C25 File Offset: 0x0000F025
		public CGameID.EGameIDType Type()
		{
			return (CGameID.EGameIDType)(this.m_GameID >> 24 & 255UL);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00010C38 File Offset: 0x0000F038
		public uint ModID()
		{
			return (uint)(this.m_GameID >> 32 & (ulong)-1);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00010C48 File Offset: 0x0000F048
		public bool IsValid()
		{
			switch (this.Type())
			{
			case CGameID.EGameIDType.k_EGameIDTypeApp:
				return this.AppID() != AppId_t.Invalid;
			case CGameID.EGameIDType.k_EGameIDTypeGameMod:
				return this.AppID() != AppId_t.Invalid && (this.ModID() & 2147483648U) != 0U;
			case CGameID.EGameIDType.k_EGameIDTypeShortcut:
				return (this.ModID() & 2147483648U) != 0U;
			case CGameID.EGameIDType.k_EGameIDTypeP2P:
				return this.AppID() == AppId_t.Invalid && (this.ModID() & 2147483648U) != 0U;
			default:
				return false;
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00010CF2 File Offset: 0x0000F0F2
		public void Reset()
		{
			this.m_GameID = 0UL;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00010CFC File Offset: 0x0000F0FC
		public void Set(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00010D05 File Offset: 0x0000F105
		private void SetAppID(AppId_t other)
		{
			this.m_GameID = ((this.m_GameID & 18446744073692774400UL) | ((ulong)((uint)other) & 16777215UL) << 0);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00010D2B File Offset: 0x0000F12B
		private void SetType(CGameID.EGameIDType other)
		{
			this.m_GameID = ((this.m_GameID & 18446744069431361535UL) | (ulong)((ulong)((long)other & 255L) << 24));
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00010D50 File Offset: 0x0000F150
		private void SetModID(uint other)
		{
			this.m_GameID = ((this.m_GameID & (ulong)-1) | ((ulong)other & (ulong)-1) << 32);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00010D6A File Offset: 0x0000F16A
		public override string ToString()
		{
			return this.m_GameID.ToString();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00010D7D File Offset: 0x0000F17D
		public override bool Equals(object other)
		{
			return other is CGameID && this == (CGameID)other;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00010D9E File Offset: 0x0000F19E
		public override int GetHashCode()
		{
			return this.m_GameID.GetHashCode();
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00010DB1 File Offset: 0x0000F1B1
		public static bool operator ==(CGameID x, CGameID y)
		{
			return x.m_GameID == y.m_GameID;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00010DC3 File Offset: 0x0000F1C3
		public static bool operator !=(CGameID x, CGameID y)
		{
			return !(x == y);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00010DCF File Offset: 0x0000F1CF
		public static explicit operator CGameID(ulong value)
		{
			return new CGameID(value);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00010DD7 File Offset: 0x0000F1D7
		public static explicit operator ulong(CGameID that)
		{
			return that.m_GameID;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00010DE0 File Offset: 0x0000F1E0
		public bool Equals(CGameID other)
		{
			return this.m_GameID == other.m_GameID;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00010DF1 File Offset: 0x0000F1F1
		public int CompareTo(CGameID other)
		{
			return this.m_GameID.CompareTo(other.m_GameID);
		}

		// Token: 0x04000C91 RID: 3217
		public ulong m_GameID;

		// Token: 0x02000263 RID: 611
		public enum EGameIDType
		{
			// Token: 0x04000C93 RID: 3219
			k_EGameIDTypeApp,
			// Token: 0x04000C94 RID: 3220
			k_EGameIDTypeGameMod,
			// Token: 0x04000C95 RID: 3221
			k_EGameIDTypeShortcut,
			// Token: 0x04000C96 RID: 3222
			k_EGameIDTypeP2P
		}
	}
}
