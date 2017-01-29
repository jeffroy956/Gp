namespace GpCore.Model.Common
{
    public interface UnitOfWork
    {
        void Commit();
        void Rollback();
    }
}