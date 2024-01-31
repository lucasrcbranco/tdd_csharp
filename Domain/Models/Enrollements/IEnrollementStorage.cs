namespace Domain.Tests.Enrollements
{
    public interface IEnrollementStorage
    {
        void Storage(EnrollementDto EnrollementDto);
        void CompleteEnrollement(Guid enrollementId, decimal grade);
        void CancelEnrollement(Guid enrollementId);
    }
}