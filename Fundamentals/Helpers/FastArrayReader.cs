// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class FastArrayReader
{
    private readonly byte[] bytes;
    private int index;

    public FastArrayReader(byte[] bytes)
    {
        this.bytes = bytes;
    }

    public byte ReadByte() => bytes[index++];

    public sbyte ReadSByte() => unchecked((sbyte)bytes[index++]);

    public ushort ReadUInt16()
    {
        var value = BitConverter.ToUInt16(bytes, index);

        index += 2;

        return value;
    }

    public short ReadInt16()
    {
        var value = BitConverter.ToInt16(bytes, index);

        index += 2;

        return value;
    }

    public int ReadInt32()
    {
        var value = BitConverter.ToInt32(bytes, index);

        index += 4;

        return value;
    }

    public float ReadSingle()
    {
        var value = BitConverter.ToSingle(bytes, index);

        index += 4;

        return value;
    }
}