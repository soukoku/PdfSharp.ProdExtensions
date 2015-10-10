using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PdfSharp.Pdf.Annotations
{
    /// <summary>
    /// Annotation as text overlay.
    /// </summary>
    public class FreeTextAnnotation : PdfAnnotation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FreeTextAnnotation"/> class.
        /// </summary>
        /// <param name="color">The text color.</param>
        /// <param name="font">The font name.</param>
        /// <param name="fontSize">Size of the font.</param>
        public FreeTextAnnotation(XColor color, string font, double fontSize)
        {
            Elements.SetName(Keys.Subtype, "/FreeText");
            
            string daString = string.Format(CultureInfo.InvariantCulture,
                "{0} {1} {2} rg /{3} {4} Tf /BG []",
                color.R / (double)byte.MaxValue,
                color.G / (double)byte.MaxValue,
                color.B / (double)byte.MaxValue,
                font, fontSize);
            Elements["/DA"] = new PdfString(daString);

            // no border
            var border = new PdfArray();
            border.Elements.Add(new PdfInteger(0));
            border.Elements.Add(new PdfInteger(0));
            border.Elements.Add(new PdfInteger(0));
            Elements[Keys.Border] = border;
        }
    }
}
