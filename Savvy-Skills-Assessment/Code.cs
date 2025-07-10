namespace Savvy_Skills_Assessment;

public class Code
{
    public string Secret { get; }

    public Code(string? code = null)
    {
        Secret = code ?? GenrateCode();
    }

    private string GenrateCode()
    {
        var nums = "012345678";
        var rand = new Random();
        return new string(nums.OrderBy(_ => rand.Next()).Take(4).ToArray());
    }
}