namespace Gp.Data.Common
{
    public interface UnitOfWork
    {
        void Commit();
        void Rollback();
    }
}