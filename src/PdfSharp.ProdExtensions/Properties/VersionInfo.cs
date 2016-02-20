using System.Reflection;

[assembly: AssemblyCopyright("Copyright \x00a9 Yin-Chun Wang 2016")]
[assembly: AssemblyCompany("Yin-Chun Wang")]

[assembly: AssemblyVersion(PdfSharp.ProdExtensions.VersionInfo.Release)]
[assembly: AssemblyFileVersion(PdfSharp.ProdExtensions.VersionInfo.Build)]
[assembly: AssemblyInformationalVersion(PdfSharp.ProdExtensions.VersionInfo.Build)]

namespace PdfSharp.ProdExtensions
{
    /// <summary>
    /// Contains version info of NTwain.
    /// </summary>
    static class VersionInfo
    {
        /// <summary>
        /// The major release version number.
        /// </summary>
        public const string Release = "1.0.0.0"; // keep this same in major (breaking) releases

        
        /// <summary>
        /// The build release version number.
        /// </summary>
        public const string Build = "1.0.2"; // change this for each nuget release


    }
}