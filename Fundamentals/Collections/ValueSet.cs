using System.Numerics;
using System.Text.Json.Nodes;

namespace SquidEyes.Fundamentals;

public class ValueSet
{
    private readonly Type type;
    private readonly List<IConvertible> values;

    private ValueSet(Type type, List<IConvertible> values)
    {
        this.type = type;
        this.values = values;
    }

    public List<T> GetValues<T>()
    {
        typeof(T).Must().Be(v => v == type);

        return values.Cast<T>().ToList();
    }

    public static ValueSet From(int min, int max, int step) =>
        From<int>(min, max, step);

    public static ValueSet From(double min, double max, double step) =>
        From<double>(min, max, step);

    private static ValueSet From<T>(T min, T max, T step)
        where T : INumber<T>, IConvertible
    {
        if (max <= min)
            throw new ArgumentOutOfRangeException(nameof(max));

        if (step <= T.Zero || step > (max - min))
            throw new ArgumentOutOfRangeException(nameof(step));

        var values = new List<IConvertible>();

        for (T value = min; value < max; value += step)
            values.Add(value);

        return new ValueSet(typeof(T), values);
    }

    public static ValueSet From<T>(params T[] values)
        where T : IConvertible
    {
        return From(values.ToList());
    }

    public static ValueSet From<T>(IEnumerable<T> values)
        where T : IConvertible
    {
        if (values == null)
            throw new ArgumentNullException(nameof(values));

        if (!values.Any() || values.Any(v => v == null))
            throw new ArgumentOutOfRangeException(nameof(values));

        values.Must().Be(v => IsValueSetType(values));

        return new ValueSet(
            typeof(T), values.Cast<IConvertible>().ToList());
    }

    internal static (string Name, ValueSet ValueSet) From(JsonNode node)
    {
        var name = node!["name"]!.GetValue<string>();

        var type = Type.GetType(node!["type"]!.GetValue<string>());

        (T, T, T) GetMinMaxStep<T>() where T : struct, IConvertible
        {
            var min = node!["min"]!.GetValue<T>();
            var max = node!["max"]!.GetValue<T>();
            var step = node!["step"]!.GetValue<T>();

            return (min, max, step);
        }

        if (type!.IsEnum)
            return (name, new ValueSet(type, GetEnums(node, type)));

        var values = new List<IConvertible>();

        switch (type.FullName)
        {
            case "System.Bool":
                values.AddRange(new HashSet<IConvertible>(node!["values"]!
                    .AsArray().Select(v => (IConvertible)v!.GetValue<bool>())));
                return (name, new ValueSet(type, values));
            case "System.Int32":
                values = GetValues<int>(node);
                if (values.Count == 0)
                {
                    var (min, max, step) = GetMinMaxStep<int>();
                    for (var value = min; value <= max; value += step)
                        values.Add(value);
                }
                return (name, new ValueSet(type, values));
            case "System.Double":
                values = GetValues<double>(node);
                if (values.Count == 0)
                {
                    var (min, max, step) = GetMinMaxStep<double>();
                    for (var value = min; value <= max; value += step)
                        values.Add(value);
                }
                return (name, new ValueSet(type, values));
            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }

    private static bool IsValueSetType<T>(T value)
    {
        if (value!.GetType().GetGenericArguments()[0].IsEnum)
            return true;

        return value switch
        {
            IEnumerable<bool> _ => true,
            IEnumerable<int> _ => true,
            IEnumerable<double> _ => true,
            _ => false
        };
    }

    private static List<IConvertible> GetEnums(JsonNode node, Type type)
    {
        if (!type!.IsEnum)
            throw new ArgumentOutOfRangeException(nameof(type));

        var values = new List<IConvertible>();

        var n = node!["values"]!;

        if (n == null)
            return values;

        foreach (var e in n.AsArray().Select(v => v!.GetValue<string>()))
            values.Add((IConvertible)Enum.Parse(type!, e));

        return values;
    }

    private static List<IConvertible> GetValues<T>(JsonNode node)
        where T : struct, IConvertible
    {
        var values = new List<IConvertible>();

        var n = node!["values"]!;

        if (n == null)
            return values;

        foreach (var value in n.AsArray().Select(v => v!.GetValue<T>()))
            values.Add(value);

        return values;
    }

    private static List<IConvertible> GetStrings(JsonNode parent)
    {
        var node = parent!["values"]!;

        return node == null
            ? throw new ArgumentNullException(nameof(parent))
            : node.AsArray().Select(
            v => (IConvertible)v!.GetValue<string>()).ToList();
    }
}