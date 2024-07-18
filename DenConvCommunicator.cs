using System;
using System.Runtime.InteropServices;

class DenConvCommunicator
{
	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern IntPtr VirtualAlloc(IntPtr lpAddress, IntPtr dwSize, int flAllocationType, int flProtect);

	private const int MEM_COMMIT = 0x00001000;
	private const int MEM_RESERVE = 0x00002000;
	private const int PAGE_READWRITE = 0x04;

	private static readonly Byte[] BuildPutternId = new Byte[] { 0xaf, 0x11, 0xb4, 0x40 };
	private static readonly IntPtr BuildPutternAddress = new IntPtr(0x400110);

	private static readonly IntPtr PowerPtr = new IntPtr(0x4eda72);
	private static readonly IntPtr BrakePtr = new IntPtr(0x4ed2a0);
	private static readonly IntPtr ClosePtr = new IntPtr(0x534eb0);
	private static readonly IntPtr PressurePtr = new IntPtr(0x53467c);
	private static readonly IntPtr DistancePtr = new IntPtr(0x534680);
	private static readonly IntPtr SpeedPtr = new IntPtr(0x53463c);
	private static readonly IntPtr ATCPtr = new IntPtr(0x5345c4);
	private static readonly IntPtr ATCNoticePtr = new IntPtr(0x5345c8);
	private static readonly IntPtr ATCActivePtr = new IntPtr(0x535268);
	private static readonly IntPtr MovePtr = new IntPtr(0x534f0c);
	private static readonly IntPtr ShockPtr = new IntPtr(0x4ed130);

	private static IntPtr mem = IntPtr.Zero, mem2 = IntPtr.Zero, mem3 = IntPtr.Zero;

	private static Byte[] GetValueArray(uint value)
	{
		Byte[] array = new Byte[4];
		array[0] = (byte)value;
		array[1] = (byte)(value >> 8);
		array[2] = (byte)(value >> 16);
		array[3] = (byte)(value >> 24);
		return array;
	}

	private static int Round(double value)
	{
		return (int)Math.Round(value, MidpointRounding.AwayFromZero);
	}

	public static bool Initialize()
	{
		if (IntPtr.Zero.Equals(mem))
		{
			mem = VirtualAlloc(new IntPtr(0x400000), new IntPtr(0x1000), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
		}
		if (IntPtr.Zero.Equals(mem2))
		{
			mem2 = VirtualAlloc(new IntPtr(0x4ed000), new IntPtr(0x1000), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
		}
		if (IntPtr.Zero.Equals(mem3))
		{
			mem3 = VirtualAlloc(new IntPtr(0x534000), new IntPtr(0x2000), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
		}
		if (IntPtr.Zero.Equals(mem) || IntPtr.Zero.Equals(mem2) || IntPtr.Zero.Equals(mem3))
		{
			return false;
		}
		Marshal.Copy(BuildPutternId, 0, BuildPutternAddress, 4);
		Byte[] zero = GetValueArray(0);
		Marshal.Copy(zero, 0, PowerPtr, 1);
		Marshal.Copy(zero, 0, BrakePtr, 1);
		Marshal.Copy(zero, 0, MovePtr, 1);
		Marshal.Copy(zero, 0, ShockPtr, 4);
		SetDoorClosed(false);
		SetPressure(0);
		SetDistance(0);
		SetSpeed(0);
		SetATC(-1);
		SetATCActive(false);
		return true;
	}

	public struct PowerAndBrake
	{
		public readonly byte Power, Brake;

		public PowerAndBrake(byte power, byte brake)
		{
			Power = power;
			Brake = brake;
		}
	}

	public static PowerAndBrake GetPowerAndBrake()
	{
		Byte[] data = new Byte[2];
		Marshal.Copy(PowerPtr, data, 0, 1);
		Marshal.Copy(BrakePtr, data, 1, 1);
		return new PowerAndBrake(data[0], data[1]);
	}

	public static void SetDoorClosed(bool closed)
	{
		Byte[] data = GetValueArray((uint)(closed ? 0x32 : 0));
		Marshal.Copy(data, 0, ClosePtr, 4);
	}

	public static void SetPressure(float pressure)
	{
		Byte[] data = GetValueArray(pressure < 0 ? 0 : (uint)Round(pressure * 41.07242339832869));
		Marshal.Copy(data, 0, PressurePtr, 4);
	}

	public static void SetDistance(float distance)
	{
		uint distanceToSend;
		if (distance > 0.125) distanceToSend = 511;
		else if (distance < -0.125) distanceToSend = 512;
		else
		{
			distanceToSend = (uint)Round((distance + 0.25) * 4096);
			if (distanceToSend < 512) distanceToSend = 512;
			if (distanceToSend > 511 + 1024) distanceToSend = 511 + 1024;
			distanceToSend %= 1024;
		}
		Byte[] data = GetValueArray(distanceToSend);
		Marshal.Copy(data, 0, DistancePtr, 4);
	}

	public static void SetSpeed(float speed)
	{
		Byte[] data = GetValueArray((uint)Round(Math.Abs(speed) * 4096));
		Marshal.Copy(data, 0, SpeedPtr, 4);
	}

	public static void SetATC(float atc, float atcNotice)
	{
		Byte[] atcData = GetValueArray(atc < 0 ? 0xffff : (uint)atc);
		Byte[] atcNoticeData = GetValueArray(atcNotice < 0 ? 0xffff : (uint)atcNotice);
		Marshal.Copy(atcData, 0, ATCPtr, 2);
		Marshal.Copy(atcNoticeData, 0, ATCNoticePtr, 2);
	}

	public static void SetATC(float atc)
	{
		SetATC(atc, -1);
	}

	public static void SetATCActive(bool active)
	{
		Byte[] data = new Byte[] { (byte)(active ? 2 : 0) };
		Marshal.Copy(data, 0, ATCActivePtr, 1);
	}
}
