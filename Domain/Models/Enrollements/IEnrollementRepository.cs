using Domain.Models.Enrollements;

namespace Domain.Tests.Enrollements;

public interface IEnrollementRepository
{
    void Add(Enrollement Enrollement);
    void Update(Enrollement enrollement);
    Enrollement GetById(Guid id);
}
