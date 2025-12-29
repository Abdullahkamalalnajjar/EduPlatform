using QRCoder;

namespace Project.Data.Helpers;

public static class QRCodeHelper
{
    public static string GenerateQrCodeSvg(string content)
    {
        var generator = new QRCodeGenerator();
        var data = generator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);

        var svgQrCode = new SvgQRCode(data);
        string svgImage = svgQrCode.GetGraphic(5);

        return svgImage; // ده string SVG ممكن تعرضه مباشرة في الـ frontend
    }
}
