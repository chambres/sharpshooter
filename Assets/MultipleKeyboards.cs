using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class RawInputExample : MonoBehaviour
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rawinputdevicelist
    {
        public IntPtr hDevice;
        public uint dwType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rawinputdevice
    {
        public uint dwSize;
        public uint dwType;
        public IntPtr hDevice;
        public uint wParam;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rawinputheader
    {
        public uint dwType;
        public uint dwSize;
        public IntPtr hDevice;
        public IntPtr wParam;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rawinputkeyboard
    {
        public Rawinputheader header;
        public uint dwMakeCode;
        public uint dwFlags;
        public uint dwReserved;
        public uint dwExtraInfo;
    }

    private const uint RIDEV_INPUTSINK = 0x00000100;
    private const uint RIDEV_DEVNOTIFY = 0x00002000;
    private const uint RIDEV_REMOVE = 0x00000001;
    private const uint WM_INPUT = 0x00FF;
    private const uint WM_INPUT_DEVICE_CHANGE = 0x00FE;

    private IntPtr[] keyboardHandles;
    private int numKeyboards;

    private void Start()
    {
        RegisterForRawInput();
    }

    private void OnDestroy()
    {
        UnregisterForRawInput();
    }

    private void RegisterForRawInput()
    {
        // Get list of raw input devices
        uint numDevices = 0;
        GetRawInputDeviceList(IntPtr.Zero, ref numDevices, (uint)Marshal.SizeOf(typeof(Rawinputdevicelist)));

        IntPtr deviceList = Marshal.AllocHGlobal((int)(numDevices * Marshal.SizeOf(typeof(Rawinputdevicelist))));
        GetRawInputDeviceList(deviceList, ref numDevices, (uint)Marshal.SizeOf(typeof(Rawinputdevicelist)));

        // Register for raw input events for all keyboards
        numKeyboards = 0;
        for (uint i = 0; i < numDevices; i++)
        {
            uint size = 0;
            GetRawInputDeviceInfo(deviceList + i * Marshal.SizeOf(typeof(Rawinputdevicelist)), 0x20000005 /*RIDI_DEVICEINFO*/, IntPtr.Zero, ref size);
            IntPtr deviceInfo = Marshal.AllocHGlobal((int)size);
            GetRawInputDeviceInfo(deviceList + i * Marshal.SizeOf(typeof(Rawinputdevicelist)), 0x20000005 /*RIDI_DEVICEINFO*/, deviceInfo, ref size);

            uint type = (uint)Marshal.ReadInt32(deviceInfo, 4 /*offsetof(RID_DEVICE_INFO, dwType)*/);
            if (type == 1 /*RIM_TYPEKEYBOARD*/)
            {
                IntPtr handle = Marshal.ReadIntPtr(deviceInfo, 8 /*offsetof(RID_DEVICE_INFO, hDevice)*/);
                RegisterRawInputDevices(handle);
                numKeyboards++;
            }

            Marshal.FreeHGlobal(deviceInfo);
        }

        Marshal.FreeHGlobal(deviceList);
    }

    private void UnregisterForRawInput()
    {
        for (int i = 0; i < numKeyboards; i++)
        {
            UnregisterRawInputDevices(keyboardHandles[i]);
        }
    }

    private void RegisterRawInputDevices(IntPtr handle)
    {
        // Register for input sink and device notifications
        RAWINPUTDEVICE[] devices = new RAWINPUTDEVICE[1];
    devices[0].usUsagePage = 0x01;
    devices[0].usUsage = 0x06;
    devices[0].dwFlags = RIDEV_INPUTSINK | RIDEV_DEVNOTIFY;
    devices[0].hwndTarget = IntPtr.Zero;
    devices[0].hDevice = handle;

    bool result = RegisterRawInputDevices(devices, (uint)devices.Length, (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICE)));
    if (!result)
    {
        Debug.LogError("Failed to register raw input devices");
    }
    else
    {
        Debug.Log("Registered raw input devices");
    }
}

private void UnregisterRawInputDevices(IntPtr handle)
{
    RAWINPUTDEVICE[] devices = new RAWINPUTDEVICE[1];
    devices[0].hDevice = handle;
    devices[0].dwFlags = RIDEV_REMOVE;

    bool result = RegisterRawInputDevices(devices, (uint)devices.Length, (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICE)));
    if (!result)
    {
        Debug.LogError("Failed to unregister raw input devices");
    }
    else
    {
        Debug.Log("Unregistered raw input devices");
    }
}

private void Update()
{
    // Process raw input messages
    while (PeekMessage(out MSG msg, IntPtr.Zero, WM_INPUT, WM_INPUT, 1 /*PM_REMOVE*/))
    {
        uint size = 0;
        GetRawInputData(msg.lParam, 0x10000003 /*RID_INPUT*/, IntPtr.Zero, ref size, (uint)Marshal.SizeOf(typeof(Rawinputheader)));

        IntPtr data = Marshal.AllocHGlobal((int)size);
        GetRawInputData(msg.lParam, 0x10000003 /*RID_INPUT*/, data, ref size, (uint)Marshal.SizeOf(typeof(Rawinputheader)));

        Rawinputheader header = (Rawinputheader)Marshal.PtrToStructure(data, typeof(Rawinputheader));
        if (header.dwType == 1 /*RIM_TYPEKEYBOARD*/)
        {
            Rawinputkeyboard keyboard = (Rawinputkeyboard)Marshal.PtrToStructure(data, typeof(Rawinputkeyboard));
            int keyboardIndex = Array.IndexOf(keyboardHandles, header.hDevice);
            Debug.Log("Keyboard " + keyboardIndex + " pressed key " + keyboard.dwMakeCode);
        }

        Marshal.FreeHGlobal(data);
    }

    // Check for device change notifications
    while (PeekMessage(out MSG msg, IntPtr.Zero, WM_INPUT_DEVICE_CHANGE, WM_INPUT_DEVICE_CHANGE, 1 /*PM_REMOVE*/))
    {
        Debug.Log("Device change notification received");
    }
}

[DllImport("User32.dll")]
private static extern uint GetRawInputDeviceList(IntPtr pRawInputDeviceList, ref uint uiNumDevices, uint cbSize);

[DllImport("User32.dll")]
private static extern uint GetRawInputDeviceInfo(IntPtr hDevice, uint uiCommand, IntPtr pData, ref uint pcbSize);

[DllImport("User32.dll")]
private static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

[DllImport("User32.dll")]
private static extern bool GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

[DllImport("User32.dll")]
private static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

[StructLayout(LayoutKind.Sequential)]
public struct RAWINPUTDEVICE
{
    public ushort usUsagePage;
    public ushort usUsage;
   
    public uint dwFlags;
    public IntPtr hwndTarget;
    public IntPtr hDevice;
}

[StructLayout(LayoutKind.Sequential)]
public struct Rawinputheader
{
    public uint dwType;
    public uint dwSize;
    public IntPtr hDevice;
    public IntPtr wParam;
}

[StructLayout(LayoutKind.Sequential)]
public struct Rawinputkeyboard
{
    public ushort MakeCode;
    public ushort Flags;
    public ushort Reserved;
    public ushort VKey;
    public uint   Message;
    public uint   ExtraInformation;
}

private IntPtr[] keyboardHandles;

private void Start()
{
    // Get list of raw input devices
    uint deviceCount = 0;
    GetRawInputDeviceList(IntPtr.Zero, ref deviceCount, (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICELIST)));

    IntPtr deviceListPtr = Marshal.AllocHGlobal((int)(deviceCount * Marshal.SizeOf(typeof(RAWINPUTDEVICELIST))));
    GetRawInputDeviceList(deviceListPtr, ref deviceCount, (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICELIST)));

    RAWINPUTDEVICELIST[] deviceList = new RAWINPUTDEVICELIST[deviceCount];
    for (int i = 0; i < deviceCount; i++)
    {
        deviceList[i] = (RAWINPUTDEVICELIST)Marshal.PtrToStructure(new IntPtr(deviceListPtr.ToInt64() + i * Marshal.SizeOf(typeof(RAWINPUTDEVICELIST)))), typeof(RAWINPUTDEVICELIST))));
    }

    // Register raw input devices
    List<IntPtr> keyboardHandlesList = new List<IntPtr>();
    for (int i = 0; i < deviceCount; i++)
    {
        if (deviceList[i].dwType == RIM_TYPEKEYBOARD)
        {
            RAWINPUTDEVICE device = new RAWINPUTDEVICE();
            device.usUsagePage = 0x01;
            device.usUsage = 0x06;
            device.dwFlags = RIDEV_INPUTSINK | RIDEV_DEVNOTIFY;
            device.hwndTarget = IntPtr.Zero;
            device.hDevice = deviceList[i].hDevice;

            bool result = RegisterRawInputDevices(new RAWINPUTDEVICE[] { device }, 1, (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICE)));
            if (result)
            {
                keyboardHandlesList.Add(deviceList[i].hDevice);
                Debug.Log("Registered raw input device: " + deviceList[i].hDevice);
            }
            else
            {
                Debug.LogWarning("Failed to register raw input device: " + deviceList[i].hDevice);
            }
        }
    }

    keyboardHandles = keyboardHandlesList.ToArray();

    Marshal.FreeHGlobal(deviceListPtr);
}

private void OnDisable()
{
    // Unregister raw input devices
    for (int i = 0; i < keyboardHandles.Length; i++)
    {
        UnregisterRawInputDevices(keyboardHandles[i]);
    }
}
}