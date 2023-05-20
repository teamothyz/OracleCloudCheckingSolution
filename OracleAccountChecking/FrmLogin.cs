using Newtonsoft.Json;

namespace OracleAccountChecking
{
    public partial class FrmLogin : Form
    {
        private readonly string Key = "gCjK+DZ/hazZzhotdogzZz==";
        private readonly string IV = "47l5QsSeaczZzhotdogzZz==";
        private bool IsSigning = false;
        private static readonly object LoginLock = new();

        public FrmLogin()
        {
            InitializeComponent();
            var user = User.GetSavedUser();
            if (user != null)
            {
                UsernameTextBox.Text = user.UserName ?? "";
                PasswordTextBox.Text = user.Password ?? "";
            }

            ActiveControl = UsernameTextBox;
        }

        private async void SignInBtn_Click(object sender, EventArgs e)
        {
            ActiveControl = UsernameTextBox;
            lock (LoginLock)
            {
                if (IsSigning) return;
                IsSigning = true;
            }
            try
            {
                var username = UsernameTextBox.Text;
                var password = PasswordTextBox.Text;
                var loginString = $"{username}ORACLE{password}";
                var aes = new AES(Key, IV);
                var encryptString = aes.EncryptToBase64String(loginString);

                var client = new HttpClient()
                {
                    Timeout = TimeSpan.FromSeconds(30)
                };
                var url = "https://docs.google.com/spreadsheets/d/1KO9GKCoMhX6s95RyruXVuIPeQMel8Uf-06_GWkX151U";
                var res = await client.GetStringAsync(url);
                if (string.IsNullOrWhiteSpace(encryptString) || !res.Contains(encryptString))
                {
                    var text = "Tài khoản hoặc mật khẩu không chính xác";
                    var cap = "Đăng nhập thất bại";
                    MessageBox.Show(text, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var user = new User()
                {
                    UserName = username,
                    Password = password
                };
                User.SavedUser(user);
                Hide();
                new FrmMain().ShowDialog();
                Application.Exit();
            }
            catch
            {
                var text = "Ứng dụng gặp sự cố. Hãy kiểm tra lại và thử lại sau!";
                var cap = "Lỗi không xác định";
                MessageBox.Show(text, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { IsSigning = false; }
        }
    }

    public class User
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public static User? GetSavedUser()
        {
            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                using var reader = new StreamReader($"{basePath}/user");
                var userRaw = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<User>(userRaw);
            }
            catch { return null; }
        }

        public static void SavedUser(User user)
        {
            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                using var writer = new StreamWriter($"{basePath}/user", false);
                writer.Write(JsonConvert.SerializeObject(user));
                writer.Flush();
                writer.Close();
            }
            catch { }
        }
    }
}
