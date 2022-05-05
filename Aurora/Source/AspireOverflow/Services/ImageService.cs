using System.Drawing;

namespace AspireOverflow.Services
{
    public static class ImageService
    {
        public static byte[] ImageToByteArray(IFormFile imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.CopyTo(ms);
                return ms.ToArray();
            }
        }
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }
    }
}