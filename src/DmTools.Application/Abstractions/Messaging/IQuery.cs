using Ardalis.Result;

using MediatR;

namespace DmTools.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
