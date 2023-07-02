using ChromeDriverLibrary;
using OracleAccountChecking.Models;
using OracleAccountChecking.Services;
using SeleniumUndetectedChromeDriver;

namespace OracleAccountChecking
{
    public partial class FrmMain : Form
    {
        public static bool ClickCookie { get; private set; } = false;

        private int TotalThreads = 20;
        private Queue<Account> Accounts;
        private List<string> Proxies;
        private readonly CountModel CountModel;
        private CancellationTokenSource CancelToken;
        private CancellationTokenSource ForceCancelToken;

        private bool IsHeadless = true;
        private bool UseProxy = false;
        private bool PrivateMode = false;
        private bool DisableImg = true;

        private string ProxyPrefix = string.Empty;

        private int proxyIndex = 0;
        private static readonly object lockRun = new();

        private readonly int HeightCount;
        private readonly int WidthCount;

        private readonly List<string> ExtensionPaths;

        private string LastFileName;

        public FrmMain()
        {
            InitializeComponent();

            Accounts = new();
            Proxies = new();
            CountModel = new();
            CancelToken = new();
            ForceCancelToken = new();
            ExtensionPaths = new();

            TotalTextBox.DataBindings.Add("Text", CountModel, "Total");
            SuccessTextBox.DataBindings.Add("Text", CountModel, "Success");
            FailedTextBox.DataBindings.Add("Text", CountModel, "Failed");
            ScannedTextBox.DataBindings.Add("Text", CountModel, "Scanned");

            ActiveControl = TotalTextBox;

            HeightCount = Screen.PrimaryScreen.Bounds.Height / 500;
            WidthCount = Screen.PrimaryScreen.Bounds.Width / 375;
            LastFileName = string.Empty;
        }

