namespace Exersice2
{
    public static class Server
    {
        private static int _count = 0;
        private static ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();

        public static int GetCount()
        {
            _readerWriterLock.EnterReadLock(); 
            try
            {
                return _count;
            }
            finally
            {
                _readerWriterLock.ExitReadLock(); 
            }
        }

        public static void AddToCount(int value)
        {
            _readerWriterLock.EnterWriteLock(); 
            try
            {
                _count += value;
            }
            finally
            {
                _readerWriterLock.ExitWriteLock(); 
            }
        }
    }
}
