namespace Impersonation
{
	#region Using directives.
	// ----------------------------------------------------------------------

	using System;
	using System.Security.Principal;
	using System.Runtime.InteropServices;
	using System.ComponentModel;

	// ----------------------------------------------------------------------
	#endregion

	/////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Impersonation of a user. Allows to execute code under another
	/// user context.
	/// Please note that the account that instantiates the Impersonator class
	/// needs to have the 'Act as part of operating system' privilege set.
	/// </summary>
	/// <remarks>	
	/// This class is based on the information in the Microsoft knowledge base
	/// article http://support.microsoft.com/default.aspx?scid=kb;en-us;Q306158
	/// 
	/// Encapsulate an instance into a using-directive like e.g.:
	/// 
	///		...
	///		using ( new Impersonator( "myUsername", "myDomainname", "myPassword" ) )
	///		{
	///			...
	///			[code that executes under the new context]
	///			...
	///		}
	///		...
	/// 
	/// Please contact the author Uwe Keim (mailto:uwe.keim@zeta-software.de)
	/// for questions regarding this class.
	/// </remarks>
	public class Impersonator :
		IDisposable
	{
		#region Public methods.
		// ------------------------------------------------------------------

		/// <summary>
		/// Constructor. Starts the impersonation with the given credentials.
		/// Please note that the account that instantiates the Impersonator class
		/// needs to have the 'Act as part of operating system' privilege set.
		/// </summary>
		/// <param name="userName">The name of the user to act as.</param>
		/// <param name="domainName">The domain name of the user to act as.</param>
		/// <param name="password">The password of the user to act as.</param>
		public Impersonator(
			string userName,
			string domainName,
			string password )
		{
			ImpersonateValidUser( userName, domainName, password );
		}

		// ------------------------------------------------------------------
		#endregion

		#region IDisposable member.
		// ------------------------------------------------------------------

		public void Dispose()
		{
			UndoImpersonation();
		}

		// ------------------------------------------------------------------
		#endregion

		#region P/Invoke.
		// ------------------------------------------------------------------

		[DllImport("advapi32.dll", SetLastError=true)]
		private static extern int LogonUser(
			string lpszUserName,
			string lpszDomain,
			string lpszPassword,
			int dwLogonType,
			int dwLogonProvider,
			ref IntPtr phToken);
        [DllImport("advapi32.dll")]
        private extern static int CheckTokenMembership(IntPtr TokenHandle, IntPtr SidToCheck, ref int IsMember);
        [DllImport("advapi32.dll")]
        private extern static int AllocateAndInitializeSid(byte[] pIdentifierAuthority, byte nSubAuthorityCount, int dwSubAuthority0, int dwSubAuthority1, int dwSubAuthority2, int dwSubAuthority3, int dwSubAuthority4, int dwSubAuthority5, int dwSubAuthority6, int dwSubAuthority7, out IntPtr pSid);
    
		[DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern int DuplicateToken(
			IntPtr hToken,
			int impersonationLevel,
			ref IntPtr hNewToken);

		[DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern bool RevertToSelf();

		[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
		private static extern  bool CloseHandle(
			IntPtr handle);

        private const int LOGON32_LOGON_SERVICE = 5;
		private const int LOGON32_LOGON_INTERACTIVE = 2;
		private const int LOGON32_PROVIDER_DEFAULT = 0;

		// ------------------------------------------------------------------
		#endregion

		#region Private member.
		// ------------------------------------------------------------------
        #region Private Fields

        /// <summary>
        ///    SECURITY_BUILTIN_DOMAIN_RID.
        /// </summary>
        private const int SECURITY_BUILTIN_DOMAIN_RID = 0x20;

        /// <summary>
        ///    DOMAIN_ALIAS_RID_ADMINS.
        /// </summary>
        private const int DOMAIN_ALIAS_RID_ADMINS = 0x220;

        /// <summary>
        ///    The option for recieving all packets.
        /// </summary>
        private const int RecieveAll = unchecked((int)0x98000001);
        #endregion
        /// <summary>
		/// Does the actual impersonation.
		/// </summary>
		/// <param name="userName">The name of the user to act as.</param>
		/// <param name="domainName">The domain name of the user to act as.</param>
		/// <param name="password">The password of the user to act as.</param>
		private void ImpersonateValidUser(
			string userName, 
			string domain, 
			string password )
		{
			WindowsIdentity tempWindowsIdentity = null;
			IntPtr token = IntPtr.Zero;
			IntPtr tokenDuplicate = IntPtr.Zero;

			try
			{
				if ( RevertToSelf() )
				{
                    /*Enum LOGON32_LOGON
    INTERACTIVE = 2
    NETWORK = 3
    BATCH = 4
    SERVICE = 5
    UNLOCK = 7
    NETWORK_CLEARTEXT = 8
    NEW_CREDENTIALS = 9
End Enum
Enum LOGON32_PROVIDER
    [DEFAULT] = 0
    WINNT35 = 1
    WINNT40 = 2
    WINNT50 = 3
End Enum
Enum SECURITY_LEVEL
    Anonymous = 0
    Identification = 1
    Impersonation = 2
    Delegation = 3
End Enum
*/
					if ( LogonUser(
						userName, 
						domain, 
						password,
                        LOGON32_LOGON_SERVICE,
                        //LOGON32_LOGON_INTERACTIVE,
						3, 
						ref token ) != 0 )
					{
                        //tempWindowsIdentity = new WindowsIdentity(token);
                        //    impersonationContext = tempWindowsIdentity.Impersonate();
                            if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                            {
                                tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                                //tempWindowsIdentity = new WindowsIdentity(tokenDuplicate, "WindowsAuthentication", WindowsAccountType.System);
                                impersonationContext = tempWindowsIdentity.Impersonate();

                                byte[] NtAuthority = new byte[6];
      IntPtr AdministratorsGroup;
      
      NtAuthority[5] = 5; // SECURITY_NT_AUTHORITY
      
      
      int ret = AllocateAndInitializeSid(NtAuthority, 2, SECURITY_BUILTIN_DOMAIN_RID, DOMAIN_ALIAS_RID_ADMINS, 0, 0, 0, 0, 0, 0, out AdministratorsGroup);

      if (ret != 0)
      {
          if (CheckTokenMembership(tokenDuplicate, AdministratorsGroup, ref ret) == 0)
          {
              ret = 0;
          }
      }
      Console.WriteLine("ret=="+ret);
                            }
                            else
                            {
                                throw new Win32Exception(Marshal.GetLastWin32Error());
                            }
					}
					else
					{
						throw new Win32Exception( Marshal.GetLastWin32Error() );
					}
				}
				else
				{
					throw new Win32Exception( Marshal.GetLastWin32Error() );
				}
			}
			finally
			{
				if ( token!= IntPtr.Zero )
				{
					CloseHandle( token );
				}
				if ( tokenDuplicate!=IntPtr.Zero )
				{
					CloseHandle( tokenDuplicate );
				}
			}
		}

		/// <summary>
		/// Reverts the impersonation.
		/// </summary>
		private void UndoImpersonation()
		{
			if ( impersonationContext!=null )
			{
				impersonationContext.Undo();
			}	
		}

		private WindowsImpersonationContext impersonationContext = null;

		// ------------------------------------------------------------------
		#endregion
	}

	/////////////////////////////////////////////////////////////////////////
}