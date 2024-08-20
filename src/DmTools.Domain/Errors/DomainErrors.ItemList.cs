using KJWT.SharedKernel.Results;

namespace DmTools.Domain.Errors;

public static partial class DomainErrors
{
    public static class ItemList
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "ItemList.NotFound",
            $"The Item List with the the identifier {id} was not found.");
    }
}
