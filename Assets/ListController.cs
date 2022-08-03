
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class ListController
{

    // UXML template for list entries
    VisualTreeAsset m_ListEntryTemplate;

    // UI element references
    ListView m_CharacterList;
    Label testButtonLabel;
    Button ChannelButton;


    public void InitializeCharacterList(VisualElement root, VisualTreeAsset listElementTemplate, List<DataLayout> data)
    {
        EnumerateAllCharacters(data);


        // Store a reference to the template for the list entries
        m_ListEntryTemplate = listElementTemplate;

        // Store a reference to the character list element
        m_CharacterList = root.Q<ListView>("ItemsTab");


        testButtonLabel = root.Q<Label>("ButtonClickTestLabel");
        ChannelButton = root.Q<Button>("ChannelButton");

        FillCharacterList();

        //ASSIGN FUNCTIONS
        //ChannelButton.clickable.clicked += ChannelButtonClicked;
        // Register to get a callback when an item is selected

        m_CharacterList.onSelectionChange -= OnCharacterSelected;
        m_CharacterList.ClearSelection();

        m_CharacterList.onSelectionChange += OnCharacterSelected;
    }

    string channelString;
    void ChannelButtonClicked()
    {
        Application.OpenURL("https://www.youtube.com/channel/" + channelString);
    }

    void OnCharacterSelected(IEnumerable<object> selectedItems)
    {
        //m_CharacterList.onSelectionChange -= OnCharacterSelected;

        // Get the currently selected item directly from the ListView
        var selectedCharacter = m_CharacterList.selectedItem as DataLayout;

        Debug.Log("CHARACTER DATA IS " + selectedCharacter.tempName);
        channelString = selectedCharacter.channelId;
        //m_CharClassLabel.text = "Button Clicked"+selectedCharacter.tempName;
        Application.OpenURL("https://www.youtube.com/watch?v=" + selectedCharacter.videId);


        //m_CharacterList.onSelectionChange += OnCharacterSelected;
    }



    List<DataLayout> m_AllCharacters;
    
    void EnumerateAllCharacters(List<DataLayout> data)
    {
        //m_AllCharacters = new List<DataLayout>();
        //m_AllCharacters.AddRange(Resources.LoadAll<CharacterData>("Characters"));
        
        m_AllCharacters = data;
        Console.WriteLine(m_AllCharacters[0].tempName);

    }


    void FillCharacterList()
    {
        // Set up a make item function for a list entry
        m_CharacterList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();

            // Instantiate a controller for the data
            var newListEntryLogic = new ListDataFunctions();

            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;

            // Initialize the controller script
            newListEntryLogic.SetVisualElement(newListEntry);

            // Return the root of the instantiated visual tree
            return newListEntry;
        };

        // Set up bind function for a specific list entry
        m_CharacterList.bindItem = (item, index) =>
        {
            (item.userData as ListDataFunctions).SetCharacterData(m_AllCharacters[index]);
        };

        // Set a fixed item height
        m_CharacterList.fixedItemHeight = 650;

        // Set the actual item's source list/array
        m_CharacterList.itemsSource = m_AllCharacters;
    }

}
