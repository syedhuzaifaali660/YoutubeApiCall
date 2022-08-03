using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;
using System.Linq;

public class SocialApiData : MonoBehaviour
{
    public NewUIDocHandler _UIDocHandlerNew;
    public Items apiDataHere;
    public Texture defaultImg;
    //public DateTime time;

    Texture img;


    string site = "https://youtube.googleapis.com/youtube/v3/search";
    string searchPart = "=snippet";
    [HideInInspector]public string question;
    string maxResult = "&maxResults=5";
    string order = "&order=";
   public string userOrder;
    public string pageToken;
    string apiKey = "AIzaSyCNRueYJfHD-_eNcS7Qmxh4-8o5A8_t-Ns";
    
    #if UNITY_EDITOR
    string path = "Assets/Resources/ItemInfo.json";
#endif
    string pattern = "yyyy-MM-dd";

    public static bool ispageTokenGiven=false;

    [SerializeField] string data;

    private void Start()
    {
        //String date = "2019-07-14";
        //string pattern = "yyyy-MM-dd";
        //System.DateTime dateTime = System.DateTime.Parse(date);

        //Debug.Log(dateTime.ToString(pattern, "10"));
        //Debug.Log(date.Substring(5, 5));

    }

    public void SetFilter_OnSelected(Filters filterSelected)
    {
        if(filterSelected != Filters.none)
        {
            userOrder = filterSelected.ToString();
        }
        else
        {
            userOrder = "searchSortUnspecified";
        }

    }

    public void GetData_OnClick()
    {
        //StartCoroutine(GetData_Coroutine());
        StartCoroutine(SetAllData());
    
    }

    IEnumerator SetAllData()
    {
        //apiDataHere.item.Clear();
        DataElements.Instance.data.Clear();
        yield return StartCoroutine(GetData_Coroutine());

    }

