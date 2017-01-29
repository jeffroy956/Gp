using GpCore.Model.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpCore.Model.Sql
{
    public class SqlUnitOfWork : IDisposable, UnitOfWork
    {
        private SqlConnection _unitOfWork;
        private SqlTransaction _uowTransaction;
        private bool _completed = false;
        public SqlUnitOfWork(string connectionString)
        {
            _unitOfWork = new SqlConnection(connectionString);
            _unitOfWork.Open();
            _uowTransaction = _unitOfWork.BeginTransaction();
        }

        public SqlCommand CreateCommand()
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.Transaction = _uowTransaction;

            return cmd;
        }

        public void Commit()
        {
            _uowTransaction.Commit();
            _completed = true;
        }

        public void Rollback()
        {
            _uowTransaction.Rollback();
            _completed = true;
        }

    #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!_completed)
                    {
                        _uowTransaction.Rollback();
                    }

                    _uowTransaction.Dispose();
                    _unitOfWork.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
