using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class Tester
{
    public string name;
    public int id;
}
public class Test2 : MonoBehaviour
{
    //MAIN UIDOCUMENT REFERENCES
    VisualElement main;
    TextField searchText;
    DropdownField dropMenu;
    ListView listView;

    //SECONDARY UIDOCUMENT REFERENCES
    VisualElement root_VisualElementSecond;
    public VisualTreeAsset itemRoot;

    //DATA FOR TESTING
    [NonReorderable]
    public List<Tester> data;

    // Start is called before the first frame update
    void Start()
    {
        var rootElement = GetComponent<UIDocument>().rootVisualElement;
        root_VisualElementSecond = itemRoot.Instantiate();


        #region MyRegion
        listView = rootElement.Q<ListView>("TestList");

        //itemRoot.CloneTree(new VisualElement());

        //m_ItemsTab = itemRootsNew.Q<VisualElement>("itemsTab");

        //Func<VisualElement> makeItem = () => itemRoot.CloneTree();

        //Action<VisualElement, int> bindItem = (element, index) =>
        //{
        //    element.Q<Label>("name").text = data[index].name;
        //    element.Q<Label>("id").text = data[index].id.ToString();
        //};

        //listView = new ListView(data, 35, makeItem, bindItem);
        //listView.selectionType = SelectionType.Single;
        //m_ItemsTab.Add(listView);

        #endregion

        //LOADING ELEMENTS FROM MAIN UI DOCUMENT
        main = rootElement.Q<VisualElement>("Main");
        searchText = rootElement.Q<TextField>("SearchText");
        dropMenu = rootElement.Q<DropdownField>("DropMenu");

        ListView(rootElement);

        //SETTING EVENTS FOR CUSTOM CALL BACKS
        searchText.RegisterCallback<MouseDownEvent>(Printing);
        dropMenu.RegisterValueChangedCallback(DropDownMenuTest);
    }

    private void ListView(VisualElement rootVisualElement)
    {

        // Set ListView.itemsSource to populate the data in the list.
        //listView.itemsSource = data;

        // Set ListView.makeItem to initialize each entry in the list.
        //listView.makeItem = () => new Label();
        //listView.makeItem = () => itemRoot.CloneTree();

        // The "makeItem" function is called when the
        // ListView needs more items to render.
        Func<VisualElement> makeItem = () => itemRoot.CloneTree();


        // Set ListView.bindItem to bind an initialized entry to a data item.
        //listView.bindItem = (VisualElement element, int index) =>
        //    (element as Label).text = data[index].name;
        Action<VisualElement, int> bindItem = (element, index) =>
        {
            element.Q<Label>("name").text = data[index].name;
            element.Q<Label>("id").text = data[index].id.ToString();
        };

        //listView.bindItem = (element, index) =>
        //{
        //    element.Q<Label>("name").text = data[index].name;
        //    element.Q<Label>("id").text = data[index].id.ToString();
        //};

        var listView1 = rootVisualElement.Q<ListView>();
        listView1.makeItem = makeItem;
        listView1.bindItem = bindItem;
        listView1.itemsSource = data;

        //var listView1temp = new ListView(data, 35, makeItem, bindItem);

        //listView1temp.selectionType = SelectionType.Single;
        //listView1temp.style.flexGrow = 1.0f;
        //listView1temp.onItemsChosen += objects => Debug.Log(objects);
        //listView1temp.onSelectionChange += objects => Debug.Log(objects);
        listView1.fixedItemHeight = 120;
        main.Add(listView1);

    }

    private void DropDownMenuTest(ChangeEvent<string> evt)
    {
        Debug.Log(evt.newValue);
        Debug.Log("THIS IS CALLED ON CLICK to drop down menu");
    }


    private void Printing(MouseDownEvent evt)
    {
        
        Debug.Log("THIS IS CALLED ON CLICK");
        searchText.value = string.Empty;
    }

    //private void Update()
    //{
    //    if (Application.internetReachability == NetworkReachability.NotReachable)
    //    {
    //        Debug.Log("Your notare connected to internet and photon gameservers. Please check your internet connection!");
    //        main.style.visibility = Visibility.Hidden;
    //    }else { main.style.visibility = Visibility.Visible; }
    //}

    //https://forum.unity.com/threads/how-do-you-detect-if-someone-clicks-enter-return-on-a-textfield.688579/
}
