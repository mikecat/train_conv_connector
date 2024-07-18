using System;
using System.Runtime.InteropServices;
using System.Text;

class BeaconWindow
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	private struct WNDCLASSEX
	{
		public int cbSize;
		public int style;
		public IntPtr lpfnWndProc;
		public int cbClsExtra;
		public int cbWndExtra;
		public IntPtr hInstance;
		public IntPtr hIcon;
		public IntPtr hCursor;
		public IntPtr hbrBackground;
		public string lpszMenuName;
		public string lpszClassName;
		public IntPtr hIconSm;
	}

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	private static extern IntPtr GetModuleHandle(
		IntPtr lpModuleName
	);

	[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	private static extern ushort RegisterClassEx(
		[In] ref WNDCLASSEX lpwcx
	);

	[DllImport("user32.dll")]
	private static extern IntPtr DefWindowProc(
		IntPtr hWnd,
		uint uMsg,
		UIntPtr wParam,
		IntPtr lParam
	);

	[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	private static extern IntPtr CreateWindowEx(
		uint dwExStyle,
		string lpClassName,
		[MarshalAs(UnmanagedType.LPStr)] string lpWindowName,
		uint dwStyle,
		int x,
		int y,
		int nWidth,
		int nHeight,
		IntPtr hWndParent,
		IntPtr hMenu,
		IntPtr hInstance,
		IntPtr lpParam
	);

	private delegate IntPtr WndProcType(
		IntPtr hWnd,
		uint uMsg,
		UIntPtr wParam,
		IntPtr lParam
	);

	private static WndProcType wndProc = new WndProcType(DefWindowProc);
	private static ushort wndClassAtom = 0;

	public static IntPtr CreateBeaconWindow(IntPtr hParentWnd)
	{
		string className = Encoding.Default.GetString(
			Encoding.GetEncoding("shift_jis").GetBytes("電車でＧＯ！プロフェッショナル仕様")
		);
		IntPtr hModule = GetModuleHandle(IntPtr.Zero);
		if (wndClassAtom == 0)
		{
			WNDCLASSEX wcx = new WNDCLASSEX()
			{
				cbSize = Marshal.SizeOf(typeof(WNDCLASSEX)),
				style = 0,
				lpfnWndProc = Marshal.GetFunctionPointerForDelegate(wndProc),
				cbClsExtra = 0,
				cbWndExtra = 0,
				hInstance = hModule,
				hIcon = IntPtr.Zero,
				hCursor = IntPtr.Zero,
				hbrBackground = IntPtr.Zero,
				lpszMenuName = null,
				lpszClassName = className,
				hIconSm = IntPtr.Zero,
			};
			wndClassAtom = RegisterClassEx(ref wcx);
		}
		if (wndClassAtom == 0) return IntPtr.Zero;
		return CreateWindowEx(0, className, "", 0, 0, 0, 0, 0, hParentWnd, IntPtr.Zero, hModule, IntPtr.Zero);
	}
}
