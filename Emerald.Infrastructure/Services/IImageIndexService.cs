using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Services
{
    public interface IImageIndexService
    {
        Task DecreaseImageReference(string imageId);
        Task IncreaseImageReference(string imageId);
    }
}
