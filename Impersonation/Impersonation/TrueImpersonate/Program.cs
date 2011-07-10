using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace TrueImpersonate
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Impersonator imp = new Impersonator("andy2", ".", "andy2"))
            {
                bool fInAdminGroup = false;
                int cbSize = 0;
                SafeTokenHandle hToken = null;
                SafeTokenHandle hTokenToCheck = null;
                IntPtr pElevationType = IntPtr.Zero;
                IntPtr pLinkedToken = IntPtr.Zero;
                WindowsIdentity wi = WindowsIdentity.GetCurrent();
                IntPtr currentToken = wi.Token;
                hToken = new SafeTokenHandle(currentToken);
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    // Running Windows Vista or later (major version >= 6). 
                    // Determine token type: limited, elevated, or default. 

                    // Allocate a buffer for the elevation type information.
                    cbSize = sizeof(TOKEN_ELEVATION_TYPE);
                    pElevationType = Marshal.AllocHGlobal(cbSize);
                    if (pElevationType == IntPtr.Zero)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }

                    // Retrieve token elevation type information.
                    if (!NativeMethod.GetTokenInformation(hToken,
                        TOKEN_INFORMATION_CLASS.TokenElevationType, pElevationType,
                        cbSize, out cbSize))
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }

                    // Marshal the TOKEN_ELEVATION_TYPE enum from native to .NET.
                    TOKEN_ELEVATION_TYPE elevType = (TOKEN_ELEVATION_TYPE)
                        Marshal.ReadInt32(pElevationType);

                    // If limited, get the linked elevated token for further check.
                    if (elevType == TOKEN_ELEVATION_TYPE.TokenElevationTypeLimited)
                    {
                        // Allocate a buffer for the linked token.
                        cbSize = IntPtr.Size;
                        pLinkedToken = Marshal.AllocHGlobal(cbSize);
                        if (pLinkedToken == IntPtr.Zero)
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        // Get the linked token.
                        if (!NativeMethod.GetTokenInformation(hToken,
                            TOKEN_INFORMATION_CLASS.TokenLinkedToken, pLinkedToken,
                            cbSize, out cbSize))
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        // Marshal the linked token value from native to .NET.
                        IntPtr hLinkedToken = Marshal.ReadIntPtr(pLinkedToken);
                        hTokenToCheck = new SafeTokenHandle(hLinkedToken);
                    }
                }
                if (hTokenToCheck == null)
                {
                    if (!NativeMethod.DuplicateToken(hToken,
                        SECURITY_IMPERSONATION_LEVEL.SecurityIdentification,
                        out hTokenToCheck))
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }

                // Check if the token to be checked contains admin SID.
                WindowsIdentity id = new WindowsIdentity(hTokenToCheck.DangerousGetHandle());
                WindowsPrincipal principal = new WindowsPrincipal(id);
                fInAdminGroup = principal.IsInRole(WindowsBuiltInRole.Administrator);
                Console.WriteLine(fInAdminGroup);
               // Console.WriteLine(AuthenticationHelper.IsUserInAdminGroup());
            }
            using (Impersonator imp = new Impersonator("onlyuser", ".", "onlyuser"))
            {
                bool fInAdminGroup = false;
                int cbSize = 0;
                SafeTokenHandle hToken = null;
                SafeTokenHandle hTokenToCheck = null;
                IntPtr pElevationType = IntPtr.Zero;
                IntPtr pLinkedToken = IntPtr.Zero;
                WindowsIdentity wi = WindowsIdentity.GetCurrent();
                IntPtr currentToken = wi.Token;
                hToken = new SafeTokenHandle(currentToken);
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    // Running Windows Vista or later (major version >= 6). 
                    // Determine token type: limited, elevated, or default. 

                    // Allocate a buffer for the elevation type information.
                    cbSize = sizeof(TOKEN_ELEVATION_TYPE);
                    pElevationType = Marshal.AllocHGlobal(cbSize);
                    if (pElevationType == IntPtr.Zero)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }

                    // Retrieve token elevation type information.
                    if (!NativeMethod.GetTokenInformation(hToken,
                        TOKEN_INFORMATION_CLASS.TokenElevationType, pElevationType,
                        cbSize, out cbSize))
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }

                    // Marshal the TOKEN_ELEVATION_TYPE enum from native to .NET.
                    TOKEN_ELEVATION_TYPE elevType = (TOKEN_ELEVATION_TYPE)
                        Marshal.ReadInt32(pElevationType);

                    // If limited, get the linked elevated token for further check.
                    if (elevType == TOKEN_ELEVATION_TYPE.TokenElevationTypeLimited)
                    {
                        // Allocate a buffer for the linked token.
                        cbSize = IntPtr.Size;
                        pLinkedToken = Marshal.AllocHGlobal(cbSize);
                        if (pLinkedToken == IntPtr.Zero)
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        // Get the linked token.
                        if (!NativeMethod.GetTokenInformation(hToken,
                            TOKEN_INFORMATION_CLASS.TokenLinkedToken, pLinkedToken,
                            cbSize, out cbSize))
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        // Marshal the linked token value from native to .NET.
                        IntPtr hLinkedToken = Marshal.ReadIntPtr(pLinkedToken);
                        hTokenToCheck = new SafeTokenHandle(hLinkedToken);
                    }
                }
                if (hTokenToCheck == null)
                {
                    if (!NativeMethod.DuplicateToken(hToken,
                        SECURITY_IMPERSONATION_LEVEL.SecurityIdentification,
                        out hTokenToCheck))
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }

                // Check if the token to be checked contains admin SID.
                WindowsIdentity id = new WindowsIdentity(hTokenToCheck.DangerousGetHandle());
                WindowsPrincipal principal = new WindowsPrincipal(id);
                fInAdminGroup = principal.IsInRole(WindowsBuiltInRole.Administrator);
                Console.WriteLine(fInAdminGroup);
                // Console.WriteLine(AuthenticationHelper.IsUserInAdminGroup());
            }
            //using (Impersonator imp = new Impersonator("andy2", ".", "andy2"))
            //{
            //    Console.WriteLine(AuthenticationHelper.IsUserInAdminGroup());
            //}
            //using (Impersonator imp2 = new Impersonator("andy", ".", "andy"))
            //{
            //    Console.WriteLine(AuthenticationHelper.IsUserInAdminGroup());
            //}
        }
    }
}
