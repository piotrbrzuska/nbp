using MediatR;

namespace nbp.core.commands
{
    public abstract class Command<T> : IRequest<T>
    {
        
    }
}