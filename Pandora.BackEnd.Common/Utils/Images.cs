using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Pandora.BackEnd.Common.Utils
{
    public static class Images
    {
        public static Image ConvertByteArrayToImage(byte[] arrayOfByetes)
        {
            MemoryStream ms = new MemoryStream(arrayOfByetes);
            Image image = Image.FromStream(ms);
            return image;
        }

        public static byte[] ConvertImagenToByteArray(Image image, ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, format);
            return ms.ToArray();
        }
    }
}
