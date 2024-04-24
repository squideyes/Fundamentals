// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;

namespace SquidEyes.Fundamentals;

public class UrlBuilder
{
    private readonly List<string> segments = [];
    private readonly Dictionary<string, string?> queryParams = new();

    private readonly Uri baseUri;

    public UrlBuilder(string uriString)
        : this(new Uri(uriString))
    {
    }

    public UrlBuilder(Uri baseUri)
    {
        if (!baseUri.IsAbsoluteUri)
            throw new ArgumentOutOfRangeException(nameof(baseUri));

        this.baseUri = new Uri(baseUri.GetLeftPart(UriPartial.Authority));

        segments.AddRange(baseUri.LocalPath.Split('/')
            .Where(s => !string.IsNullOrWhiteSpace(s)));
    }

    public UrlBuilder AppendPathSegment(string segment)
    {
        if (string.IsNullOrWhiteSpace(segment))
            throw new ArgumentOutOfRangeException(nameof(segment));

        if (!Uri.IsWellFormedUriString(segment, UriKind.Relative))
            throw new ArgumentOutOfRangeException(nameof(segment));

        segments.Add(segment);

        return this;
    }

    public UrlBuilder SetQueryParam(string token, string? value = null)
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
}