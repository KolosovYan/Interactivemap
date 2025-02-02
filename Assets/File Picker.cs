using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using System.IO;

public class FilePicker : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private MarkerCreationWindow mkw;

    private void Start()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        FileBrowser.SetDefaultFilter(".jpg");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        FileBrowser.SingleClickMode = true;
    }

    public void OpenFilePicker()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, true, null, null, "Select Files", "Load");

        // Dialog is closed
        // Print whether the user has selected some files or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
            OnFilesSelected(FileBrowser.Result); // FileBrowser.Result is null, if FileBrowser.Success is false
    }

    public static Sprite GetSpriteFromPath(string filePath)
    {
        byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        Sprite spr = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        return spr;
    }

    public static void DeleteSpriteFromPath(string filePath)
    {
        FileBrowserHelpers.DeleteFile(filePath);
    }

    void OnFilesSelected(string[] filePaths)
    {
        // Get the file path of the first selected file
        string filePath = filePaths[0];

        // Read the bytes of the first file via FileBrowserHelpers
        // Contrary to File.ReadAllBytes, this function works on Android 10+, as well

        // Or, copy the first file to persistentDataPath
        string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(filePath));
        FileBrowserHelpers.CopyFile(filePath, destinationPath);

        mkw.SetSelectedImage(GetSpriteFromPath(destinationPath), destinationPath);
    }
}
