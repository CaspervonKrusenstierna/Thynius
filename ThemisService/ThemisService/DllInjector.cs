﻿
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ThyniusService
{
    public enum DllInjectionResult
    {
        DllNotFound,
        GameProcessNotFound,
        InjectionFailed,
        Success
    }

    public class DllInjector
    {
        static readonly nint INTPTR_ZERO = 0;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern nint OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int CloseHandle(nint hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern nint GetProcAddress(nint hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern nint GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern nint VirtualAllocEx(nint hProcess, nint lpAddress, nint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WriteProcessMemory(nint hProcess, nint lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern nint CreateRemoteThread(nint hProcess, nint lpThreadAttribute, nint dwStackSize, nint lpStartAddress,
            nint lpParameter, uint dwCreationFlags, nint lpThreadId);

        static DllInjector _instance;

        public static DllInjector GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DllInjector();
                }
                return _instance;
            }
        }

        public DllInjector() { }

        public DllInjectionResult Inject(uint _procId, string sDllPath)
        {
            if (!File.Exists(sDllPath))
            {
                return DllInjectionResult.DllNotFound;
            }

            if (_procId == 0)
            {
                return DllInjectionResult.GameProcessNotFound;
            }

            if (!bInject(_procId, sDllPath))
            {
                return DllInjectionResult.InjectionFailed;
            }

            return DllInjectionResult.Success;
        }

        bool bInject(uint pToBeInjected, string sDllPath)
        {
            nint hndProc = OpenProcess(0x2 | 0x8 | 0x10 | 0x20 | 0x400, 1, pToBeInjected);

            if (hndProc == INTPTR_ZERO)
            {
                return false;
            }

            nint lpLLAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (lpLLAddress == INTPTR_ZERO)
            {
                return false;
            }

            nint lpAddress = VirtualAllocEx(hndProc, (nint)null, sDllPath.Length, 0x1000 | 0x2000, 0X40);

            if (lpAddress == INTPTR_ZERO)
            {
                return false;
            }

            byte[] bytes = Encoding.ASCII.GetBytes(sDllPath);

            if (WriteProcessMemory(hndProc, lpAddress, bytes, (uint)bytes.Length, 0) == 0)
            {
                return false;
            }

            if (CreateRemoteThread(hndProc, (nint)null, INTPTR_ZERO, lpLLAddress, lpAddress, 0, (nint)null) == INTPTR_ZERO)
            {
                return false;
            }

            CloseHandle(hndProc);

            return true;
        }
    }
}