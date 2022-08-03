using UnityEngine;
using UnityEngine.UIElements;

public class ListDataFunctions
{
    Label m_NameLabel;
    //Texture m_Icon;
    VisualElement icon;
    public void SetVisualElement(VisualElement visualElement)
    {
        m_NameLabel = visualElement.Q<Label>("Name");
        icon = visualElement.Q<VisualElement>("Icon");

    }

    public void SetCharacterData(DataLayout data)
    {
        m_NameLabel.text = data.tempName;
        icon.style.backgroundImage = (StyleBackground) data.unknown;
    }
}



//https://docs.unity.cn/2021.2/Documentation/Manual/UIE-HowTo-CreateRuntimeUI.html