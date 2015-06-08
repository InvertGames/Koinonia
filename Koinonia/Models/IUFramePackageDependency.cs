namespace Koinonia.Models
{
    public interface IUFramePackageDependency
    {
        string PackageCode { get; set; }
        string MinorVersion { get; set; }
        string MajorVersion { get; set; }
    }
}