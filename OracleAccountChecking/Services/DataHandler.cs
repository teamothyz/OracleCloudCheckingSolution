using Newtonsoft.Json;
using OracleAccountChecking.Models;

namespace OracleAccountChecking.Services
{
    public class DataHandler
    {
        private static readonly object lockData = new();
        private static readonly object lockProxy = new();
        private static readonly object lockSuccess = new();
        private static readonly object lockFailed = new();
        private static readonly object lockError = new();
        private static readonly object lockLogs = new();
        private static readonly object lockInfo = new();

        public static void WriteLastInfo(int scanned, string fileName)
        {
            lock (lockInfo)
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                try
                {
                    var folder = Path.Combine(basePath, "last");
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                    var filePath = Path.Combine(folder, $"{fileName}_{scanned}_{DateTime.Now:HHmmss}_{DateTime.Now:ddMMyyyy}");
                    using var stream = File.Create(filePath);
                    stream.Close();
                }
                catch { }
            }
        }

        public static Queue<Account> ReadDataFile(string path)
        {
            lock (lockData)
            {
                var data = new Queue<Account>();
                try
                {
                    using var reader = new StreamReader(path);
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        try
                        {
                            var info = line.Split(":");
                            var acc = new Account
                            {
                                Email = info[0],
                                Password = info[1]
                            };
                            data.Enqueue(acc);
                        }
                        catch { }
                        line = reader.ReadLine();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    WriteLog(ex);
                }
                return data;
            }
        }

        public static List<string> ReadProxyFile(string path)
        {
            lock (lockProxy)
            {
                var data = new List<string>();
                try
                {
                    using var reader = new StreamReader(path);
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var detail = line.Split(":");
                        if (detail.Length != 2 && detail.Length != 4) continue;
                        else data.Add(line);

                        line = reader.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    WriteLog(ex);
                }
                return data;
            }
        }

        public static void WriteSuccessData(Account acc, List<string> bills, bool is2FA = false)
        {
            lock (lockSuccess)
            {
                try
                {
                    var postFix = is2FA ? "_2FA" : "";
                    var basePath = AppDomain.CurrentDomain.BaseDirectory;
                    var directoryPath = $"{basePath}/output";
                    var subDirectoryPath = $"{basePath}/output/success";

                    var fileName = $"{DateTime.Now:ddMMyyyy}success{postFix}.txt";
                    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                    if (!Directory.Exists(subDirectoryPath)) Directory.CreateDirectory(subDirectoryPath);

                    using var writer = new StreamWriter($"{subDirectoryPath}/{fileName}", true);
                    var data = $"{acc.Email}:{acc.Password}:{string.Join(":", bills)}";
                    writer.WriteLine(data);
                    writer.Flush();
                    writer.Close();
                }
                catch (Exception ex)
                {
                    WriteLog(ex);
                }
            }
        }

        public static void WriteErrorData(Account acc, string reason)
        {
            lock (lockError)
            {
                try
                {
                    var basePath = AppDomain.CurrentDomain.BaseDirectory;
                    var directoryPath = $"{basePath}/output";
                    var subDirectoryPath = $"{basePath}/output/error";

                    var fileName = $"{DateTime.Now:ddMMyyyy}error.txt";
                    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                    if (!Directory.Exists(subDirectoryPath)) Directory.CreateDirectory(subDirectoryPath);

                    using var writer = new StreamWriter($"{subDirectoryPath}/{fileName}", true);
                    var data = $"{acc.Email}:{acc.Password}:{reason}";
                    writer.WriteLine(data);
                    writer.Flush();
                    writer.Close();
                }
                catch (Exception ex)
                {
                    WriteLog(ex);
                }
            }
        }

        public static void WriteFailedData(Account acc, string reason)
        {
            lock (lockFailed)
            {
                try
                {
                    var basePath = AppDomain.CurrentDomain.BaseDirectory;
                    var directoryPath = $"{basePath}/output";
                    var subDirectoryPath = $"{basePath}/output/failed";

                    var fileName = $"{DateTime.Now:ddMMyyyy}failed.txt";
                    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                    if (!Directory.Exists(subDirectoryPath)) Directory.CreateDirectory(subDirectoryPath);

                    using var writer = new StreamWriter($"{subDirectoryPath}/{fileName}", true);
                    var data = $"{acc.Email}:{acc.Password}:{reason}";
                    writer.WriteLine(data);
                    writer.Flush();
                    writer.Close();
                }
                catch (Exception ex)
                {
                    WriteLog(ex);
                }
            }
        }

        public static void WriteLog(Exception ex)
        {
            lock (lockLogs)
            {
                WriteSimpleLog(ex.Message);
                WriteDetailsLog(ex.ToString());
            }
        }

        private static void WriteSimpleLog(string data)
        {
            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var directoryPath = $"{basePath}/logs";
                var fileName = $"{DateTime.Now:ddMMyyyy}simplelog.txt";
                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                using var writer = new StreamWriter($"{directoryPath}/{fileName}", true);
                writer.WriteLine(data);
                writer.Flush();
                writer.Close();
            }
            catch { }
        }

        private static void WriteDetailsLog(string data)
        {
            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var directoryPath = $"{basePath}/logs";
                var fileName = $"{DateTime.Now:ddMMyyyy}log.txt";
                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                using var writer = new StreamWriter($"{directoryPath}/{fileName}", true);
                writer.WriteLine(data);
                writer.Flush();
                writer.Close();
            }
            catch { }
        }
    }
}
