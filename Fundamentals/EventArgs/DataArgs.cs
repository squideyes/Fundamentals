// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class DataArgs<T> : EventArgs
{
    public DataArgs(T data)
    {
        Data = data;
    }

    public T Data { get; }
}