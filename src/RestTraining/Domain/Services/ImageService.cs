using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace RestTraining.Api.Domain.Services
{
    public class ImageService
    {
        public const int maxImageHeight = 200;
        public const int maxImageWidth = 300;

        public static byte[] ToPngImageBytes(byte[] bytes)
        {
            if (bytes == null)
                return null;

            using (MemoryStream input = new MemoryStream(bytes))
            {
                return ToPngImageBytes(input);
            }
        }

        public static byte[] ToPngImageBytes(Stream inputStream)
        {
            if (inputStream == null)
                return null;

            try
            {
                using (inputStream)
                {
                    var image = Image.FromStream(inputStream);

                    using (MemoryStream output = new MemoryStream())
                    {
                        image = ScaleImage(image, maxImageWidth, maxImageHeight);
                        image.Save(output, ImageFormat.Png);
                        return output.ToArray();
                    }
                }
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
    }
}