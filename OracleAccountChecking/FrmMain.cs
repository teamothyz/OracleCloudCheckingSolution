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

        private bool IsHeadless = true;
        private bool UseProxy = false;
        private string ProxyPrefix = string.Empty;

        private int proxyIndex = 0;
        private static readonly object lockRun = new();

        private readonly int HeightCount;
        private readonly int WidthCount;

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

            HeightCount = Screen.PrimaryScreen.Bounds.Height / 500;
            WidthCount = Screen.PrimaryScreen.Bounds.Width / 375;
        }

        private async void StartBtn_Click(object sender, EventArgs e)
        {
            ActiveControl = TotalTextBox;
            var currentTitle = Text;
            if (Accounts.Count == 0)
            {
                MessageBox.Show("Vui lòng nhập dữ liệu", "Cảnh báo");
                return;
            }
            if (UseProxy && Proxies.Count == 0)
            {
                MessageBox.Show("Vui lòng nhập proxy", "Cảnh báo");
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
                CancelToken = new();
                var totalThread = Accounts.Count > TotalThreads ? TotalThreads : Accounts.Count;

                for (var i = 0; i < totalThread; i++)
                {
                    var token = CancelToken.Token;
                    var index = i;
                    var task = Task.Run(async () => await RunThread(index, token));
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

        private async Task RunThread(int index, CancellationToken token)
        {
            index %= (WidthCount * HeightCount + 1);
            var positionX = (index % WidthCount) * 375;
            var positionY = (index / WidthCount) * 500;

            var proxy = string.Empty;
            Account? account = null;
            var completed = true;
            UndetectedChromeDriver? driver = null;

            try
            {
                while (true)
                {
                    if (token.IsCancellationRequested) return;

                    lock (lockRun)
                    {
                        if (completed)
                        {
                            if (Accounts.Count > 0)
                            {
                                account = Accounts.Dequeue();
                                if (UseProxy)
                                {
                                    if (proxyIndex >= Proxies.Count) proxyIndex = 0;
                                    proxy = ProxyPrefix + Proxies[proxyIndex];
                                    proxyIndex++;
                                }
                                completed = false;
                            }
                            else return;
                        }
                    }
                    if (account == null) return;

                    driver = await Task.Run(() => ChromeDriverInstance.GetInstance(positionX: positionX, positionY: positionY,
                        proxy: proxy, isHeadless: IsHeadless, token), token);
                    if (driver == null)
                    {
                        DataHandler.WriteLog(new Exception("can't create chrome driver"));
                        await Task.Delay(3000, token).ConfigureAwait(false);
                        continue;
                    }
                    await CheckAccount(account, driver, token);
                    completed = true;
                    try { driver?.Quit(); } catch { }

                    await Task.Delay(3000, token).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                DataHandler.WriteLog(ex);
                if (completed == false && account != null)
                {
                    DataHandler.WriteFailedData(account, "cancel scan");
                    lock (CountModel)
                    {
                        Invoke(() =>
                        {
                            CountModel.Failed++;
                            CountModel.Remaining--;
                        });
                    }
                }
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
                        Invoke(() =>
                        {
                            CountModel.Failed++;
                            CountModel.Remaining--;
                        });
                    }
                    return;
                }

                var loginRs = await WebDriverService.Login(driver, account.Email, account.Password, token);
                if (!loginRs.Item1)
                {
                    DataHandler.WriteFailedData(account, loginRs.Item2);
                    lock (CountModel)
                    {
                        Invoke(() =>
                        {
                            CountModel.Failed++;
                            CountModel.Remaining--;
                        });
                    }
                    return;
                }

                var bills = await WebDriverService.GetBilling(driver, token);
                DataHandler.WriteSuccessData(account, bills);
                lock (CountModel)
                {
                    Invoke(() =>
                    {
                        CountModel.Success++;
                        CountModel.Remaining--;
                    });
                }
            }
            catch (Exception ex)
            {
                lock (CountModel)
                {
                    Invoke(() =>
                    {
                        CountModel.Failed++;
                        CountModel.Remaining--;
                    });
                }
                DataHandler.WriteLog(ex);
                DataHandler.WriteFailedData(account, ex.Message);
            }
            finally
            {
                try { driver?.Quit(); } catch { }
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
                HideChromeCheckBox.Enabled = !isRun;
                ProxyLessRadioBtn.Enabled = !isRun;
                HttpRadioBtn.Enabled = !isRun;
                Socks5RadioBtn.Enabled = !isRun;

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

        private void HideChromeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsHeadless = HideChromeCheckBox.Checked;
        }

        private void ProxyLessRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            UseProxy = false;
            ProxyPrefix = string.Empty;
        }

        private void HttpRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            UseProxy = true;
            ProxyPrefix = "http://";
        }

        private void Socks5RadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            UseProxy = true;
            ProxyPrefix = "socks5://";
        }
    }
}
