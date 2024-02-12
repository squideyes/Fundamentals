// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Fundamentals;

public class HttpHelper
{
    private readonly List<string> segments = [];
    private readonly Dictionary<string, string?> queryParams = [];

    private readonly HttpClient client;
    private readonly Uri baseUri;

    private JsonSerializerOptions? jsonSerializerOptions = null;

    public HttpHelper(HttpClient client, string uriString)
        : this(client, new Uri(uriString))
    {
    }

    public HttpHelper(HttpClient client, Uri baseUri)
    {
        baseUri.IsAbsoluteUri.MustBe().EqualTo(true);

        this.client = client;

        this.baseUri = new Uri(baseUri.GetLeftPart(UriPartial.Authority));

        segments.AddRange(baseUri.LocalPath.Split('/')
            .Where(s => !string.IsNullOrWhiteSpace(s)));
    }

    public HttpHelper AppendPathSegment(string segment)
    {
        segment.MayNotBe().NullOrWhitespace();

        Uri.IsWellFormedUriString(
            segment, UriKind.Relative).MustBe().EqualTo(true);

        segments.Add(segment);

        return this;
    }

    public HttpHelper SetQueryParam(string token, string? value = null)
    {
        if (token.IsEmptyOrWhitespace())
            throw new ArgumentOutOfRangeException(nameof(token));

        if (!Uri.IsWellFormedUriString(value, UriKind.Relative))
            throw new ArgumentOutOfRangeException(nameof(value));

        if (value != null)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            if (!Uri.IsWellFormedUriString(value, UriKind.Relative))
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        queryParams.Add(token, value);

        return this;
    }

    public Uri GetUri()
    {
        var sb = new StringBuilder();

        sb.Append(baseUri.AbsoluteUri);
        sb.Append(string.Join("/", segments));

        int count = 0;

        foreach (var key in queryParams.Keys)
        {
            sb.Append(count++ == 0 ? '?' : '&');
            sb.Append(key);

            if (queryParams[key] != null)
            {
                sb.Append('=');
                sb.Append(queryParams[key]);
            }
        }

        return new Uri(sb.ToString());
    }

    public async Task<string> GetStringAsync() => await client.GetStringAsync(GetUri());

    public async Task<T?> GetJsonAsync<T>(JsonSerializerOptions? options = null)
        where T : class, new()
    {
        jsonSerializerOptions ??= GetJsonSerializerOptions();

        var json = await GetStringAsync();

        return JsonSerializer.Deserialize<T?>(json, options ?? jsonSerializerOptions);
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        options.Converters.Add(new JsonStringEnumConverter());

        return options;
    }
}