using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories
{
    public interface IEntryCommentRepository : IGenericRepository<EntryComment>
    {
    }
}