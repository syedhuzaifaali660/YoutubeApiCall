using System;
using System.Collections.Generic;
using UnityEngine;

public class DataElements : MonoBehaviour
{
    public string nextPageToken;
    public List<DataLayout> data = new List<DataLayout>();

    public static DataElements Instance;
    private void Awake()
    {
        Instance = this;
    }
}

[Serializable]
public class DataLayout
{
    public Texture unknown;
    public string tempName;
    public string videId;
    public string channelId;
    public string channelName;
    public string publishAt;


}
