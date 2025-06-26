using System.Numerics;

namespace CrackRARArchive
{
    public class IteratorOnArray
    {
        public readonly static string line = "qwertyuiopasdfghjklzxcvbnm[{]};:\'\",<.>/?\\|`~!@#$%^&*()_+1234567890-=";//count 68
        //public readonly static string line = "0123456789";
        public int[] verticalIndexs;
        public int countSymbols;
        public bool endIteration = false;

        public int startOfIterDimansion;
        public int startIndex;
        public int endIndex;

        public BigInteger maxCountIteration = 0;

        public IteratorOnArray(int countSymbols, int startOfIterDimansion, int startIndex, int endIndex)
        {
            this.countSymbols = countSymbols;
            verticalIndexs = new int[countSymbols];
            for (int i = 0; i < countSymbols; i++)
            {
                verticalIndexs[i] = -1;
            }
            verticalIndexs[countSymbols - 1] = 0;
            this.countSymbols = countSymbols;

            for (int i = startOfIterDimansion; i < countSymbols; i++)
            {
                verticalIndexs[i] = 0;
            }
            verticalIndexs[startOfIterDimansion] = startIndex;
            this.startOfIterDimansion = startOfIterDimansion;
            this.startIndex = startIndex;
            this.endIndex = endIndex;

            for (int i = 0; i <= countSymbols; i++)
            {
                maxCountIteration += BigInteger.Pow(line.Length, i);
            }
        }

        public IteratorOnArray(int countSymbols)
        {
            this.countSymbols = countSymbols;
            verticalIndexs = new int[countSymbols];
            for (int i = 0; i < countSymbols; i++)
            {
                verticalIndexs[i] = -1;
            }

            for (int i = 0; i <= countSymbols; i++)
            {
                maxCountIteration += BigInteger.Pow(line.Length, i);
            }
        }

        public void next()
        {
            if ((startIndex != endIndex && verticalIndexs[startOfIterDimansion] == endIndex) || maxCountIteration <= 0)
            {
                endIteration = true;
                return;
            }

            maxCountIteration--;

            verticalIndexs[countSymbols - 1]++;
            for (int i = countSymbols - 1; i >= 0; i--)
            {
                if (verticalIndexs[i] == line.Length)
                {
                    verticalIndexs[i] = 0;
                    if (i == 0) break;
                    verticalIndexs[i - 1] += 1;
                }
            }
        }
        public string getItem()
        {
            var outputString = "";
            for (int i = 0; i < verticalIndexs.Length; i++)
            {
                if (verticalIndexs[i] != -1)
                    outputString += line[verticalIndexs[i]];
            }
            return outputString;
        }
    }
}