    public void SetPageToken(string token)
    {
        pageToken = token;
    }
    string GiveApiUrl()
    {
        if (ispageTokenGiven)
        {
            return site + "?part" + searchPart + "&q=" + question + maxResult + "&pageToken=" + pageToken + order + userOrder + "&key=" + apiKey;

        } else {
            return site + "?part" + searchPart + "&q=" + question + maxResult + "&pageToken=" + order + userOrder + "&key=" + apiKey;
        }
    }

   
    IEnumerator GetData_Coroutine()
    {
        string url = GiveApiUrl();
        Debug.Log(url);
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error");
                yield break;
            }
            else
            {
                JSONNode info = JSON.Parse(www.downloadHandler.text);
                JSONNode apiData = info;
                JSONNode items1 = info["items"];

                Debug.Log(apiData["nextPageToken"]);
                //yield return null;
                for (int i = 0; i < items1.Count; i++)
                {
                    //AddingToMyApiList(items1[i]);
                    StartCoroutine(AddToListView_v2(items1[i], apiData["nextPageToken"]));
                        Debug.Log("----------------------------");

                    //Debug.Log(items1[i]);
                }


            }
        }

    }

    //IEnumerator AddToListView(Info apiData)
    //{
    //    DataLayout newLayout = new DataLayout();
    //    newLayout.tempName = apiData.snippet.title;

    //    var videoID = apiData.id.videoId;
    //    newLayout.videId = videoID;
        
    //    newLayout.channelId = apiData.snippet.channedId;
    //    newLayout.channelName = apiData.snippet.channelTitle;


    //    yield return StartCoroutine(DownloadImage(apiData.snippet.thumnails.defaults.url, setTexture));
    //    newLayout.unknown = img != null ? img:null;
    //    yield return new WaitForSecondsRealtime(0.05f);
    //    DataElements.Instance.data.Add(newLayout);
    //}
    IEnumerator AddToListView_v2(JSONNode apiData, JSONNode pagetoken)
    {
        DataLayout newLayout = new DataLayout();
        newLayout.tempName = apiData["snippet"]["title"];

        var videoID = apiData["id"]["videoId"];
        newLayout.videId = videoID;

        newLayout.channelId = apiData["snippet"]["channelId"];
        newLayout.channelName = apiData["snippet"]["channelTitle"];
        newLayout.publishAt = apiData["snippet"]["publishedAt"];
        DataElements.Instance.nextPageToken = pagetoken;


        yield return StartCoroutine(DownloadImage(apiData["snippet"]["thumbnails"]["high"]["url"], setTexture));
        newLayout.unknown = img != null ? img : null;
        yield return new WaitForSecondsRealtime(0.05f);
        DataElements.Instance.data.Add(newLayout);
    }

    //void AddingToMyApiList(JSONNode _info)
    //{
        

    //    Info temp = new Info();
    //    temp.kind = _info["kind"];
    //    temp.etag = _info["etag"];


    //    //JSONNode ids = _info["id"];

    //    temp.id = new ID();
    //    temp.id.kind = _info["id"]["kind"];
    //    temp.id.videoId = _info["id"]["videoId"];


    //    //JSONNode snippet = _info["snippet"];
    //    temp.snippet = new Snippet();
    //    //temp.snippet.publiushedAt = _info["snippet"]["publishedAt"];
    //    string testDate = _info["snippet"]["publishedAt"];
    //    temp.snippet.publiushedAt = testDate.Substring(0, 10);
    //    temp.snippet.channedId = _info["snippet"]["channelId"];
    //    temp.snippet.title = _info["snippet"]["title"];
    //    temp.snippet.description = _info["snippet"]["description"];

    //    temp.snippet.thumnails = new Thumbnail();
    //    temp.snippet.thumnails.defaults = new Default();
    //    temp.snippet.thumnails.defaults.url = _info["snippet"]["thumbnails"]["high"]["url"];
    //    temp.snippet.thumnails.defaults.width = _info["snippet"]["thumbnails"]["high"]["width"];
    //    temp.snippet.thumnails.defaults.height = _info["snippet"]["thumbnails"]["high"]["height"];


    //    temp.snippet.channelTitle = _info["snippet"]["channelTitle"];
    //    temp.snippet.liveBroadcastContent = _info["snippet"]["liveBroadcastContent"];
    //    temp.snippet.publishTime = _info["snippet"]["publishTime"];

    //    apiDataHere.item.Add(temp);


    //}

    //TEXTURE MANIPULATION
    void setTexture(Texture tex)
    {
        img = tex;
    }
    IEnumerator DownloadImage(string MediaUrl, Action<Texture> callback)
    {
        if (MediaUrl == null) yield return defaultImg;

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log(www.error);
        else
        {
            Texture tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
            if (www.isDone)
            {
                callback(tex);
            }

        }


    }

}
       //youtube.googleapis.com/youtube/v3/
      //search?part=snippet&q=the%20monkey%20king%203
      //&key=AIzaSyCNRueYJfHD-_eNcS7Qmxh4-8o5A8_t-Ns
    
#region Cleaning
    //public void SortByYear_OnClick()
    //{
    //    var a = new ListController();

    //    var lista = a.GetSourceListReference();

    //    //var listToSort = DataElements.Instance.data;
    //    var listToSort = lista;


    //    //listToSort = listToSort.OrderBy(x => x.date).ToList();

    //    listToSort.Sort((x, y) => x.date.CompareTo(y.date));


    //    for (int i = 0; i < listToSort.Count; i++)
    //    {
    //        Debug.Log(listToSort[i].tempName);
    //        Debug.Log(listToSort[i].date);
    //    }
    //}

    //public void SortList_OjnClick()
    //{

    //    var listToSort = DataElements.Instance.data;
    //    //var listToSort = listToSort;


    //    //listToSort = listToSort.OrderBy(x => x.date).ToList();

    //    listToSort.Sort((x, y) => x.date.CompareTo(y.date));


    //    for (int i = 0; i < listToSort.Count; i++)
    //    {
    //        Debug.Log(listToSort[i].tempName);
    //        Debug.Log(listToSort[i].date);
    //    }
    //    //listToSort.OrderBy(a => a.);
    //}
    #endregion