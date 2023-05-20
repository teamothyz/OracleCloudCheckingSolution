using ChromeDriverLibrary;
using OracleAccountChecking.Models;
using OracleAccountChecking.Services;
using SeleniumUndetectedChromeDriver;

namespace OracleAccountChecking
{
    public partial class FrmMain : Form
    {
        private int TotalThreads = 20;
        private Queue<Account> Accounts;
        private List<string> Proxies;
        private readonly CountModel CountModel;
        private CancellationTokenSource CancelToken;

        private int proxyIndex = 0;
        private static readonly object lockRun = new();

        public FrmMain()
        {
            InitializeComponent();

            Accounts = new();
            Proxies = new();
            CountModel = new();
            CancelToken = new();

            TotalTextBox.DataBindings.Add("Text", CountModel, "Total");
            SuccessTextBox.DataBindings.Add("Text", CountModel, "Success");
            FailedTextBox.DataBindings.Add("Text", CountModel, "Failed");
            RemainTextBox.DataBindings.Add("Text", CountModel, "Remaining");

            ActiveControl = TotalTextBox;
        }

        private async void StartBtn_Click(object sender, EventArgs e)
        {
            ActiveControl = TotalTextBox;
            var currentTitle = Text;
            if (Proxies.Count == 0 || Accounts.Count == 0)
            {
                MessageBox.Show("Vui lòng nhập dữ liệu", "Cảnh báo");
                return;
            }
            EnableBtn(true);
            try
            {
                Invoke(() =>
                {
                    Text = $"{currentTitle} - Running...!";
                    MessageBox.Show("Chương trình bắt đầu", "Thông báo");
                });

                proxyIndex = 0;
                var tasks = new List<Task>();
                for (var i = 0; i < TotalThreads; i++)
                {
                    var token = CancelToken.Token;
                    var task = Task.Run(async () => await RunThread(token));
                    tasks.Add(task);
                }
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                DataHandler.WriteLog(ex);
            }
            finally
            {
                Invoke(() =>
                {
                    Text = currentTitle;
                    MessageBox.Show("Chương trình đã dừng lại", "Thông báo");
                });
                EnableBtn(false);
            }
        }

        private async Task RunThread(CancellationToken token)
        {
            try
            {
                var proxy = string.Empty;
                Account? account = null;
                var completed = true;

                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        if (completed == false && account != null)
                        {
                            DataHandler.WriteFailedData(account, "cancel scan");
                            lock (CountModel)
                            {
                                CountModel.Failed++;
                                CountModel.Remaining--;
                            }
                        }
                        return;
                    }
                    lock (lockRun)
                    {
                        if (completed)
                        {
                            if (Accounts.Count > 0)
                            {
                                account = Accounts.Dequeue();
                                if (proxyIndex >= Proxies.Count) proxyIndex = 0;
                                proxy = Proxies[proxyIndex];
                                proxyIndex++;
                                completed = false;
                            }
                            else return;
                        }
                    }
                    if (account == null || string.IsNullOrEmpty(proxy)) return;

                    var driver = await Task.Run(() => ChromeDriverInstance.GetInstance(proxy, true, token), token);
                    if (driver == null) throw new Exception("can't create chrome driver");
                    completed = true;

                    await CheckAccount(account, driver, token);
                    try { driver?.Quit(); } catch { }

                    await Task.Delay(3000, token).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                DataHandler.WriteLog(ex);
            }
        }

        private async Task CheckAccount(Account account, UndetectedChromeDriver driver, CancellationToken token)
        {
            try
            {
                var accountName = account.Email.Split("@")[0];

                var tenantRs = await WebDriverService.EnterTenant(driver, accountName, token);
                if (!tenantRs.Item1)
                {
                    DataHandler.WriteFailedData(account, tenantRs.Item2);
                    lock (CountModel)
                    {
                        CountModel.Failed++;
                        CountModel.Remaining--;
                    }
                    return;
                }

                var loginRs = await WebDriverService.Login(driver, account.Email, account.Password, token);
                if (!loginRs.Item1)
                {
                    DataHandler.WriteFailedData(account, loginRs.Item2);
                    lock (CountModel)
                    {
                        CountModel.Failed++;
                        CountModel.Remaining--;
                    }
                    return;
                }

                var bills = await WebDriverService.GetBilling(driver, token);
                DataHandler.WriteSuccessData(account, bills);
                lock (CountModel)
                {
                    CountModel.Success++;
                    CountModel.Remaining--;
                }
            }
            catch (Exception ex)
            {
                lock (CountModel)
                {
                    CountModel.Failed++;
                    CountModel.Remaining--;
                }
                DataHandler.WriteLog(ex);
                DataHandler.WriteFailedData(account, ex.Message);
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            ActiveControl = TotalTextBox;
            CancelToken.Cancel();
        }

        private void EnableBtn(bool isRun)
        {
            Invoke(() =>
            {
                StartBtn.Enabled = !isRun;
                DataBtn.Enabled = !isRun;
                ProxyBtn.Enabled = !isRun;
                ThreadInput.Enabled = !isRun;

                StopBtn.Enabled = isRun;
            });
        }

        private async void DataBtn_Click(object sender, EventArgs e)
        {
            ActiveControl = TotalTextBox;
            var dialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open data file"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Accounts = await Task.Run(() => DataHandler.ReadDataFile(dialog.FileName)).ConfigureAwait(false);
                Invoke(() =>
                {
                    CountModel.Total = Accounts.Count;
                    CountModel.Success = 0;
                    CountModel.Failed = 0;
                    CountModel.Remaining = Accounts.Count;
                    MessageBox.Show("Đọc dữ liệu xong", "Thông báo");
                });
            }
        }

        private async void ProxyBtn_Click(object sender, EventArgs e)
        {
            ActiveControl = TotalTextBox;
            var dialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open proxy file"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Proxies = await Task.Run(() => DataHandler.ReadProxyFile(dialog.FileName)).ConfigureAwait(false);
                Invoke(() =>
                {
                    ProxyTextBox.Text = Proxies.Count.ToString();
                    MessageBox.Show("Đọc danh sách proxy xong", "Thông báo");
                });
            }
        }

        private void ThreadInput_ValueChanged(object sender, EventArgs e)
        {
            TotalThreads = (int)ThreadInput.Value;
        }
    }
}
