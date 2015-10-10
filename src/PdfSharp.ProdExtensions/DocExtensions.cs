using PdfSharp.Drawing;
using PdfSharp.Pdf.Advanced;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace PdfSharp.Pdf
{
    /// <summary>
    /// Contains extension methods for <see cref="PdfDocument"/>.
    /// </summary>
    public static class DocExtensions
    {
        /// <summary>
        /// Sets the action when pdf is opened.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="action">The action.</param>
        public static void SetOpenAction(this PdfDocument doc, PdfDictionary action)
        {
            if (doc == null) { throw new ArgumentNullException("doc"); }

            if (action == null)
            {
                doc.Internals.Catalog.Elements.Remove("/OpenAction");
            }
            else
            {
                doc.Internals.Catalog.Elements["/OpenAction"] = action;
            }
        }

        /// <summary>
        /// Sets the additional action when some event occurred for the pdf.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="action">The action.</param>
        public static void SetAdditionalAction(this PdfDocument doc, PdfName actionType, PdfDictionary action)
        {
            if (doc == null) { throw new ArgumentNullException("doc"); }
            if (actionType == null) { throw new ArgumentNullException("actionType"); }

            var oldAA = doc.ResolveObject<PdfDictionary>("/AA");
            if (oldAA == null)
            {
                oldAA = new PdfDictionary();
                doc.Internals.Catalog.Elements["/AA"] = oldAA;
                //doc.Internals.AddObject(oldAA);
                //doc.Internals.Catalog.Elements["/AA"] = PdfInternals.GetReference(oldAA);
            }

            if (action == null)
            {
                oldAA.Elements.Remove(actionType.Value);
            }
            else
            {
                oldAA.Elements[actionType.Value] = action;
            }
        }

        /// <summary>
        /// Gets an object from the catalog and resolves it if necessary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc">The document.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static T ResolveObject<T>(this PdfDocument doc, string name) where T : PdfItem
        {
            if (doc == null) { throw new ArgumentNullException("doc"); }
            if (string.IsNullOrEmpty(name)) { return null; }

            var test = doc.Internals.Catalog.Elements[name];

            var refObj = test as PdfReference;
            if (refObj != null)
            {
                test = doc.Internals.GetObject(refObj.ObjectID);
            }

            return test as T;
        }


        #region add pages

        /// <summary>
        /// Merges all the image pages into a new pdf page for the specified doc.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="srcDoc">The source document.</param>
        /// <param name="newPageCallback">The callback after the image has been added as a new pdf page.</param>
        /// <param name="includePageCallback">The callback to check if a page should be included.</param>
        /// <exception cref="System.ArgumentNullException">doc</exception>
        public static void AddMultipage(this PdfDocument doc, PdfDocument srcDoc, Action<int, PdfPage> newPageCallback, Predicate<int> includePageCallback = null)
        {
            if (doc == null) { throw new ArgumentNullException("doc"); }
            if (srcDoc != null)
            {
                var total = srcDoc.PageCount;
                for (int i = 0; i < total;)
                {
                    i++;
                    if (includePageCallback == null || includePageCallback(i))
                    {
                        var newPg = doc.AddPage(srcDoc.Pages[i]);
                        if (newPageCallback != null)
                        {
                            newPageCallback(i, newPg);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Merges all the image pages into a new pdf page for the specified doc.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="image">The image.</param>
        /// <param name="newPageCallback">The callback after the image has been added as a new pdf page.</param>
        /// <param name="includePageCallback">The callback to check if a page should be included.</param>
        /// <exception cref="System.ArgumentNullException">doc</exception>
        public static void AddMultipage(this PdfDocument doc, Image image, Action<int, PdfPage> newPageCallback, Predicate<int> includePageCallback = null)
        {
            if (doc == null) { throw new ArgumentNullException("doc"); }
            if (image != null)
            {
                var total = image.GetFrameCount(FrameDimension.Page);
                for (int i = 0; i < total;)
                {
                    i++;
                    if (includePageCallback == null || includePageCallback(i))
                    {
                        image.SelectActiveFrame(FrameDimension.Page, i);

                        var newPg = AddSinglePageReal(doc, image);
                        if (newPageCallback != null)
                        {
                            newPageCallback(i, newPg);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Merges the current image as a new pdf page for the specified doc.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="image">The image.</param>
        public static PdfPage AddSinglePage(this PdfDocument doc, Image image)
        {
            if (doc == null) { throw new ArgumentNullException("doc"); }
            if (image != null)
            {
                return AddSinglePageReal(doc, image);
            }
            return null;
        }

        static PdfPage AddSinglePageReal(PdfDocument doc, Image image)
        {
            // in case of diff x/y resolutions
            var widthPt = XUnit.FromPoint(72 * (image.Width / image.HorizontalResolution));
            var heightPt = XUnit.FromPoint(72 * (image.Height / image.VerticalResolution));

            var newPage = doc.Pages.Add(new PdfPage
            {
                Width = widthPt,
                Height = heightPt
            });
            using (var xgr = XGraphics.FromPdfPage(newPage))
            {
                // cannot dispose pdfImg in loop or it will dispose real image as well
                var pdfImg = XImage.FromGdiPlusImage(image);
                xgr.DrawImage(pdfImg, 0, 0, newPage.Width, newPage.Height);
            }
            return newPage;
        }

        #endregion
    }
}
