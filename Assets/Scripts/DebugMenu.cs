using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    public Button debugToggle;
    public GameObject debugMenu, characters, objects;
    public Button talkBtn, pickupBtn;
    public Button[] persons;
    // Start is called before the first frame update
    void Start()
    {
        debugMenu.SetActive(false);
        characters.SetActive(false);

        setOnClickListeners();
    }

    void setOnClickListeners()
    {
        debugToggle.GetComponent<Button>().onClick.AddListener(() => toggleDebug());

        Button talkButton = talkBtn.GetComponent<Button>();
        talkButton.onClick.AddListener(() => talk());

        Button pickupButton = pickupBtn.GetComponent<Button>();
        pickupButton.onClick.AddListener(() => pickup());

        foreach (Button characterButton in persons)
        {
            Button btn = characterButton.GetComponent<Button>();
            btn.onClick.AddListener(() => talkTo(characterButton.name));
        }
    }

    void talkTo(string characterName)
    {
        // new conversation
        Conversation conversation = ScriptableObject.CreateInstance<Conversation>();
    }


    void talk()
    {
        objects.SetActive(false);
        characters.SetActive(true);
    }
    void pickup()
    {
        characters.SetActive(false);
        objects.SetActive(true);
    }


    void toggleDebug()
    {
        print("debug menu toggle");
        if (debugMenu.activeSelf)
        {
            debugMenu.SetActive(false);
        }
        else if (!debugMenu.activeSelf)
        {
            debugMenu.SetActive(true);
            // bottom.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
