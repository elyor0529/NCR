using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using NCR.Library.Extensions;

namespace NCR.Library.Utils
{
    public sealed class IOParser : IDisposable
    {
        private readonly string _inputFile;
        private readonly IList<int[]> _cachedMatrix = new List<int[]>();
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static IOParser()
        {
            XmlConfigurator.Configure(new FileInfo(Environment.CurrentDirectory + "\\log4net.config"));
        }

        public IOParser(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<bool> ReadToEnd()
        {
            try
            {
                var watcher = new Stopwatch();
                watcher.Start();
                var txt = File.ReadAllText(_inputFile, Encoding.UTF8);
                var reader = new StringReader(txt);
                var line = "";
                
                while (!String.IsNullOrWhiteSpace(line = await reader.ReadLineAsync()))
                {
                    var splittedNumbers = line.Split(new string[] { ",", "-" }, StringSplitOptions.RemoveEmptyEntries);

                    if (splittedNumbers.Length == 0)
                        _cachedMatrix.Add(new int[] { });
                    else
                    {
                        try
                        {
                            var arrray = splittedNumbers.Select(s => int.Parse(s)).ToArray();

                            _cachedMatrix.Add(arrray);

                        }
                        catch (Exception exception)
                        {
                            Logger.Warn(exception);
                        }
                    }
                }
                watcher.Stop();
                Logger.InfoFormat("Readed: {0:g}", watcher.Elapsed);

                return await Task.FromResult(true);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }

            return await Task.FromResult(false);
        }

        public async Task WriteToEnd(string outputFile)
        {
            var writer = new StringWriter();
            var watcher = new Stopwatch();
            watcher.Start();

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
            watcher.Stop();
            
            Logger.InfoFormat("Writed: {0:g}", watcher.Elapsed);
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _cachedMatrix.Clear();
        }

        #endregion
    }
}
