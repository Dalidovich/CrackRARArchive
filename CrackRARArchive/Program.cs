namespace CrackRARArchive
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var iteratorTest = new IteratorTest();
            await iteratorTest.Tests();

            var archiveWorker = new ArchiveWorker("", "", "", "");
            archiveWorker.WorkWithSingleTask();
            await archiveWorker.WorkMultyTask();
        }
    }
}
