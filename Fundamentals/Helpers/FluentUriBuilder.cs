// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;

namespace SquidEyes.Fundamentals;

public class FluentUriBuilder
{
    private readonly Dictionary<string, object> keyValues = new();

    private readonly Uri baseUri;
    private readonly string action;

    public FluentUriBuilder(string baseUrl, string action)
    {
        baseUri = new Uri(baseUrl);
        this.action = action;
    }

    public FluentUriBuilder SetQueryParam(string key, object value)
    {
        keyValues.Add(key, value);

        return this;
    }

    public Uri GetUri()
    {
        var sb = new StringBuilder();

        sb.Append("?action=");
        sb.Append(action);

        foreach(var kv in keyValues)
        {
            sb.Append('&');
            sb.Append(kv.Key);
            sb.Append('=');
            sb.Append(kv.Value);
        }

        return new Uri(baseUri, sb.ToString());
    }
}