using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Archnowledge.Pcces.PccesMain.ShellLib;

public class ShellApi
{
	public delegate int BrowseCallbackProc(IntPtr hwnd, uint uMsg, int lParam, int lpData);

	public struct BROWSEINFO
	{
		public IntPtr hwndOwner;

		public IntPtr pidlRoot;

		[MarshalAs(UnmanagedType.LPStr)]
		public string pszDisplayName;

		[MarshalAs(UnmanagedType.LPStr)]
		public string lpszTitle;

		public uint ulFlags;

		[MarshalAs(UnmanagedType.FunctionPtr)]
		public BrowseCallbackProc lpfn;

		public int lParam;

		public int iImage;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct STRRET
	{
		[FieldOffset(0)]
		public uint uType;

		[FieldOffset(4)]
		public IntPtr pOleStr;

		[FieldOffset(4)]
		public IntPtr pStr;

		[FieldOffset(4)]
		public uint uOffset;

		[FieldOffset(4)]
		public IntPtr cStr;
	}

	public struct SHELLEXECUTEINFO
	{
		public uint cbSize;

		public uint fMask;

		public IntPtr hwnd;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpVerb;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpFile;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpParameters;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpDirectory;

		public int nShow;

		public IntPtr hInstApp;

		public IntPtr lpIDList;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpClass;

		public IntPtr hkeyClass;

		public uint dwHotKey;

		public IntPtr hIconMonitor;

		public IntPtr hProcess;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct SHFILEOPSTRUCT
	{
		public IntPtr hwnd;

		public uint wFunc;

		public IntPtr pFrom;

		public IntPtr pTo;

		public ushort fFlags;

		public int fAnyOperationsAborted;

		public IntPtr hNameMappings;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszProgressTitle;
	}

	public enum CSIDL
	{
		CSIDL_FLAG_CREATE = 32768,
		CSIDL_ADMINTOOLS = 48,
		CSIDL_ALTSTARTUP = 29,
		CSIDL_APPDATA = 26,
		CSIDL_BITBUCKET = 10,
		CSIDL_CDBURN_AREA = 59,
		CSIDL_COMMON_ADMINTOOLS = 47,
		CSIDL_COMMON_ALTSTARTUP = 30,
		CSIDL_COMMON_APPDATA = 35,
		CSIDL_COMMON_DESKTOPDIRECTORY = 25,
		CSIDL_COMMON_DOCUMENTS = 46,
		CSIDL_COMMON_FAVORITES = 31,
		CSIDL_COMMON_MUSIC = 53,
		CSIDL_COMMON_PICTURES = 54,
		CSIDL_COMMON_PROGRAMS = 23,
		CSIDL_COMMON_STARTMENU = 22,
		CSIDL_COMMON_STARTUP = 24,
		CSIDL_COMMON_TEMPLATES = 45,
		CSIDL_COMMON_VIDEO = 55,
		CSIDL_CONTROLS = 3,
		CSIDL_COOKIES = 33,
		CSIDL_DESKTOP = 0,
		CSIDL_DESKTOPDIRECTORY = 16,
		CSIDL_DRIVES = 17,
		CSIDL_FAVORITES = 6,
		CSIDL_FONTS = 20,
		CSIDL_HISTORY = 34,
		CSIDL_INTERNET = 1,
		CSIDL_INTERNET_CACHE = 32,
		CSIDL_LOCAL_APPDATA = 28,
		CSIDL_MYDOCUMENTS = 12,
		CSIDL_MYMUSIC = 13,
		CSIDL_MYPICTURES = 39,
		CSIDL_MYVIDEO = 14,
		CSIDL_NETHOOD = 19,
		CSIDL_NETWORK = 18,
		CSIDL_PERSONAL = 5,
		CSIDL_PRINTERS = 4,
		CSIDL_PRINTHOOD = 27,
		CSIDL_PROFILE = 40,
		CSIDL_PROFILES = 62,
		CSIDL_PROGRAM_FILES = 38,
		CSIDL_PROGRAM_FILES_COMMON = 43,
		CSIDL_PROGRAMS = 2,
		CSIDL_RECENT = 8,
		CSIDL_SENDTO = 9,
		CSIDL_STARTMENU = 11,
		CSIDL_STARTUP = 7,
		CSIDL_SYSTEM = 37,
		CSIDL_TEMPLATES = 21,
		CSIDL_WINDOWS = 36
	}

	public enum SHGFP_TYPE
	{
		SHGFP_TYPE_CURRENT,
		SHGFP_TYPE_DEFAULT
	}

