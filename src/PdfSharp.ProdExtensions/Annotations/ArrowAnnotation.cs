using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfSharp.Pdf.Annotations
{
    /// <summary>
    /// Annotation with arrow line.
    /// </summary>
    public class ArrowAnnotation : PdfAnnotation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArrowAnnotation"/> class.
        /// </summary>
        /// <param name="color">The arrow color.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        public ArrowAnnotation(XColor color, float x1, float y1, float x2, float y2)
        {
            Elements.SetName(Keys.Subtype, "/Line");

            var line = new PdfArray();
            line.Elements.Add(new PdfReal(x1));
            line.Elements.Add(new PdfReal(y1));
            line.Elements.Add(new PdfReal(x2));
            line.Elements.Add(new PdfReal(y2));
            Elements["/L"] = line;

            // change line ending
            PdfArray end = new PdfArray();
            end.Elements.Add(new PdfName("/ClosedArrow"));
            end.Elements.Add(new PdfName("/None"));
            Elements["/LE"] = end;

            // change all color properties
            Elements.SetColor("/IC", color);
            Color = color;

            // change line width
            var bsDict = new PdfBorderStyle(3, BorderStyle.Solid);
            Elements[Keys.BS] = bsDict;
        }
    }
}
