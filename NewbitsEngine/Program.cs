using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NewbitsEngine;

internal class Program
{
	private const int LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000;

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool SetDefaultDllDirectories(int directoryFlags);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	private static extern void AddDllDirectory(string lpPathName);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool SetDllDirectory(string lpPathName);

	public static void Main(string[] args)
	{
		if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			try
			{
				SetDefaultDllDirectories(LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
				AddDllDirectory(Path.Combine(
					AppDomain.CurrentDomain.BaseDirectory,
					Environment.Is64BitProcess ? "x64" : "x86"
				));
			}
			catch
			{
				SetDllDirectory(Path.Combine(
					AppDomain.CurrentDomain.BaseDirectory,
					Environment.Is64BitProcess ? "x64" : "x86"
				));
			}

		using GameCore game = new();
		game.Run();
	}
}
