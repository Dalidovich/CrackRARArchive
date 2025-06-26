using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;

namespace CrackRARArchive
{
    public class ArchiveWorker
    {
        public string PASSWORD = "not exist";

        public string RarFilePath = @"C:\Users\Ilia\Desktop\test.rar";
        public string WorkSpacePath = @"C:\Users\Ilia\Desktop\WorkSpace\";
        public string ExtractPath = @"C:\Users\Ilia\Desktop\ExtractSpace\";

        public ArchiveWorker(string password, string rarFilePath, string workSpacePath, string extractPath)
        {
            PASSWORD = password;
            RarFilePath = rarFilePath;
            WorkSpacePath = workSpacePath;
            ExtractPath = extractPath;
        }

        public bool TryExtract(string rarFilePath, string extractPath, string password)
        {
            try
            {
                using (var archive = RarArchive.Open(rarFilePath, new SharpCompress.Readers.ReaderOptions() { Password = password }))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            entry.WriteToDirectory(extractPath, new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true,

                            });
                        }
                    }
                }
                Console.WriteLine("Распаковка завершена успешно!");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task WorkMultyTask()
        {
            var countSymbols = 4;

            var iterators = new List<IteratorOnArray> { new IteratorOnArray(countSymbols) };

            for (int g = 0; g < countSymbols; g++)
            {
                for (int i = 0; i < IteratorOnArray.line.Length; i++)
                {
                    if (i == IteratorOnArray.line.Length - 1)
                    {
                        iterators.Add(new IteratorOnArray(countSymbols, g, i, 0));
                    }
                    else
                    {
                        iterators.Add(new IteratorOnArray(countSymbols, g, i, i + 1));
                    }
                }
            }


            for (int i = 0; i < IteratorOnArray.line.Length * countSymbols; i++)
            {
                File.Copy(RarFilePath, WorkSpacePath + $"{i}_test.rar", true);
                Directory.CreateDirectory(ExtractPath + $"_{i}");
            }

            var count = iterators.Count;

            var tasks = new Task[count];

            Console.WriteLine("enviroment created");

            for (int i = 0; i < count; i++)
            {
                var iClone = i;
                tasks[iClone] = Task.Run(() =>
                {
                    for (int z = 0; !iterators[iClone].endIteration; z++)
                    {
                        if (PASSWORD != "not exist") return;
                        var strValue = iterators[iClone].getItem();
                        iterators[iClone].next();

                        if (TryExtract(WorkSpacePath + $"{iClone}_test.rar", ExtractPath + $"_{iClone}", strValue))
                        {
                            PASSWORD = strValue;
                            Console.WriteLine($"exctract is succsess with password - {strValue}\ti - {iClone}");
                            TryExtract(WorkSpacePath + $"{iClone}_test.rar", ExtractPath, strValue);
                            return;
                        }
                    }
                    Console.WriteLine($"{iClone} task end");
                });
            }

            await Task.WhenAll(tasks);

        }

        public void WorkWithSingleTask()
        {
            var countSymbols = 3;
            var iterator = new IteratorOnArray(countSymbols);

            for (int i = 0; !iterator.endIteration; i++)
            {
                var strValue = iterator.getItem();
                iterator.next();

                if (TryExtract(RarFilePath, ExtractPath, strValue))
                {
                    Console.WriteLine($"exctract is succsess with password - {strValue}");
                    return;
                }
            }
        }
    }
}
