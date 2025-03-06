namespace InvoizR.Application.Common.Contracts;

public interface IQrCodeGenerator
{
    byte[] GetBytes(string text);
}
