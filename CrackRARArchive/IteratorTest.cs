using System.Diagnostics;

namespace CrackRARArchive
{
    public class IteratorTest
    {
        public readonly string PasswordForTest = "5h>?=";
        public readonly int CountSymbolsForPassword = 5;

        public IteratorTest(string? passwordForTest = null, int? countSymbolsInTryPasswordForTest = null)
        {
            PasswordForTest = passwordForTest ?? PasswordForTest;
            CountSymbolsForPassword = countSymbolsInTryPasswordForTest ?? CountSymbolsForPassword;
        }

        public async Task Tests()
        {
            var iteratorManager = new IteratorTest();
            Console.WriteLine("single");
            iteratorManager.TestSimpleIterator();
            Console.WriteLine("multy");
            await iteratorManager.TestMultyTask();
        }

        public void TestSimpleIterator()
        {
            var sw = Stopwatch.StartNew();

            var iterator = new IteratorOnArray(CountSymbolsForPassword);

            for (int i = 0; !iterator.endIteration; i++)
            {
                var strValue = iterator.getItem();
                iterator.next();
                //Console.WriteLine(strValue);

                if (strValue == PasswordForTest)
                {
                    Console.WriteLine($"UNLOCK with password - \'{strValue}\'");
                    sw.Stop();
                    Console.WriteLine($"time - {sw.ElapsedMilliseconds}");
                    return;
                }

            }

            sw.Stop();
            Console.WriteLine($"time - {sw.ElapsedMilliseconds}");
        }

        public async Task TestMultyTask()
        {
            var sw = Stopwatch.StartNew();

            var iterators = new List<IteratorOnArray>
            {
                new IteratorOnArray(CountSymbolsForPassword)

            };

            for (int i = 0; i < IteratorOnArray.line.Length; i++)
            {
                if (i == IteratorOnArray.line.Length - 1)
                {
                    iterators.Add(new IteratorOnArray(CountSymbolsForPassword, 0, i, 0));
                }
                else
                {
                    iterators.Add(new IteratorOnArray(CountSymbolsForPassword, 0, i, i + 1));
                }
            }

            var count = iterators.Count;

            var tasks = new Task[count];
            var findPassword = false;

            for (int i = 0; i < count; i++)
            {
                var iClone = i;
                tasks[iClone] = Task.Run(() =>
                {
                    for (int z = 0; !iterators[iClone].endIteration; z++)
                    {
                        if (findPassword) return;
                        var strValue = iterators[iClone].getItem();

                        if (strValue == PasswordForTest)
                        {
                            Console.WriteLine($"UNLOCK with password - \'{strValue}\'");
                            Console.WriteLine($"task n_{iClone} find password");
                            findPassword = true;
                            return;
                        }

                        iterators[iClone].next();
                    }
                    Console.WriteLine($"task n_{iClone} close");
                });
            }

            await Task.WhenAll(tasks);

            sw.Stop();
            Console.WriteLine($"time - {sw.ElapsedMilliseconds}");
        }
    }
}
