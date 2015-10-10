using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfSharp.Pdf
{
    /// <summary>
    /// A <see cref="PdfDictionary"/> for /BS value.
    /// </summary>
    public class PdfBorderStyle : PdfDictionary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfBorderStyle"/> class.
        /// </summary>
        /// <param name="width">The border width.</param>
        public PdfBorderStyle(int width)
            : this(width, BorderStyle.Solid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfBorderStyle"/> class.
        /// </summary>
        /// <param name="width">The border width.</param>
        /// <param name="style">The border style.</param>
        public PdfBorderStyle(int width, BorderStyle style)
        {
            Elements.SetInteger("/W", width);
            switch (style)
            {
                case BorderStyle.Solid:
                    Elements.SetName("/S", "/S");
                    break;
                case BorderStyle.Dashed:
                    Elements.SetName("/S", "/D");
                    break;
                case BorderStyle.Beveled:
                    Elements.SetName("/S", "/B");
                    break;
                case BorderStyle.Inset:
                    Elements.SetName("/S", "/I");
                    break;
                case BorderStyle.Underline:
                    Elements.SetName("/S", "/U");
                    break;
            }
        }

        ///// <summary>
        ///// Gets or sets the color of the fill.
        ///// </summary>
        ///// <value>
        ///// The color of the fill.
        ///// </value>
        //public XColor FillColor
        //{
        //    get
        //    {
        //        PdfArray pdfArray = this.Elements["/IC"] as PdfArray;
        //        if (pdfArray != null && pdfArray.Elements.Count == 3)
        //            return XColor.FromArgb((int)(pdfArray.Elements.GetReal(0) * (double)byte.MaxValue), (int)(pdfArray.Elements.GetReal(1) * (double)byte.MaxValue), (int)(pdfArray.Elements.GetReal(2) * (double)byte.MaxValue));
        //        return XColors.Black;
        //    }
        //    set
        //    {
        //        this.Elements["/IC"] = new PdfArray(this.Owner, new PdfItem[3]
        //        {
        //          new PdfReal((double) value.R / (double) byte.MaxValue),
        //          new PdfReal((double) value.G / (double) byte.MaxValue),
        //          new PdfReal((double) value.B / (double) byte.MaxValue)
        //        });
        //        this.Elements.SetDateTime("/M", DateTime.Now);
        //    }
        //}
    }

    /// <summary>
    /// Border style values.
    /// </summary>
    public enum BorderStyle
    {
        Solid = 0,
        Dashed = 1,
        Beveled = 2,
        Inset = 3,
        Underline = 4,
    }
}
