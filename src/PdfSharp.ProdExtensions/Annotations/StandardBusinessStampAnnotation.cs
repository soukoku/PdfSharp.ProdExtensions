using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfSharp.Pdf.Annotations
{

    /// <summary>
    /// Stamp annotation using adobe's built-in standard business stamps.
    /// </summary>
    public class StandardBusinessStampAnnotation : PdfAnnotation
    {
        static readonly Type IconType = typeof(StandardBusinessStampIcon);

        /// <summary>
        /// Parses the name string into <see cref="StandardBusinessStampIcon"/>.
        /// </summary>
        /// <param name="stampName">Name of the stamp.</param>
        /// <returns></returns>
        public static StandardBusinessStampIcon ParseIconName(string stampName)
        {
            if (Enum.IsDefined(IconType, stampName))
            {
                return (StandardBusinessStampIcon)Enum.Parse(IconType, stampName);
            }
            return StandardBusinessStampIcon.NoIcon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardBusinessStampAnnotation"/> class.
        /// </summary>
        public StandardBusinessStampAnnotation()
        {
            this.Elements.SetName(Keys.Subtype, "/Stamp");
        }

        /// <summary>
        /// Gets or sets an icon to be used in displaying the annotation.
        /// </summary>
        public StandardBusinessStampIcon Icon
        {
            get
            {
                string name = this.Elements.GetName("/Name");
                if (string.IsNullOrEmpty(name) || name.Length < 3)
                {
                    return StandardBusinessStampIcon.NoIcon;
                }
                return ParseIconName(name.Substring(3));
            }
            set
            {
                if (Enum.IsDefined(IconType, value) && value != StandardBusinessStampIcon.NoIcon)
                    this.Elements.SetName("/Name", "/SB" + value.ToString());
                else
                    this.Elements.Remove("/Name");
            }
        }
    }

    /// <summary>
    /// Icon values for use with <see cref="StandardBusinessStampAnnotation"/>.
    /// </summary>
    public enum StandardBusinessStampIcon
    {
        NoIcon = 0,
        Approved,
        Completed,
        Confidential,
        Draft,
        Final,
        ForComment,
        ForPublicRelease,
        NotApproved,
        NotForPublicRelease,
        Void
    }
}
