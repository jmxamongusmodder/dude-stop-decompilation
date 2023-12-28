using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000264 RID: 612
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct CSteamID : IEquatable<CSteamID>, IComparable<CSteamID>
	{
		// Token: 0x06000E38 RID: 3640 RVA: 0x00010E05 File Offset: 0x0000F205
		public CSteamID(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.Set(unAccountID, eUniverse, eAccountType);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00010E18 File Offset: 0x0000F218
		public CSteamID(AccountID_t unAccountID, uint unAccountInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.InstancedSet(unAccountID, unAccountInstance, eUniverse, eAccountType);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00010E2D File Offset: 0x0000F22D
		public CSteamID(ulong ulSteamID)
		{
			this.m_SteamID = ulSteamID;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00010E36 File Offset: 0x0000F236
		public void Set(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			if (eAccountType == EAccountType.k_EAccountTypeClan || eAccountType == EAccountType.k_EAccountTypeGameServer)
			{
				this.SetAccountInstance(0U);
			}
			else
			{
				this.SetAccountInstance(1U);
			}
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00010E6E File Offset: 0x0000F26E
		public void InstancedSet(AccountID_t unAccountID, uint unInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			this.SetAccountInstance(unInstance);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00010E8D File Offset: 0x0000F28D
		public void Clear()
		{
			this.m_SteamID = 0UL;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00010E97 File Offset: 0x0000F297
		public void CreateBlankAnonLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonGameServer);
			this.SetAccountInstance(0U);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00010EBA File Offset: 0x0000F2BA
		public void CreateBlankAnonUserLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonUser);
			this.SetAccountInstance(0U);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00010EDE File Offset: 0x0000F2DE
		public bool BBlankAnonAccount()
		{
			return this.GetAccountID() == new AccountID_t(0U) && this.BAnonAccount() && this.GetUnAccountInstance() == 0U;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00010F0D File Offset: 0x0000F30D
		public bool BGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00010F27 File Offset: 0x0000F327
		public bool BPersistentGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00010F32 File Offset: 0x0000F332
		public bool BAnonGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00010F3D File Offset: 0x0000F33D
		public bool BContentServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeContentServer;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00010F48 File Offset: 0x0000F348
		public bool BClanAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeClan;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00010F53 File Offset: 0x0000F353
		public bool BChatAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat;
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00010F5E File Offset: 0x0000F35E
		public bool IsLobby()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat && (this.GetUnAccountInstance() & 262144U) != 0U;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00010F81 File Offset: 0x0000F381
		public bool BIndividualAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeIndividual || this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00010F9C File Offset: 0x0000F39C
		public bool BAnonAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00010FB7 File Offset: 0x0000F3B7
		public bool BAnonUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00010FC3 File Offset: 0x0000F3C3
		public bool BConsoleUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00010FCF File Offset: 0x0000F3CF
		public void SetAccountID(AccountID_t other)
		{
			this.m_SteamID = ((this.m_SteamID & 18446744069414584320UL) | ((ulong)((uint)other) & (ulong)-1) << 0);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00010FF4 File Offset: 0x0000F3F4
		public void SetAccountInstance(uint other)
		{
			this.m_SteamID = ((this.m_SteamID & 18442240478377148415UL) | ((ulong)other & 1048575UL) << 32);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00011019 File Offset: 0x0000F419
		public void SetEAccountType(EAccountType other)
		{
			this.m_SteamID = ((this.m_SteamID & 18379190079298994175UL) | (ulong)((ulong)((long)other & 15L) << 52));
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0001103B File Offset: 0x0000F43B
		public void SetEUniverse(EUniverse other)
		{
			this.m_SteamID = ((this.m_SteamID & 72057594037927935UL) | (ulong)((ulong)((long)other & 255L) << 56));
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00011060 File Offset: 0x0000F460
		public void ClearIndividualInstance()
		{
			if (this.BIndividualAccount())
			{
				this.SetAccountInstance(0U);
			}
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00011074 File Offset: 0x0000F474
		public bool HasNoIndividualInstance()
		{
			return this.BIndividualAccount() && this.GetUnAccountInstance() == 0U;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0001108D File Offset: 0x0000F48D
		public AccountID_t GetAccountID()
		{
			return new AccountID_t((uint)(this.m_SteamID & (ulong)-1));
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0001109E File Offset: 0x0000F49E
		public uint GetUnAccountInstance()
		{
			return (uint)(this.m_SteamID >> 32 & 1048575UL);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x000110B1 File Offset: 0x0000F4B1
		public EAccountType GetEAccountType()
		{
			return (EAccountType)(this.m_SteamID >> 52 & 15UL);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x000110C1 File Offset: 0x0000F4C1
		public EUniverse GetEUniverse()
		{
			return (EUniverse)(this.m_SteamID >> 56 & 255UL);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x000110D4 File Offset: 0x0000F4D4
		public bool IsValid()
		{
			return this.GetEAccountType() > EAccountType.k_EAccountTypeInvalid && this.GetEAccountType() < EAccountType.k_EAccountTypeMax && this.GetEUniverse() > EUniverse.k_EUniverseInvalid && this.GetEUniverse() < EUniverse.k_EUniverseMax && (this.GetEAccountType() != EAccountType.k_EAccountTypeIndividual || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() <= 4U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeClan || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() == 0U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeGameServer || !(this.GetAccountID() == new AccountID_t(0U)));
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0001119A File Offset: 0x0000F59A
		public override string ToString()
		{
			return this.m_SteamID.ToString();
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x000111AD File Offset: 0x0000F5AD
		public override bool Equals(object other)
		{
			return other is CSteamID && this == (CSteamID)other;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000111CE File Offset: 0x0000F5CE
		public override int GetHashCode()
		{
			return this.m_SteamID.GetHashCode();
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x000111E1 File Offset: 0x0000F5E1
		public static bool operator ==(CSteamID x, CSteamID y)
		{
			return x.m_SteamID == y.m_SteamID;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000111F3 File Offset: 0x0000F5F3
		public static bool operator !=(CSteamID x, CSteamID y)
		{
			return !(x == y);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000111FF File Offset: 0x0000F5FF
		public static explicit operator CSteamID(ulong value)
		{
			return new CSteamID(value);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00011207 File Offset: 0x0000F607
		public static explicit operator ulong(CSteamID that)
		{
			return that.m_SteamID;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00011210 File Offset: 0x0000F610
		public bool Equals(CSteamID other)
		{
			return this.m_SteamID == other.m_SteamID;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00011221 File Offset: 0x0000F621
		public int CompareTo(CSteamID other)
		{
			return this.m_SteamID.CompareTo(other.m_SteamID);
		}

		// Token: 0x04000C97 RID: 3223
		public static readonly CSteamID Nil = default(CSteamID);

		// Token: 0x04000C98 RID: 3224
		public static readonly CSteamID OutofDateGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000C99 RID: 3225
		public static readonly CSteamID LanModeGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniversePublic, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000C9A RID: 3226
		public static readonly CSteamID NotInitYetGS = new CSteamID(new AccountID_t(1U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000C9B RID: 3227
		public static readonly CSteamID NonSteamGS = new CSteamID(new AccountID_t(2U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000C9C RID: 3228
		public ulong m_SteamID;
	}
}
