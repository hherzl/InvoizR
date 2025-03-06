namespace InvoizR.Application.Common.FileExport;

public interface IQrCodeGenerator
{
    byte[] GetBytes(string text);
}
