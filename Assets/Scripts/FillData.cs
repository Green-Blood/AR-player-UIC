using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Web;

public class FillData : MonoBehaviour
{
    public GameObject prefab;
    public LibraryListData libraryListData;
    // Start is called before the first frame update
    void Start()
    {
        fillPrefab();
    }
    public void fillPrefab()
    {
        StartCoroutine(WebController.clientApp.GetLibraryData(libraryListData.libraryInfoData.id, getData));
    }
    public void getData(LibraryFullInfoData data)
    {
        libraryListData.libraryFullInfoData = data;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
