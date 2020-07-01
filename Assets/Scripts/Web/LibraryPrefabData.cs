using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using Web;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LibraryPrefabData : MonoBehaviour
{
    public LibraryListData libraryListData;
    public InfoWidget infoWidget;
    
    //public TextMeshProUGUI title;
    //public TextMeshProUGUI downQuantityText;
    //public RawImage downQuantityImage; 
    // Start is called before the first frame update
    public void onDownloadClick()
    {
        downloadData();
    }

    public void getData(LibraryFullInfoData data)
    {
        libraryListData.libraryFullInfoData = data;
    }

    public void downloadData()
    {
        StartCoroutine(WebController.clientApp.GetLibraryData(libraryListData.libraryInfoData.id, getData));
    }

    public void callWidget()
    {
        if (string.IsNullOrEmpty(libraryListData.libraryFullInfoData.caption_text))
        {
            StartCoroutine(
                WebController.clientApp.GetLibraryData(libraryListData.libraryInfoData.id, addFullInfoData));
        }
        else
        {
            infoWidget.fillImage();
        }

        infoWidget.data = libraryListData;
        infoWidget.closeBtn.SetActive(!infoWidget.closeBtn.activeInHierarchy);
        infoWidget.gameObject.SetActive(!infoWidget.gameObject.activeInHierarchy);
        infoWidget.FillData();
    }

    public void addFullInfoData(LibraryFullInfoData newData)
    {
        libraryListData.libraryFullInfoData = newData;
        libraryListData.libraryFullInfoData.caption_image_path = Path.Combine(Application.persistentDataPath, "data", "Icons", Path.GetFileName(libraryListData.libraryFullInfoData.caption_image));
        StartCoroutine(WebController.clientApp._downloadImage(new WWW(libraryListData.libraryFullInfoData.caption_image), libraryListData.libraryFullInfoData.caption_image_path,infoWidget.fillImage));

    }
     
}