using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfSharp.Pdf
{

    // see
    // http://forum.pdfsharp.net/viewtopic.php?f=2&t=368
    // http://www.pdfsharp.com/PDFsharp/index.php?option=com_content&task=view&id=20&Itemid=32


    /// <summary>
    /// Represents a javascript object in pdf. This can be used as the pdf action.
    /// </summary>
    public class PdfJavaScript : PdfDictionary
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="PdfJavaScript"/> class.
        /// </summary>
        public PdfJavaScript()
        {
            Elements.SetName("/S", "/JavaScript");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfJavaScript"/> class.
        /// </summary>
        public PdfJavaScript(string value)
            : this()
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the script value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value
        {
            get
            {
                if (elements.ContainsKey("/JS"))
                {
                    return Elements["/JS"].ToString();
                }
                return null;
            }
            set
            {
                Elements.SetString("/JS", value);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
