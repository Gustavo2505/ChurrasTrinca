using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurrasTrinca.Helpers.MarkupExtensions
{
    [ContentProperty(nameof(ImageSource))]
    public class ImageSourceFromSvgExtension : IMarkupExtension
    {
        public string SvgSource { get; set; }

        public int ImageWidth { get; set; }

        public int ImageHeight { get; set; }

        public Color? SvgTint { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (SvgSource == null)
            {
                return null;
            }

            Dictionary<string, string> replaceMap = null;

            if (SvgTint != null)
            {
                replaceMap = new Dictionary<string, string>();
                replaceMap.Add("stroke=\"#03B2C7\"", $"stroke=\"{SvgTint.Value.ToHex()}\"");
            }

            var img = SvgImageSource.FromResource(SvgSource, vectorWidth: ImageWidth, vectorHeight: ImageHeight, replaceStringMap: replaceMap);
            return img;

        }

    }
}