        private async void StartBtn_Click(object sender, EventArgs e)
        {
            ActiveControl = TotalTextBox;
            if (Accounts.Count == 0)
            {
                MessageBox.Show(this, "Vui lòng nhập dữ liệu", "Cảnh báo");
                return;
            }
            if (UseProxy && Proxies.Count == 0)
            {
                MessageBox.Show(this, "Vui lòng nhập proxy", "Cảnh báo");
                return;
            }
            EnableBtn(true);
            var needDeleted = (int)StartPointInput.Value;
            try
            {
                Invoke(() =>
                {
                    StatusTextBox.StateCommon.Back.Color1 = Color.FromArgb(0, 192, 0);
                    StatusTextBox.Text = "Đang chạy";
                    MessageBox.Show(this, "Chương trình bắt đầu", "Thông báo");
                });

                proxyIndex = 0;
                var tasks = new List<Task>();
                CancelToken = new();
                ForceCancelToken = new();
                var totalThread = Accounts.Count > TotalThreads ? TotalThreads : Accounts.Count;

                var forceToken = ForceCancelToken.Token;
                await Task.Run(() =>
                {
                    for (var i = 0; i < needDeleted; i++)
                    {
                        _ = Accounts.Dequeue();
                    }
                    Invoke(() => CountModel.Scanned += needDeleted);
                });

                while (Accounts.Count > 0)
                {
                    if (CancelToken.IsCancellationRequested) break;
                    for (var i = 0; i < totalThread; i++)
                    {
                        if (CancelToken.IsCancellationRequested) break;
                        var token = CancelToken.Token;
                        var index = i;
                        var task = Task.Run(async () => await RunThread(index, token), forceToken);
                        tasks.Add(task);
                    }
                    await Task.WhenAll(tasks);
                    tasks.Clear();
                    ChromeDriverInstance.ForceKillAll();
                }
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                DataHandler.WriteLog(ex);
            }
            finally
            {
                await Task.Run(() => DataHandler.WriteLastInfo(CountModel.Scanned, LastFileName));
                Invoke(() =>
                {
                    StartPointInput.Value = 0;
                    StatusTextBox.StateCommon.Back.Color1 = Color.FromArgb(192, 0, 0);
                    StatusTextBox.Text = "Đã dừng";
                    MessageBox.Show(this, "Chương trình đã dừng lại", "Thông báo");
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

                    var driverInfo = await Task.Run(() => ChromeDriverInstance.GetInstance(positionX: positionX, positionY: positionY,
                        proxy: proxy, isHeadless: IsHeadless, extensionPaths: ExtensionPaths,
                        disableImg: DisableImg, privateMode: PrivateMode, token: token), token);

                    driver = driverInfo.Item1;
                    if (driver == null)
                    {
                        DataHandler.WriteLog(new Exception("can't create chrome driver"));
                        await Task.Delay(3000, token).ConfigureAwait(false);
                        continue;
                    }
                    await CheckAccount(account, driver, driverInfo.Item2, token);
                    completed = true;

                    await ChromeDriverInstance.Close(driver, driverInfo.Item2).ConfigureAwait(false);
                    return;
                }
            }
            catch (Exception ex)
            {
                DataHandler.WriteLog(ex);
                if (completed == false && account != null)
                {
                    DataHandler.WriteErrorData(account, "failed caused by cancel scan");
                    lock (CountModel)
                    {
                        Invoke(() =>
                        {
                            CountModel.Failed++;
                            CountModel.Scanned++;
                        });
                    }
                }
            }
        }

        private async Task CheckAccount(Account account, UndetectedChromeDriver driver, string userDir, CancellationToken token)
        {
            try
            {
                var bills = new List<string>();
                var accountName = account.Email.Split("@")[0];
                var checkTenant = await WebDriverService.CheckTenant(driver, accountName, token);
                if (!checkTenant)
                {
                    DataHandler.WriteErrorData(account, "check tenant failed");
                    lock (CountModel)
                    {
                        Invoke(() =>
                        {
                            CountModel.Failed++;
                            CountModel.Scanned++;
                        });
                    }
                    return;
                }

                var tenantRs = await WebDriverService.EnterTenant(driver, account.Email, account.Password, token);
                if (tenantRs.Item2.Contains("[login]"))
                {
                    if (!tenantRs.Item1)
                    {
                        DataHandler.WriteFailedData(account, tenantRs.Item2);
                        lock (CountModel)
                        {
                            Invoke(() =>
                            {
                                CountModel.Failed++;
                                CountModel.Scanned++;
                            });
                        }
                    }
                    else
                    {
                        bills = await WebDriverService.GetBilling(driver, token);
                        DataHandler.WriteSuccessData(account, bills);
                        lock (CountModel)
                        {
                            Invoke(() =>
                            {
                                CountModel.Success++;
                                CountModel.Scanned++;
                            });
                        }
                    }
                    return;
                }
                else if (!tenantRs.Item1)
                {
                    DataHandler.WriteErrorData(account, tenantRs.Item2);
                    lock (CountModel)
                    {
                        Invoke(() =>
                        {
                            CountModel.Failed++;
                            CountModel.Scanned++;
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
                            CountModel.Scanned++;
                        });
                    }
                    return;
                }
                else if (loginRs.Item1 && loginRs.Item2 == "pwdmustchange")
                {
                    DataHandler.WriteSuccessData(account, new List<string> { "pwdmustchange" });
                    lock (CountModel)
                    {
                        Invoke(() =>
                        {
                            CountModel.Success++;
                            CountModel.Scanned++;
                        });
                    }
                    return;
                }

                bills = await WebDriverService.GetBilling(driver, token);
                DataHandler.WriteSuccessData(account, bills);
                lock (CountModel)
                {
                    Invoke(() =>
                    {
                        CountModel.Success++;
                        CountModel.Scanned++;
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
                        CountModel.Scanned++;
                    });
                }
                DataHandler.WriteLog(ex);
                DataHandler.WriteErrorData(account, ex.Message);
            }
            finally { await ChromeDriverInstance.Close(driver, userDir).ConfigureAwait(false); }
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
                TimeoutInput.Enabled = !isRun;
                ClearExtensionsBtn.Enabled = !isRun;
                ExtensionBtn.Enabled = !isRun;
                LoadExtensionBtn.Enabled = !isRun;
                StartPointInput.Enabled = !isRun;
                PrivateModeBtn.Enabled = !isRun;
                DisableImgBtn.Enabled = !isRun;
                ClickCookieCheckBox.Enabled = !isRun;

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
                LastFileName = Path.GetFileName(dialog.FileName);
                Invoke(() =>
                {
                    StartPointInput.Value = 0;
                    CountModel.Total = Accounts.Count;
                    CountModel.Success = 0;
                    CountModel.Failed = 0;
                    CountModel.Scanned = 0;
                    MessageBox.Show(this, "Đọc dữ liệu xong", "Thông báo");
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
                    MessageBox.Show(this, "Đọc danh sách proxy xong", "Thông báo");
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

        private void TopMostBtn_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = TopMostBtn.Checked;
        }

        private void ForceStopBtn_Click(object sender, EventArgs e)
        {
            ActiveControl = TotalTextBox;
            ForceCancelToken.Cancel();
            StopBtn_Click(sender, e);
            ChromeDriverInstance.ForceKillAll();
        }

        private void TimeoutInput_ValueChanged(object sender, EventArgs e)
        {
            WebDriverService.DefaultTimeout = (int)TimeoutInput.Value;
        }

        private void ExtensionBtn_Click(object sender, EventArgs e)
        {
            ActiveControl = TotalTextBox;
            using var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ExtensionPaths.Clear();
                ExtensionPaths.AddRange(Directory.GetDirectories(folderBrowserDialog.SelectedPath));
                Invoke(() => ExtensionsTextBox.Text = ExtensionPaths.Count.ToString());
            }
        }

        private void ClearExtensionsBtn_Click(object sender, EventArgs e)
        {
            ActiveControl = TotalTextBox;
            ExtensionPaths.Clear();
            Invoke(() => ExtensionsTextBox.Text = ExtensionPaths.Count.ToString());
        }

        private void LoadExtensionBtn_Click(object sender, EventArgs e)
        {
            ActiveControl = TotalTextBox;
            ExtensionPaths.Clear();
            var basePath = $"{AppDomain.CurrentDomain.BaseDirectory}/extensions";
            if (Directory.Exists(basePath)) ExtensionPaths.AddRange(Directory.GetDirectories(basePath));
            Invoke(() => ExtensionsTextBox.Text = ExtensionPaths.Count.ToString());
        }

        private void PrivateModeBtn_CheckedChanged(object sender, EventArgs e)
        {
            PrivateMode = PrivateModeBtn.Checked;
        }

        private void DisableImgBtn_CheckedChanged(object sender, EventArgs e)
        {
            DisableImg = DisableImgBtn.Checked;
        }

        private void ClickCookieCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ClickCookie = ClickCookieCheckBox.Checked;
        }
    }
}
