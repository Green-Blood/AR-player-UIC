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
        
        public void ChangeToUzbekSplash()
        {
            for (var i = 0; i < splashScreenText.Length; i++)
            {
                splashScreenText[i].text = uzbekText[i];
            }
        }

        public void ChangeToRussianSplash()
        {
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
