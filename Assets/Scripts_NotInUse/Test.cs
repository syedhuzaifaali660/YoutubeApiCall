using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    TextField searchText;
    VisualElement main;

    // Start is called before the first frame update
    void Start()
    {
        var rootElement = GetComponent<UIDocument>().rootVisualElement;

        main = rootElement.Q<VisualElement>("Main");
        searchText = rootElement.Q<TextField>("userInnputField");
        //if(searchText!=null)
        //{
        //    searchText.value = string.Empty;
        //}
        searchText.RegisterCallback<MouseDownEvent>(Printing);

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
            Debug.Log("Your notare connected to internet and photon gameservers. Please check your internet connection!");
            main.style.visibility = Visibility.Hidden;
        }else { main.style.visibility = Visibility.Visible; }
    }

    //https://forum.unity.com/threads/how-do-you-detect-if-someone-clicks-enter-return-on-a-textfield.688579/
}
