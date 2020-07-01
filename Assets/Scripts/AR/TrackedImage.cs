using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImage : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public ARTrackedImage aRTracked;
    TrackingState oldState;
    private Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        aRTracked = GetComponent<ARTrackedImage>();
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = aRTracked.referenceImage.name;
        oldState = TrackingState.None;
        _renderer=GetComponent<Renderer>();  
    }



    // Update is called once per frame
    void Update()
    {

        if (oldState != aRTracked.trackingState)
        {
            if (aRTracked.trackingState == TrackingState.Tracking)
            {
                //videoObj.SetActive(true);
                videoPlayer.Play();
                _renderer.enabled = true;
            }
            else
            {
                //videoObj.SetActive(false);
                videoPlayer.Pause();
                _renderer.enabled = false;
            }
            oldState = aRTracked.trackingState;
        }


    }
}
