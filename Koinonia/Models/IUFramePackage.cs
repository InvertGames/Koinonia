using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.Models
{
    public interface IUFramePackage
    {

            string Name { get; set; }
            string Description { get; set; }
            string PackageCode { get; set; }
            int MinorVersion { get; set; }
            int MajorVersion { get; set; }
            string PackageUrl { get; set; }
            string AccessToken { get; set; }
            int AuthorId { get; set; } //TODO this may be changed to something like a User object.
            IList<IUFramePackageDependency> Dependencies { get; set; }

    }
}
