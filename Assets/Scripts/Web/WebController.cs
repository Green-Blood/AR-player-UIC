using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
namespace Web
{
    public class WebController : MonoBehaviour
    {
        public static WebController clientApp;

    public SavedData savedData;
    public string serverPath = "https://ar.controller.uz/";
    public AllLibrariesData allLibrariesData;
    public GetOneLibraryObject getOneLibraryObject;
    public LibraryPrefabData prefabData;
    public GameObject parent;
    public InfoWidget infoWidget;

    // public TextMeshProUGUI title;
    //public TextMeshProUGUI downQuantityText;
    //public RawImage downQuantityImage;

    //public LibraryPrefabData[] prefab;
     
    private void Awake()
    {
        //if (clientApp)
        //    Destroy(gameObject);
        clientApp = this;
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        LoadContent();
        StartCoroutine(CheckUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CheckUpdate()
    {
        var request = UnityWebRequest.Get(serverPath + "library/list/");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("token", "64789bff76d77b466a8655e4b64c7904d4df0328");

        yield return request.SendWebRequest();

        allLibrariesData = JsonUtility.FromJson<AllLibrariesData>(request.downloadHandler.text);
        if(allLibrariesData.count>allLibrariesData.results.Length)
        {
            request = UnityWebRequest.Get(serverPath + "library/list/" + "?limit=" + allLibrariesData.count+ "&offset=0");

            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
            allLibrariesData = JsonUtility.FromJson<AllLibrariesData>(request.downloadHandler.text);
            downloadAndNewItems();
        }

    }


    public IEnumerator GetLibraryData(int id, System.Action<LibraryFullInfoData> libraryFullInfo)
    {
        var request = UnityWebRequest.Get(serverPath + "library/list/"+id.ToString()+"/");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("token", "64789bff76d77b466a8655e4b64c7904d4df0328");

        yield return request.SendWebRequest();

        getOneLibraryObject = JsonUtility.FromJson<GetOneLibraryObject>(request.downloadHandler.text);
        savedData.libraryFullInfoData.Add(getOneLibraryObject.results[0]);
        libraryFullInfo(savedData.libraryFullInfoData[savedData.libraryFullInfoData.Count - 1]); ;
    }

    public void downloadAndNewItems()
    {
        if (allLibrariesData.count > savedData.libraryInfoData.Count)
        {
            for (int i = 0; i < allLibrariesData.results.Length; i++)
            {
                allLibrariesData.results[i].icon_path = Path.Combine(Application.persistentDataPath, "data", "Icons", Path.GetFileName(allLibrariesData.results[i].icon));
                Debug.Log(Path.Combine(Application.persistentDataPath, "data", "Icons", Path.GetFileName(allLibrariesData.results[i].icon)));
                StartCoroutine(_downloadImage(new WWW(allLibrariesData.results[i].icon), allLibrariesData.results[i].icon_path));
                savedData.libraryInfoData.Add(allLibrariesData.results[i]);
            }

        }
        fillData();
    }
    
    
    
    public void fillData()
    {
        for(int i=0;i< savedData.libraryInfoData.Count;i++)
        {
            prefabData.libraryListData = new LibraryListData(savedData.libraryInfoData[i], savedData.libraryFullInfoData.Find(ni => ni.id == savedData.libraryInfoData[i].id));
            
            // if (string.IsNullOrEmpty(prefabData.libraryListData.libraryFullInfoData.caption_text))
            // {
            //     // StartCoroutine(
            //     //     GetLibraryData(prefabData.libraryListData.libraryInfoData.id, AddFullInfoData));
            // }
            // else
            // {
            //     fillImage();
            // }
            
            prefabData.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = prefabData.libraryListData.libraryInfoData.caption_text;
            prefabData.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = prefabData.libraryListData.libraryInfoData.views.ToString();
            

            Instantiate(prefabData, parent.transform).infoWidget=infoWidget;
            //fillImage();
            
 
        }
    }
    // public void AddFullInfoData(LibraryInfoData newData)
    // {
    //     prefabData.libraryListData.libraryInfoData = newData;
    //     prefabData.libraryListData.libraryInfoData.icon_path = Path.Combine(Application.persistentDataPath, "data", "Icons", Path.GetFileName(prefabData.libraryListData.libraryInfoData.icon));
    //     StartCoroutine(_downloadImage(new WWW(prefabData.libraryListData.libraryInfoData.icon), prefabData.libraryListData.libraryInfoData.icon_path,infoWidget.fillImage));

    // }
    
    
    public void fillImage()
    {
        Texture2D tmp = LoadPNG(prefabData.libraryListData.libraryInfoData.icon_path);
        prefabData.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Sprite.Create(tmp,new Rect(0,0,tmp.width,tmp.height), Vector2.one/2.0f );

    }

    private void SaveContent()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/libraryData.dat");

        bf.Serialize(file, savedData);
        file.Close();
    }

    public void LoadContent()
    {
        if (File.Exists(Application.persistentDataPath + "/libraryData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/libraryData.dat", FileMode.Open);
            savedData = (SavedData)bf.Deserialize(file);
            file.Close();
        }
        else
            savedData = new SavedData();
    }



    private void saveData(string path, byte[] dataBytes)
    {
        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        try
        {
            File.WriteAllBytes(path, dataBytes);
            //Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            //Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            //Debug.LogWarning("Error: " + e.Message);
        }
    }
    
    public static Texture2D LoadPNG(string filePath) {
 
        Texture2D tex = null;
        byte[] fileData;
 
        if (File.Exists(filePath))     {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }


        private IEnumerator _downloadImage(WWW www, string savePath)
        {
            yield return www;

            //Check if we failed to send
            if (string.IsNullOrEmpty(www.error))
            {
                //UnityEngine.Debug.Log("Success");

                //Save Image
                saveData(savePath, www.bytes);
            }
            else
            {
                UnityEngine.Debug.Log("Error: " + www.error);
            }
        }
        private IEnumerator _downloadImage(WWW www, string savePath,int id, Action<int> callFunction)
        {
            yield return www;

            //Check if we failed to send
            if (string.IsNullOrEmpty(www.error))
            {
                //UnityEngine.Debug.Log("Success");

                //Save Image
                saveData(savePath, www.bytes);
                callFunction(id);
            }
            else
            {
                UnityEngine.Debug.Log("Error: " + www.error);
            }
        }
        public IEnumerator _downloadImage(WWW www, string savePath,Action callFunction)
    {
        yield return www;

        //Check if we failed to send
        if (string.IsNullOrEmpty(www.error))
        {
            //UnityEngine.Debug.Log("Success");

            //Save Image
            saveData(savePath, www.bytes);
            callFunction();
        }
        else
        {
            UnityEngine.Debug.Log("Error: " + www.error);
        }
    }

        public void downloadImageAndVideoData(int id, Action<int> callFunction)
        {
            LibraryFullInfoData  tmp = savedData.libraryFullInfoData.Find(ni => ni.id == id); 
            tmp.full_image_path = Path.Combine(Application.persistentDataPath, "data", "Full_Images", Path.GetFileName(tmp.full_image));
            StartCoroutine(_downloadImage(new WWW(tmp.full_image), tmp.full_image_path));


            tmp.video_path = Path.Combine(Application.persistentDataPath, "data", "Videos", Path.GetFileName(tmp.video));
            StartCoroutine(_downloadImage(new WWW(tmp.video), tmp.video_path,id, callFunction));
        }
    }
}

[System.Serializable]
public class AllLibrariesData
{
    public int count;
    public string next;
    public string previous;
    public LibraryInfoData[] results;
}

[System.Serializable]
public class LibraryInfoData
{
    public int id;
    public string icon;
    public string caption_text;
    public int views;
    public string created;

