namespace Base.Domain;

public class LangStr : Dictionary<string, string>
{
    // TODO: use app conf param
    private const string DefaultCulture = "en";

    public new string this[string key]
    {
        get => base[key];
        set => base[key] = value;
    }

    public LangStr()
    {
    }

    public LangStr(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
    {
    }

    public LangStr(string value, string culture)
    {
        if (culture.Length < 1) throw new ApplicationException("Culture is required!");

        var neutralCulture = culture.Split('-')[0];
        this[neutralCulture] = value;
    }

    public string? Translate(string? culture = null)
    {
        if (Count == 0) return null;
        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;

        if (ContainsKey(culture))
        {
            return this[culture];
        }

        var neutralCulture = culture.Split('-')[0];
        if (ContainsKey(neutralCulture))
        {
            return this[neutralCulture];
        }

        return ContainsKey(DefaultCulture) ? this[DefaultCulture] : null;
    }

    public void SetTranslation(string value, string? culture = null)
    {
        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;
        var neutralCulture = culture.Split('-')[0];
        this[neutralCulture] = value;
    }

    public override string ToString()
    {
        return Translate() ?? "????";
    }

    // string xxx = new LangStr("foo","et-EE"); xxx == "foo";
    public static implicit operator string(LangStr? langStr) => langStr?.ToString() ?? "null";

    // LangStr xxx = "foobar";
    public static implicit operator LangStr(string value) => new(value);
}