using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ButtonChange
{
    [RequireComponent(typeof(Button))]
    public class ButtonOnOff : MonoBehaviour
    {
        public Button downButton;
        public Button playButton;
        
        public Sprite downButtonOn;
        public Sprite downButtonOff;

        public GameObject closeBtn;
        
        bool _isClicked = false;

        public GameObject thePanel;
        private static readonly int IsOpen = Animator.StringToHash("isOpen");

        private void Start()
        {
            downButton = GetComponent<Button>();
            playButton = GetComponent<Button>();
        }

        public void ChangeButton()
        {
            _isClicked = !_isClicked;
            downButton.image.overrideSprite = _isClicked ? downButtonOn : downButtonOff;
            closeBtn.SetActive(!closeBtn.activeInHierarchy);
            var theAnimator = thePanel.GetComponent<Animator>();
            if (theAnimator == null) return;
            var isOpen = theAnimator.GetBool(IsOpen);
            theAnimator.SetBool(IsOpen, !isOpen);

        }
    }
}
