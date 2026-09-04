using System.Runtime.InteropServices;
using System.Security;

namespace AlliED;

[SecurityCritical, SuppressUnmanagedCodeSecurity]
internal static class NativeMethods
{
    public const int WM_UPDATEUISTATE = 0x0128;

    [DllImport("user32.dll", EntryPoint = "SendMessage")]
    public static extern nint SendMessage(nint hWnd, int msg, nint wp, nint lp);

    [DllImport("user32.dll", EntryPoint = "GetSysColor")]
    public static extern uint GetSysColor(uint nIndex);
}
