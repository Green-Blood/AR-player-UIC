using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSceneAnimator : MonoBehaviour
{
    public Animation firstAnim, secondAnim;
    public GameObject thirdAnim;

    public GameObject prevBtn, nextBtn, pagination;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    public void OnGamePageChanged(int page)
    {
        switch (page)
        {
            case 0:
                pagination.SetActive(true);
                prevBtn.SetActive(true);
                nextBtn.SetActive(true);
                firstAnim.Play("download");
                break;
            case 1:
                pagination.SetActive(true);
                prevBtn.SetActive(true);
                nextBtn.SetActive(true);
                secondAnim.Play("скан");
                break;
            case 2:
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
