using Microsoft.AspNetCore.Http;

namespace Utils.Dependency
{
    public interface IProvider<T>
    {
        T Get();
    }

    public class Provider<T> : IProvider<T>
    {
        IHttpContextAccessor contextAccessor;
        public Provider(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        T IProvider<T>.Get()
        {
            return (T)contextAccessor.HttpContext.RequestServices.GetService(typeof(T));
        }
    }
}
