namespace ApiSec.Core.Entities;

public class Token
{
    public Token(string value, int expireAt)
    {
        Value = value;
        ExpireAt = expireAt;
    }

    public string Value { get; private set; } = null!;
    public int ExpireAt { get; private set; }
}
