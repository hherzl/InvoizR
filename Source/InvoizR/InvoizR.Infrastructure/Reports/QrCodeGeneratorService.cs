using System.Drawing.Imaging;
using InvoizR.Application.Common.Contracts;
using QRCoder;

namespace InvoizR.Infrastructure.Reports;

public class QrCodeGeneratorService : IQrCodeGenerator
{
    public byte[] GetBytes(string text)
    {
        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new QRCode(qrCodeData);
        var qrCodeImage = qrCode.GetGraphic(20);

        using var stream = new MemoryStream();
        qrCodeImage.Save(stream, ImageFormat.Png);

        return stream.ToArray();
    }
}
