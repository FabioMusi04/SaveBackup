using SaveBackup.Classi;
using Velopack;

namespace SaveBackup;

public partial class Form1 : Form
{
    private string savePath = string.Empty;
    private readonly BackupManager backupManager;

    public Form1()
    {
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        InitializeComponent();

        toolTip.SetToolTip(linkLblSalvataggioEsterno, "Clicca per aprire la cartella dei backup");
        toolTip.SetToolTip(linkLblCaricamentoEsterno, "Clicca per aprire la cartella del salvataggio del gioco");
        toolTip.SetToolTip(BtnCaricaSalvataggio, "Carica il salvataggio selezionato (Da backup al salvataggio fisico");
        toolTip.SetToolTip(BtnSalvaSalvataggio, "Salva il salvataggio fisico (Da salvataggio fisico a backup)");

        CmbSalvataggi.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        BtnCaricaSalvataggio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        BtnSalvaSalvataggio.Anchor = AnchorStyles.Top | AnchorStyles.Left;

        string backupPath = Path.Combine(Application.StartupPath, "Backup");
        backupManager = new BackupManager(backupPath);

        UpdateDisplayedPaths();
        LoadBackupList();
    }

    private static async Task UpdateMyApp()
    {
        var mgr = new UpdateManager("https://github.com/FabioMusi04/SaveBackup/releases/latest/download/");

        var newVersion = await mgr.CheckForUpdatesAsync();
        if (newVersion == null)
            return;

        await mgr.DownloadUpdatesAsync(newVersion);

        mgr.ApplyUpdatesAndRestart(newVersion);
    }

    private void LoadBackupList()
    {
        CmbSalvataggi.Items.Clear();
        CmbSalvataggi.Items.Add("Seleziona un salvataggio...");

        var backups = backupManager.GetAvailableBackups();
        foreach (var backup in backups)
        {
            CmbSalvataggi.Items.Add(backup);
        }

        CmbSalvataggi.SelectedIndex = backups.Count != 0 ? 1 : 0;
    }

    private void UpdateDisplayedPaths()
    {
        linkLblSalvataggioEsterno.Text = $"Il percorso dei backup è: {backupManager.BackupRootPath}";
        linkLblCaricamentoEsterno.Text = $"Il percorso del salvataggio del gioco è: {(string.IsNullOrEmpty(savePath) ? "-" : savePath)}";
    }

    protected override async void OnShown(EventArgs e)
    {
        base.OnShown(e);

        try
        {
            await UpdateMyApp();
        }
        catch (Exception ex)
        {
            // Optional: log or show message, but silent is usually fine
            Console.WriteLine($"Update check failed: {ex.Message}");
        }
    }


    private async void BtnSalvaSalvataggio_Click(object sender, EventArgs e)
    {
        SetButtonsEnabled(false);
        try
        {
            if (!Directory.Exists(savePath))
            {
                using var dialog = new FolderBrowserDialog { Description = "Seleziona la cartella dei salvataggi del gioco..." };
                if (dialog.ShowDialog() == DialogResult.OK)
                    savePath = dialog.SelectedPath;
                else
                    return;
            }

            using var loadingForm = new LoadingForm("Sto salvando il backup...");
            loadingForm.Show();
            loadingForm.Refresh();

            await Task.Run(() =>
            {
                backupManager.CreateBackup(savePath);
            });

            loadingForm.Close();
            LoadBackupList();
            MessageBox.Show("Salvataggio caricato nel backup!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Errore nel salvataggio: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetButtonsEnabled(true);
            UpdateDisplayedPaths();
        }
    }


    private async void BtnCaricaSalvataggio_Click(object sender, EventArgs e)
    {
        SetButtonsEnabled(false);

        try
        {
            if (CmbSalvataggi.SelectedIndex < 1) return;

            if (string.IsNullOrEmpty(savePath))
            {
                MessageBox.Show("Percorso del salvataggio non valido.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Directory.CreateDirectory(savePath);
            string selectedBackup = CmbSalvataggi.SelectedItem!.ToString()!;


            using var loadingForm = new LoadingForm("Sto caricando il backup...");
            loadingForm.Show();
            loadingForm.Refresh();

            await Task.Run(() =>
            {
                backupManager.RestoreBackup(selectedBackup, savePath);
            });

            loadingForm.Close();

            MessageBox.Show("Salvataggio ripristinato dal backup!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Errore nel caricamento: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetButtonsEnabled(true);
        }
    }

    private void LinkLblSalvataggioEsterno_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        OpenFolder(backupManager.BackupRootPath);
    }

    private void LinkLblCaricamentoEsterno_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        OpenFolder(savePath);
    }

    private static void OpenFolder(string path)
    {
        try
        {
            if (Directory.Exists(path))
                System.Diagnostics.Process.Start("explorer.exe", path);
            else
                MessageBox.Show("Cartella non trovata.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Errore nell'apertura: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CmbSalvataggi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbSalvataggi.SelectedIndex <= 0)
        {
            savePath = string.Empty;
            UpdateDisplayedPaths();
            return;
        }

        string selectedBackup = CmbSalvataggi.SelectedItem!.ToString()!;
        var info = backupManager.GetBackupInfo(selectedBackup);

        if (info != null && !string.IsNullOrEmpty(info.OriginalPath))
            savePath = info.OriginalPath;
        else
            MessageBox.Show("info.json non trovato o non valido.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        UpdateDisplayedPaths();
    }

    private void SetButtonsEnabled(bool enabled)
    {
        BtnCaricaSalvataggio.Enabled = enabled;
        BtnSalvaSalvataggio.Enabled = enabled;
        CmbSalvataggi.Enabled = enabled;
    }
}