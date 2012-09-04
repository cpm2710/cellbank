using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionStreamTest
{
    //public class FileManager : IDisposable
    //{
    //    private bool _commited = false;
    //    private SafeFileHandle _tx = null;

    //    public FileManager()
    //    {
    //        _tx = TransactionInterop.CreateTransaction(new TransactionInterop.SECURITY_ATTRIBUTES(), IntPtr.Zero, 0, 0, 0, 0, null);
    //    }

    //    public bool DeleteFile(string filename)
    //    {
    //        return TransactionInterop.DeleteFileTransacted(filename, _tx);
    //    }

    //    public void Commit()
    //    {
    //        if (TransactionInterop.CommitTransaction(_tx))
    //            _commited = true;
    //    }

    //    private void Rollback()
    //    {
    //        TransactionInterop.RollbackTransaction(_tx);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            if (!_commited)
    //            {
    //                Rollback();
    //            }
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //    }
    //}
}
