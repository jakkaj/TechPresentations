using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorsAndPensDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI;

    namespace TimeToShineClient.Util
    {
        public static class ColorUtils
        {
            /// <summary>
            /// Convert rgb to hsv color
            /// </summary>
            /// <param name="color">rgb color</param>
            /// <returns>hsv color</returns>
            public static float[] ToHSV(Color color)
            {
                var rgb = new float[]
                {
                color.R / 255f, color.G / 255f, color.B / 255f
                };

                // RGB to HSV
                float max = rgb.Max();
                float min = rgb.Min();

                float h, s, v;
                if (max == min)
                {
                    h = 0f;
                }
                else if (max == rgb[0])
                {
                    h = (60f * (rgb[1] - rgb[2]) / (max - min) + 360f) % 360f;
                }
                else if (max == rgb[1])
                {
                    h = 60f * (rgb[2] - rgb[0]) / (max - min) + 120f;
                }
                else
                {
                    h = 60f * (rgb[0] - rgb[1]) / (max - min) + 240f;
                }

                if (max == 0d)
                {
                    s = 0f;
                }
                else
                {
                    s = (max - min) / max;
                }
                v = max;

                return new float[] { h, s, v };
            }

            /// <summary>
            /// Convert hsv to rgb color
            /// </summary>
            /// <param name="hue">hue</param>
            /// <param name="saturation">saturation</param>
            /// <param name="brightness">brightness</param>
            /// <returns>Color</returns>
            public static Color FromHsv(float hue, float saturation, float brightness)
            {
                if (saturation == 0)
                {
                    var c = (byte)Math.Round(brightness * 255f, MidpointRounding.AwayFromZero);
                    return ColorHelper.FromArgb(0xff, c, c, c);
                }

                var hi = ((int)(hue / 60f)) % 6;
                var f = hue / 60f - (int)(hue / 60d);
                var p = brightness * (1 - saturation);
                var q = brightness * (1 - f * saturation);
                var t = brightness * (1 - (1 - f) * saturation);

                float r, g, b;
                switch (hi)
                {
                    case 0:
                        r = brightness;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = brightness;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = brightness;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = brightness;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = brightness;
                        break;

                    case 5:
                        r = brightness;
                        g = p;
                        b = q;
                        break;

                    default:
                        throw new InvalidOperationException();
                }

                return ColorHelper.FromArgb(
                    0xff,
                    (byte)Math.Round(r * 255d),
                    (byte)Math.Round(g * 255d),
                    (byte)Math.Round(b * 255d));
            }
        }
    }

}
