namespace Logger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            done_label.Text = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите текстовый файл";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                Logger logger = new Logger();
                await logger.ProcessLogsAsync(filePath);

                done_label.Text = "Done!";
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string logsDirectory = Path.Combine(desktopPath, "Logs");
                label1.Text = $"See file in {logsDirectory}";
            }

        }
    }
}
