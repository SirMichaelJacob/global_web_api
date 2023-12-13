namespace global_web_api.Interfaces
{
    /// <summary>
    /// Generic Interface to Implement any Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGeneric<T>
    {
        Task<HttpResponseMessage> Add(T obj);
        Task<HttpResponseMessage> Update(int id, T obj);
        Task<HttpResponseMessage> Delete(int id);
        Task<T> GetItem(int id);
        Task<HttpContent> GetAll();


    }
}
