using System.Security.Cryptography;
using System.Text;
using Xunit;

public class ProgramTest
{
    private static string ComputeHash(string input, string? salt)
    {
        string combined = input + (salt ?? string.Empty);
        byte[] bytes = Encoding.UTF8.GetBytes(combined);
        byte[] hash = MD5.HashData(bytes);
        StringBuilder sb = new();
        foreach (byte b in hash)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }

    [Fact]
    public void ComputeHash_WithInputOnly_ReturnsValidMD5Hash()
    {
        string result = ComputeHash("test", null);
        Assert.NotNull(result);
        Assert.Equal(32, result.Length);
    }

    [Fact]
    public void ComputeHash_WithInputAndSalt_ReturnsValidMD5Hash()
    {
        string result = ComputeHash("test", "salt");
        Assert.NotNull(result);
        Assert.Equal(32, result.Length);
    }

    [Fact]
    public void ComputeHash_SameInputProducesSameHash()
    {
        string hash1 = ComputeHash("test", null);
        string hash2 = ComputeHash("test", null);
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void ComputeHash_DifferentInputProducesDifferentHash()
    {
        string hash1 = ComputeHash("test1", null);
        string hash2 = ComputeHash("test2", null);
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void ComputeHash_WithAndWithoutSaltProducesDifferentHash()
    {
        string hash1 = ComputeHash("test", null);
        string hash2 = ComputeHash("test", "salt");
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void ComputeHash_EmptyStringInput_ReturnsValidHash()
    {
        string result = ComputeHash(string.Empty, null);
        Assert.Equal(32, result.Length);
    }
}