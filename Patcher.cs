using System.IO;
using System.Net.Http;

namespace Patcher;

    public class Patcher
    {
        public async Task PatchFileAsync()
        {
            using var folderBrowser = new FolderBrowserDialog();
            {
                folderBrowser.Description =
                    @"Select your EFGame folder. This should be where your Lost Ark is installed, usually in C:\Program Files (x86)\Steam\steamapps\common\Lost Ark\EFGame";
                if (folderBrowser.ShowDialog() != DialogResult.OK)
                    return;
                var folder = folderBrowser.SelectedPath;

                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync(
                            "https://raw.githubusercontent.com/Poyoanon/loa-elixir-patcher/master/data1.lpk"
                        );
                        response.EnsureSuccessStatusCode();
                        var fileBytes = await response.Content.ReadAsByteArrayAsync();

                        var backupPath = Path.Combine(folder, "data1backup.lpk");
                        File.Copy(Path.Combine(folder, "data1.lpk"), backupPath, true);

                        File.WriteAllBytes(Path.Combine(folder, "data1.lpk"), fileBytes);

                        MessageBox.Show(
                            "Patching successful! Your original data1.lpk has been saved as data1backup.lpk.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show(
                        "Error downloading file: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }
    }

