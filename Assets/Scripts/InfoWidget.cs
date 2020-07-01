using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using  UnityEngine.UI;
using TMPro;
using Web;
using AR;

public class InfoWidget : MonoBehaviour
{
    public LibraryListData data;
    public Image coverImage;
    public TMP_Text title;
    public TMP_Text downladerCount;
    public TMP_Text description;
    public GameObject playBtn, downloadBtn;
    public void FillData()
    {
        title.text = data.libraryFullInfoData.caption_text;
        downladerCount.text = data.libraryFullInfoData.views.ToString();
        description.text = data.libraryFullInfoData.full_text;

    }

    public void fillImage()
    {
        Texture2D tmp = WebController.LoadPNG(data.libraryFullInfoData.caption_image_path);
        coverImage.sprite = Sprite.Create(tmp, new Rect(0, 0, tmp.width, tmp.height), Vector2.one / 2.0f);

    }

    public void downloadImageAndVideo()
    {
        WebController.clientApp.downloadImageAndVideoData(data.libraryFullInfoData.id, ImageRecognition.imageRecognition.addImageToTracking);
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnDisable()
    {
        playBtn.SetActive(true);
        downloadBtn.SetActive(false);
    }

    void OnEnable()
    {
        playBtn.SetActive(false);
        downloadBtn.SetActive(true);
    }    // Update is called once per frame
    void Update()
    {

    }
}
