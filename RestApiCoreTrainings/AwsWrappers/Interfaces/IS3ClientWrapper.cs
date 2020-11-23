using System.Threading.Tasks;
using Common;

namespace AWSWrappers.Interfaces
{
    public interface IS3ClientWrapper
    {
        Task<Result> UploadAsync(string filePath, string key);
    }
}
