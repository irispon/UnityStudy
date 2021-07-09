

using System;
using System.Runtime.InteropServices;



public struct WPacket
{
    public WThreadData data;
    public Type type;

}


[StructLayout(LayoutKind.Explicit)]
public struct WThreadData
{
    [FieldOffset(0)]
    public Object _data;
    [FieldOffset(0)]
    public int _int;
    [FieldOffset(0)]
    public string _string;
    [FieldOffset(0)]
    public float _float;

}
