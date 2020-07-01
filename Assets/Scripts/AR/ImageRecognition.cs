using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Web;

namespace AR
{
    public class ImageRecognition : MonoBehaviour
    {
        public static ImageRecognition imageRecognition;

        private ARTrackedImageManager _arTrackedImageManager;

        private void Awake()
        {
            _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        }

        public void OnEnable()
        {
            _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
        }

        private void OnDisable()
        {
            _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
        }

        public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
        {
            foreach (var trackedImage in args.added)
            {
                Debug.Log(trackedImage.name);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            imageRecognition = this;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public IEnumerator AddImageJob(Texture2D texture2D, string name)
        {
            yield return new WaitForSeconds(2.0f);

            Rect rec = new Rect(0, 0, texture2D.width, texture2D.height);
            Sprite spriteToUse = Sprite.Create(texture2D, rec, new Vector2(0.5f, 0.5f), 100);

            yield return null;

            var firstGuid = new SerializableGuid(0, 0);
            var secondGuid = new SerializableGuid(0, 0);

            XRReferenceImage newImage = new XRReferenceImage(firstGuid, secondGuid, new Vector2(0.1f, 0.1f), name, texture2D);

            try
            {
                Debug.Log(newImage.ToString());

                MutableRuntimeReferenceImageLibrary mutableRuntimeReferenceImageLibrary = _arTrackedImageManager.referenceLibrary as MutableRuntimeReferenceImageLibrary;

                var jobHandle = mutableRuntimeReferenceImageLibrary.ScheduleAddImageJob(texture2D, name, 0.1f);

                while (!jobHandle.IsCompleted)
                {
                }


            }
            catch (Exception e)
            {
            }
        }

        public void addImageToTracking(int id)
        {
            LibraryFullInfoData tmp = WebController.clientApp.savedData.libraryFullInfoData.Find(ni => ni.id == id);
            byte[] imageBytes = loadImage(tmp.full_image_path);
            Texture2D texture;
            texture = new Texture2D(2, 2, TextureFormat.DXT1, false);
            texture.LoadImage(imageBytes);
            StartCoroutine(AddImageJob(texture, tmp.video_path));
        }


        byte[] loadImage(string path)
        {
            byte[] dataByte = null;

            //Exit if Directory or File does not exist
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Debug.LogWarning("Directory does not exist");
                return null;
            }

            if (!File.Exists(path))
            {
                Debug.Log("File does not exist");
                return null;
            }

            try
            {
                dataByte = File.ReadAllBytes(path);
                Debug.Log("Loaded Data from: " + path.Replace("/", "\\"));
            }
            catch (Exception e)
            {
                Debug.LogWarning("Failed To Load Data from: " + path.Replace("/", "\\"));
                Debug.LogWarning("Error: " + e.Message);
            }

            return dataByte;
        }

    }
}
