using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class NewUIDocHandler : MonoBehaviour
{
    public SocialApiData _handlerSocialApi;
    public MainView _mainViewHandler;
    Button searchButton;
    Button sortDateButton;
    Button sortYearButton;
    Button pull;
    Button push;
    Label pageNumber;
    int pageCount=0;
    TextField searchText;
    VisualElement wifiScreen;
    DropdownField FilterDropDown;


    TextField userSearchQuestion;
    ListView listViewHolder;
    VisualElement root;

    Stack<string> pageToken = new Stack<string>();

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        searchButton = root.Q<Button>("SearchButton");
        sortDateButton = root.Q<Button>("sortByDateButton");
        sortYearButton = root.Q<Button>("sortByYearButton");
        push = root.Q<Button>("push");
        pull = root.Q<Button>("pull");
        pageNumber = root.Q<Label>("pageNumber");


        userSearchQuestion = root.Q<TextField>("userInnputField");
        listViewHolder = root.Q<ListView>("ItemsTab");
        searchText = root.Q<TextField>("userInnputField");
        wifiScreen = root.Q<VisualElement>("WifiScreen");
        FilterDropDown = root.Q<DropdownField>("FilterDropDown");
        //main = root.Q<VisualElement>("Main");
        

        //ASSIGNING FUNCTIONS
        searchButton.clicked += SearchButtonPresed;
        sortDateButton.clicked += SortByDate_Button;
        sortYearButton.clicked += SortByYear_Button;
        push.clicked += PushButtonClicked;
        pull.clicked += PullButtonClicked;

        searchText.RegisterCallback<MouseDownEvent>(Printing);
        FilterDropDown.RegisterValueChangedCallback(FilterSeletec_OnValueChange);
        Debug.Log(FilterDropDown.index);
    }
    public void PullButtonClicked()
    {
        _handlerSocialApi.SetPageToken(pageToken.Peek());
        Debug.Log("POPPING");
        pageToken.Pop();
        
        pageCount--;
        pageNumber.text = pageCount.ToString();

        foreach (var item in pageToken)
        {
            Debug.Log(item);
        }
        Debug.Log("POPPING-------------");

        SearchButtonPresed_ForToken();
    }
    public void PushButtonClicked()
    {
        SocialApiData.ispageTokenGiven = true;
        Debug.Log("PUSHING");

        string token = DataElements.Instance.nextPageToken;
        _handlerSocialApi.SetPageToken(token);
        pageToken.Push(DataElements.Instance.nextPageToken);

        pageCount++;
        pageNumber.text = pageCount.ToString();

        foreach (var item in pageToken)
        {
            Debug.Log(item);
        }
        Debug.Log("PUSHING-------------------");

        SearchButtonPresed_ForToken();
    }

    private void FilterSeletec_OnValueChange(ChangeEvent<string> evt)
    {
        Debug.Log(evt.newValue);
        Debug.Log(FilterDropDown.index);

        _handlerSocialApi.SetFilter_OnSelected((Filters)FilterDropDown.index);
        SearchButtonPresed();
    }


    public void RefreshListView()
    {
        listViewHolder.Rebuild();
    }

    public void SortByDate_Button()
    {
        Debug.Log("SORT BY DATE PRESSED");
        
    }

    public void SortByYear_Button()
    {
        Debug.Log("SORT BY YEAR PRESSED");
        
    }

    void SearchButtonPresed()
    {
        SocialApiData.ispageTokenGiven = false;

        Debug.Log(userSearchQuestion.text);
        Debug.Log("Search Pressed");
        
        if (string.IsNullOrEmpty(userSearchQuestion.text)) return;
        

        _handlerSocialApi.question = userSearchQuestion.text;
        StartCoroutine(SettingDataHere());
    }

    void SearchButtonPresed_ForToken()
    {

        if (string.IsNullOrEmpty(userSearchQuestion.text)) return;


        _handlerSocialApi.question = userSearchQuestion.text;
        StartCoroutine(SettingDataHere());
    }

    IEnumerator SettingDataHere()
    {
        _handlerSocialApi.GetData_OnClick();
        yield return new WaitForSecondsRealtime(4f);
        Debug.Log("SCRIPT CALLED POINT 1");
        _mainViewHandler.ShowDataInListView_Func();
        yield return new WaitForSecondsRealtime(0.5f);
        listViewHolder.Rebuild();
    }


    private void Printing(MouseDownEvent evt)
    {

        Debug.Log("THIS IS CALLED ON CLICK");
        searchText.value = string.Empty;
    }

    private void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Your not connected to internet. Please check your internet connection!");
            wifiScreen.style.visibility = Visibility.Visible;
        }
        else 
        { 
            wifiScreen.style.visibility = Visibility.Hidden; 
        }
    }
}
