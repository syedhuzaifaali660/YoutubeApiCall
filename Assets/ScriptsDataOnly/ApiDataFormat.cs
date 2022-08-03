using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiDataFormat : MonoBehaviour
{
    public Items items;
}
[Serializable]
public class Items
{
    public List<Info> item;
}

[Serializable]
public class Info
{
    public string kind;
    public string etag;
    public ID id;
    public Snippet snippet;
}
[Serializable]
public struct ID
{
    public string kind;
    public string videoId;
}
[Serializable]
public struct Snippet
{
    public string publiushedAt;
    public string channedId;
    public string title;
    public string description;
    public Thumbnail thumnails;
    public string channelTitle;
    public string liveBroadcastContent;
    public string publishTime;
}
[Serializable]
public struct Thumbnail
{
    public Default defaults;
    //public Medium medium;
    //public High high;
}
[Serializable]
public struct Default
{
    public string url;
    public float width;
    public float height;
}

public enum Filters
{
    none=0,
    date=1,
    rating=2,
    viewCount=3,
    relevance=4,
    title=5,
    videoCount=6,
}
//[Serializable]
//public struct Medium
//{
//    public string url;
//    public int width;
//    public int height;
//}
//[Serializable]
//public struct High
//{
//    public string url;
//    public int width;
//    public int height;
//}


