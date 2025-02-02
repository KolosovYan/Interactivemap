using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarkerCreationWindow : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private FilePicker filePicker;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField descriptionInput;
    [SerializeField] private Image selectedImage;
    [SerializeField] private GameObject addImageButton;
    [SerializeField] private GameObject deleteImageButton;
    [SerializeField] private GameObject cashedGO;
    [SerializeField] private Button createButton;

    private string lastImagePath;
    private bool isEditingMode = false;
    private MapMarker tempMarker;
    private MapMarker lastMarker;

    public void OpenCreationWindow()
    {
        isEditingMode = false;
        lastMarker = null;
        nameInput.text = string.Empty;
        descriptionInput.text = string.Empty;
        createButton.interactable = false;
        selectedImage.sprite = null;
        lastImagePath = string.Empty;
        addImageButton.SetActive(true);
        deleteImageButton.SetActive(false);
        cashedGO.SetActive(true);
    }

    public void OpenCreationWindow(MapMarker marker)
    {
        isEditingMode = true;
        lastMarker = marker;
        nameInput.text = marker.Name;
        descriptionInput.text = marker.Description;
        createButton.interactable = true;
        selectedImage.sprite = marker.MarkerSprite;
        lastImagePath = marker.ImagePath;
        addImageButton.SetActive(false);
        deleteImageButton.SetActive(true);
        cashedGO.SetActive(true);
    }

    public void ValidateFieldEntries()
    {
        bool canCreate = true;

        if (string.IsNullOrEmpty(nameInput.text))
            canCreate = false;

        if (string.IsNullOrEmpty(descriptionInput.text))
            canCreate = false;

        if (selectedImage.sprite == null)
            canCreate = false;

        SetCreateButtonState(canCreate);
    }

    public void OnAddImagePressed()
    {
        filePicker.OpenFilePicker();
    }

    public void SetSelectedImage(Sprite spr, string path)
    {
        addImageButton.SetActive(false);
        deleteImageButton.SetActive(true);
        selectedImage.sprite = spr;
        lastImagePath = path;
        ValidateFieldEntries();
    }

    public void DeleteSelectedImage()
    {
        selectedImage.sprite = null;
        addImageButton?.SetActive(true);
        deleteImageButton.SetActive(false);
        FilePicker.DeleteSpriteFromPath(lastImagePath);
        lastImagePath = null;
        ValidateFieldEntries();
    }

    public void OnCreateButtonPressed()
    {
        tempMarker = isEditingMode ? lastMarker : MarkerCreationController.Instance.GetLastCreatedMarker();

        tempMarker.InizializeMarker(nameInput.text, descriptionInput.text, lastImagePath);
    }

    private void SetCreateButtonState(bool state) => createButton.interactable = state;
}
