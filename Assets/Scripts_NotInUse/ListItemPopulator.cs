using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ListItemPopulator : MonoBehaviour
{
    public Texture unknown;
    public string tempName;

    private VisualElement m_ItemsTab;
    private static VisualTreeAsset m_ItemRowTemplate;
    private ListView m_ItemListView;
    private float m_ItemHeight = 40;

    //private void Start()
    //{
    //    // ...previous code in method is above
    //    m_ItemsTab = GetComponent<UIDocument>().rootVisualElement.Q<ListView>("ItemsTab");
    //    GenerateListView();
    //}



    //private void GenerateListView()
    //{
    //    Func<VisualElement> makeItem = () => m_ItemRowTemplate.CloneTree();

    //    Action<VisualElement, int> bindItem = (e, i) =>
    //    {
    //        //e.Q<VisualElement>("Icon").style.backgroundImage = m_ItemDatabase[i] == null ? unknown : unknown;
    //        e.Q<VisualElement>("Icon").style.backgroundImage = (StyleBackground) unknown;
    //        e.Q<Label>("Name").text = tempName+i;
    //    };

    //    m_ItemListView = new ListView(temp, 35, makeItem, bindItem);
    //    m_ItemListView.selectionType = SelectionType.Single;
    //    m_ItemListView.style.height = 35;
    //    m_ItemsTab.Add(m_ItemListView);
    //}
}
//SITE LINK WHERE THE CODE IS FROM
//https://gamedev-resources.com/create-an-item-management-editor-tool-with-ui-toolkit/