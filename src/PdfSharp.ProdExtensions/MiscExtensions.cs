using PdfSharp.Drawing;
using PdfSharp.Pdf.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfSharp.Pdf
{
    /// <summary>
    /// Contains extension methods for other PdfSharp things.
    /// </summary>
    public static class MiscExtensions
    {
        /// <summary>
        /// Sets the color to the specified key.
        /// </summary>
        /// <param name="elements">The elements.</param>
        /// <param name="key">The key.</param>
        /// <param name="color">The color.</param>
        public static void SetColor(this PdfDictionary.DictionaryElements elements, string key, XColor color)
        {
            if (elements == null) { throw new ArgumentNullException("elements"); }
            if (!string.IsNullOrEmpty(key))
            {
                var arr = new PdfArray();
                arr.Elements.Add(new PdfReal(color.R / (double)byte.MaxValue));
                arr.Elements.Add(new PdfReal(color.G / (double)byte.MaxValue));
                arr.Elements.Add(new PdfReal(color.B / (double)byte.MaxValue));
                elements[key] = arr;
            }
        }
    }
}
