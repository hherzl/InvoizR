using System.Diagnostics;

namespace InvoizR.SharedKernel;

[DebuggerDisplay("Environment={Environment}, Service={Service}, Type={Type}, Name={Name}, Value={Value}")]
public class ThirdPartyClientParameter
{
    public ThirdPartyClientParameter()
    {
    }

    public ThirdPartyClientParameter(string environtment, string service, string type, string name, string value)
    {
        Environment = environtment;
        Service = service;
        Type = type;
        Name = name;
        Value = value;
    }

    public ThirdPartyClientParameter(string environtment, string type, string name, string value)
    {
        Environment = environtment;
        Type = type;
        Name = name;
        Value = value;
    }

    public string Environment { get; set; }
    public string Service { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}