	public enum SFGAO : uint
	{
		SFGAO_CANCOPY = 1u,
		SFGAO_CANMOVE = 2u,
		SFGAO_CANLINK = 4u,
		SFGAO_STORAGE = 8u,
		SFGAO_CANRENAME = 16u,
		SFGAO_CANDELETE = 32u,
		SFGAO_HASPROPSHEET = 64u,
		SFGAO_DROPTARGET = 256u,
		SFGAO_CAPABILITYMASK = 375u,
		SFGAO_ENCRYPTED = 8192u,
		SFGAO_ISSLOW = 16384u,
		SFGAO_GHOSTED = 32768u,
		SFGAO_LINK = 65536u,
		SFGAO_SHARE = 131072u,
		SFGAO_READONLY = 262144u,
		SFGAO_HIDDEN = 524288u,
		SFGAO_DISPLAYATTRMASK = 1032192u,
		SFGAO_FILESYSANCESTOR = 268435456u,
		SFGAO_FOLDER = 536870912u,
		SFGAO_FILESYSTEM = 1073741824u,
		SFGAO_HASSUBFOLDER = 2147483648u,
		SFGAO_CONTENTSMASK = 2147483648u,
		SFGAO_VALIDATE = 16777216u,
		SFGAO_REMOVABLE = 33554432u,
		SFGAO_COMPRESSED = 67108864u,
		SFGAO_BROWSABLE = 134217728u,
		SFGAO_NONENUMERATED = 1048576u,
		SFGAO_NEWCONTENT = 2097152u,
		SFGAO_CANMONIKER = 4194304u,
		SFGAO_HASSTORAGE = 4194304u,
		SFGAO_STREAM = 4194304u,
		SFGAO_STORAGEANCESTOR = 8388608u,
		SFGAO_STORAGECAPMASK = 1891958792u
	}

	public enum SHCONTF
	{
		SHCONTF_FOLDERS = 0x20,
		SHCONTF_NONFOLDERS = 0x40,
		SHCONTF_INCLUDEHIDDEN = 0x80,
		SHCONTF_INIT_ON_FIRST_NEXT = 0x100,
		SHCONTF_NETPRINTERSRCH = 0x200,
		SHCONTF_SHAREABLE = 0x400,
		SHCONTF_STORAGE = 0x800
	}

	public enum SHCIDS : uint
	{
		SHCIDS_ALLFIELDS = 2147483648u,
		SHCIDS_CANONICALONLY = 268435456u,
		SHCIDS_BITMASK = 4294901760u,
		SHCIDS_COLUMNMASK = 65535u
	}

	public enum SHGNO
	{
		SHGDN_NORMAL = 0,
		SHGDN_INFOLDER = 1,
		SHGDN_FOREDITING = 0x1000,
		SHGDN_FORADDRESSBAR = 0x4000,
		SHGDN_FORPARSING = 0x8000
	}

	public enum STRRET_TYPE
	{
		STRRET_WSTR,
		STRRET_OFFSET,
		STRRET_CSTR
	}

	public enum PrinterActions
	{
		PRINTACTION_OPEN,
		PRINTACTION_PROPERTIES,
		PRINTACTION_NETINSTALL,
		PRINTACTION_NETINSTALLLINK,
		PRINTACTION_TESTPAGE,
		PRINTACTION_OPENNETPRN,
		PRINTACTION_DOCUMENTDEFAULTS,
		PRINTACTION_SERVERPROPERTIES
	}

	[DllImport("shell32.dll")]
	public static extern int SHGetMalloc(out IntPtr hObject);

	[DllImport("shell32.dll")]
	public static extern int SHGetFolderLocation(IntPtr hwndOwner, int nFolder, IntPtr hToken, uint dwReserved, out IntPtr ppidl);

	[DllImport("shell32.dll")]
	public static extern int SHGetPathFromIDList(IntPtr pidl, StringBuilder pszPath);

	[DllImport("shell32.dll")]
	public static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, uint dwFlags, StringBuilder pszPath);

	[DllImport("shell32.dll")]
	public static extern int SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string pszName, IntPtr pbc, out IntPtr ppidl, uint sfgaoIn, out uint psfgaoOut);

	[DllImport("shell32.dll")]
	public static extern int SHGetDesktopFolder(out IntPtr ppshf);

	[DllImport("shell32.dll")]
	public static extern int SHBindToParent(IntPtr pidl, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr ppv, ref IntPtr ppidlLast);

	[DllImport("shlwapi.dll")]
	public static extern int StrRetToBSTR(ref STRRET pstr, IntPtr pidl, [MarshalAs(UnmanagedType.BStr)] out string pbstr);

	[DllImport("shlwapi.dll")]
	public static extern int StrRetToBuf(ref STRRET pstr, IntPtr pidl, StringBuilder pszBuf, uint cchBuf);

	[DllImport("shell32.dll")]
	public static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lbpi);

	[DllImport("shell32.dll")]
	public static extern IntPtr ShellExecute(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStr)] string lpOperation, [MarshalAs(UnmanagedType.LPStr)] string lpFile, [MarshalAs(UnmanagedType.LPStr)] string lpParameters, [MarshalAs(UnmanagedType.LPStr)] string lpDirectory, int nShowCmd);

	[DllImport("shell32.dll")]
	public static extern int ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

	[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
	public static extern int SHFileOperation(ref SHFILEOPSTRUCT lpFileOp);

	[DllImport("shell32.dll")]
	public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

	[DllImport("shell32.dll")]
	public static extern void SHAddToRecentDocs(uint uFlags, IntPtr pv);

	[DllImport("shell32.dll")]
	public static extern void SHAddToRecentDocs(uint uFlags, [MarshalAs(UnmanagedType.LPWStr)] string pv);

	[DllImport("shell32.dll")]
	public static extern int SHInvokePrinterCommand(IntPtr hwnd, uint uAction, [MarshalAs(UnmanagedType.LPWStr)] string lpBuf1, [MarshalAs(UnmanagedType.LPWStr)] string lpBuf2, int fModal);

	public static short GetHResultCode(int hr)
	{
		hr &= 0xFFFF;
		return (short)hr;
	}
}
