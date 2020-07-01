using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI.Extensions.Examples;
using Debug = UnityEngine.Debug;

namespace Splash_Screen
{
    public class SplashScreenAnimator : MonoBehaviour
    {
         
        public Animation firstAnim, secondAnim;
        public GameObject thirdAnim;

        private static readonly int IsRunning = Animator.StringToHash("isRunning");

        // public Animator thirdAnim;

        public GameObject prevBtn, nextBtn, pagination;
        
        // Start is called before the first frame update
        public void OnPageChanged(int page)
        {
            switch(page)
            {
                case 0:
                    pagination.SetActive(false);
                    prevBtn.SetActive(false);
                    nextBtn.SetActive(false);
                    break;
                case 1:
                    pagination.SetActive(true);
                    prevBtn.SetActive(true);
                    nextBtn.SetActive(true);
                    firstAnim.Play("download");
                    break;
                case 2:
                    pagination.SetActive(true);
                    prevBtn.SetActive(true);
                    nextBtn.SetActive(true);
                    secondAnim.Play("скан");
                    break;
                case 3:
                    pagination.SetActive(true);
                    prevBtn.SetActive(true);
                    nextBtn.SetActive(false);                    
                    var theAnimator = thirdAnim.GetComponent<Animator>();
                    if (theAnimator == null) return;
                    var isRunning = theAnimator.GetBool("isRunning");
                    theAnimator.SetBool(IsRunning, !isRunning);
                    break;
            }
        }

    
    }
}
