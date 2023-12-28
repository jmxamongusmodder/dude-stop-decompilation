namespace Steamworks
{
    public class SteamParamStringArrayBase
    {

        // Token: 0x06000D51 RID: 3409 RVA: 0x0000FD38 File Offset: 0x0000E138
        protected override void Finalize()
        {
            try
            {
                foreach (IntPtr hglobal in this.m_Strings)
                {
                    Marshal.FreeHGlobal(hglobal);
                }
                if (this.m_ptrStrings != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(this.m_ptrStrings);
                }
                if (this.m_pSteamParamStringArray != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(this.m_pSteamParamStringArray);
                }
            }
            finally
            {
                base.Finalize();
            }
        }
    }
}