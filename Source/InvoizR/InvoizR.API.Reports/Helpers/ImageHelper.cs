namespace InvoizR.API.Reports.Helpers;

public static class ImageHelper
{
    public static byte[] GetBytes(string path)
        => File.ReadAllBytes(path);
}
