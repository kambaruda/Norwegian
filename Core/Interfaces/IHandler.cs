namespace Core.Interfaces
{
    public interface IHandler<in TRequest, out TResponse>
        where TResponse : IResponse
    {
        TResponse Handle(TRequest request);
    }
}
