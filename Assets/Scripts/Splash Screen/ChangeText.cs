using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Splash_Screen
{
    public class ChangeText : MonoBehaviour
    {
        public Text[] splashScreenText;
        //public TMP_Text[] infoScreenText;
        public string[] uzbekText, russianText;

        public Button uzbekBtn, russianBtn;
        Sprite ButtonOn;
        Sprite ButtonOff;
        private bool _isClicked;
        private Color uzbekTextColor, russianTextColor;

        private void Start()
        {
            ButtonOn = uzbekBtn.image.sprite;
            ButtonOff = russianBtn.image.sprite;
            
            uzbekTextColor = uzbekBtn.GetComponentInChildren<TextMeshProUGUI>().color;
            russianTextColor = russianBtn.GetComponentInChildren<TextMeshProUGUI>().color;
        }

        public void ChangeToUzbekSplash()
        {

            _isClicked = true;
            if (_isClicked)
            {
            
                uzbekBtn.image.overrideSprite = ButtonOn;
                russianBtn.image.overrideSprite = ButtonOff;
                uzbekBtn.GetComponentInChildren<TextMeshProUGUI>().color = uzbekTextColor;
                russianBtn.GetComponentInChildren<TextMeshProUGUI>().color = russianTextColor;
            }
            else
            {
            
                uzbekBtn.image.overrideSprite = ButtonOff;
                russianBtn.image.overrideSprite = ButtonOn;
                uzbekBtn.GetComponentInChildren<TextMeshProUGUI>().color = russianTextColor;
                russianBtn.GetComponentInChildren<TextMeshProUGUI>().color = uzbekTextColor;
            }
            
            for (var i = 0; i < splashScreenText.Length; i++)
            {
                splashScreenText[i].text = uzbekText[i];
            }
        }

        public void ChangeToRussianSplash()
        {
            _isClicked = false;
            if (_isClicked)
            {
                uzbekBtn.GetComponentInChildren<TextMeshProUGUI>().color = uzbekTextColor;
                russianBtn.GetComponentInChildren<TextMeshProUGUI>().color = russianTextColor;
                uzbekBtn.image.overrideSprite = ButtonOn;
                russianBtn.image.overrideSprite = ButtonOff;
            }
            else
            {
                uzbekBtn.GetComponentInChildren<TextMeshProUGUI>().color = russianTextColor;
                russianBtn.GetComponentInChildren<TextMeshProUGUI>().color = uzbekTextColor;
                uzbekBtn.image.overrideSprite = ButtonOff;
                russianBtn.image.overrideSprite = ButtonOn;
            }
            for (var i = 0; i < splashScreenText.Length; i++)
            {
                splashScreenText[i].text = russianText[i];
            }
        }
        
        
        /*public void ChangeToUzbekInfo()
        {
            for (var i = 0; i < infoScreenText.Length; i++)
            {
                infoScreenText[i].text = uzbekText[i];
            }
        }

        public void ChangeToRussianInfo()
        {
            for (var i = 0; i < infoScreenText.Length; i++)
            {
                infoScreenText[i].text = russianText[i];
            }
        }*/
    }
}
