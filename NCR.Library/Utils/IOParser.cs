using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCR.Library.Extensions;

namespace NCR.Library.Utils
{
    public sealed class IOParser : IDisposable
    {
        private readonly string _inputFile;
        private readonly IList<int[]> _cachedMatrix = new List<int[]>();

        public IOParser(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<bool> ReadToEnd()
        {
            try
            {
                var txt = File.ReadAllText(_inputFile, Encoding.UTF8);
                var reader = new StringReader(txt);
                var line = "";

                while (!String.IsNullOrWhiteSpace(line = await reader.ReadLineAsync()))
                {
                    var splittedNumbers = line.Split(new string[] {",", "-"}, StringSplitOptions.RemoveEmptyEntries);

                    if (splittedNumbers.Length == 0)
                        _cachedMatrix.Add(new int[] {});
                    else
                    {
                        try
                        {
                            var arrray = splittedNumbers.Select(s => int.Parse(s)).ToArray();

                            _cachedMatrix.Add(arrray);

                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception); 
                        }
                    }
                }

               return  await Task.FromResult(true);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return await Task.FromResult(false);
        }

        public async Task WriteToEnd(string outputFile)
        {
            var writer = new StringWriter();

            for (var i = 0; i < _cachedMatrix.Count; i++)
            {
                if (_cachedMatrix[i].Length == 0)
                {
                    await writer.WriteLineAsync();
                }
                else
                {
                    var array = _cachedMatrix[i].RemoveDublicates();

                    await writer.WriteLineAsync(String.Join(",", array));
                }
            }

            File.WriteAllText(outputFile, writer.ToString(), Encoding.UTF8);
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _cachedMatrix.Clear();
        }

        #endregion
    }
}
