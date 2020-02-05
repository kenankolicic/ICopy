namespace iCopy.Web.Helper
{
    public class Graphics
    {
        public class Image
        {
            public static System.Drawing.Bitmap Resize(System.Drawing.Image image, int width, int height) 
            {
                var destRect = new System.Drawing.Rectangle(0, 0, width, height);
                var destImage = new System.Drawing.Bitmap(width, height);

                destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                using(var graphics = System.Drawing.Graphics.FromImage(image))
                using(var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel, wrapMode);
                }

                return destImage;
            }
        }
    }
}