    public string icon_path;
}

[System.Serializable]
public class LibraryFullInfoData
{
    public int id;
    public string caption_image;
    public string icon;
    public string full_image;
    public string video;
    public string caption_text;
    public string full_text;
    public int views;
    public string created;
    public string updated;

    public string caption_image_path;
    public string icon_path;
    public string full_image_path;
    public string caption_text_path;
    public string video_path;
}

[System.Serializable]
public class GetOneLibraryObject
{
    public int count;
    public string next;
    public string previous;
    public LibraryFullInfoData[] results;
}


[System.Serializable]
public class SavedData
{
    public List<LibraryInfoData> libraryInfoData;
    public List<LibraryFullInfoData> libraryFullInfoData;
    public List<string> caption_image_path;
    public List<string> icon_path;
    public List<string> full_image_path;
    public List<string> caption_text_path;
    public SavedData()
    {
        libraryInfoData = new List<LibraryInfoData>();
        libraryFullInfoData = new List<LibraryFullInfoData>();

    }
}

[System.Serializable]
public class LibraryListData
{
    public LibraryInfoData libraryInfoData;
    public LibraryFullInfoData libraryFullInfoData;

    public LibraryListData(LibraryInfoData libraryInfo, LibraryFullInfoData libraryFullInfo)
    {
        libraryInfoData = libraryInfo;
        libraryFullInfoData = libraryFullInfo;

    }
}
