using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfSharp.Pdf.Annotations
{

    /// <summary>
    /// Annotation for colored rectangle or circle.
    /// </summary>
    public class SquareCircleAnnotation : PdfAnnotation
    {
        /// <summary>
        /// Create a redaction square <see cref="SquareCircleAnnotation"/> with black color.
        /// </summary>
        /// <returns></returns>
        public static SquareCircleAnnotation CreateRedaction()
        {
            return new SquareCircleAnnotation(true)
            {
                FillColor = XColors.Black
            };
        }

        /// <summary>
        /// Create a highlight square <see cref="SquareCircleAnnotation"/> with transparent yellow color.
        /// </summary>
        /// <returns></returns>
        public static SquareCircleAnnotation CreateHighlight()
        {
            return new SquareCircleAnnotation(true)
            {
                FillColor = XColors.Yellow,
                Opacity = 0.5
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareCircleAnnotation"/> class.
        /// </summary>
        /// <param name="square">if set to <c>true</c> [square].</param>
        public SquareCircleAnnotation(bool square)
        {
            if (square)
            {
                this.Elements.SetName(Keys.Subtype, "/Square");
            }
            else
            {
                this.Elements.SetName(Keys.Subtype, "/Circle");
            }
        }

        /// <summary>
        /// Gets or sets the fill color.
        /// </summary>
        /// <value>
        /// The fill color.
        /// </value>
        public XColor FillColor
        {
            get
            {
                PdfItem pdfItem = this.Elements["/IC"];
                if (pdfItem is PdfArray)
                {
                    PdfArray pdfArray = (PdfArray)pdfItem;
                    if (pdfArray.Elements.Count == 3)
                        return XColor.FromArgb((int)(pdfArray.Elements.GetReal(0) * (double)byte.MaxValue), (int)(pdfArray.Elements.GetReal(1) * (double)byte.MaxValue), (int)(pdfArray.Elements.GetReal(2) * (double)byte.MaxValue));
                }
                return XColors.Black;
            }
            set
            {
                Elements.SetColor("/IC", value);
                this.Elements.SetDateTime("/M", DateTime.Now);
            }
        }
    }
}
