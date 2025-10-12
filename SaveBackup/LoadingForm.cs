namespace SaveBackup
{
    public partial class LoadingForm : Form
    {
        public LoadingForm(string message)
        {
            InitializeComponent();
            ControlBox = false;
            Text = "Attendere...";
            label1.Text = message;
            StartPosition = FormStartPosition.CenterParent;
        }
    }
}
