using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Character[] charactersMet;
    public UiHandler notebookHandler;
    public GameObject notebook;

    void start()
    {
        // notebook handler
    }
    void Update()
    {
        testInput();
    }


    public void spawnCharacter()
    {
        throw new System.NotImplementedException("Not implemented");
    }

    void testInput()
    {
        // CONVERSATION WITH AVERAGE BOB
        if (Input.GetKeyDown(KeyCode.A))
        {
            // TODO find another way to get the character
            // initiateConversation(GameObject.Find("averageBob"));
        }
        // CHARACTERS
        else if (Input.GetKeyDown(KeyCode.U))
        {
            // this.uiHandler.goToPage(this.notebook.pages[0]);
        }
        // CLUES
        else if (Input.GetKeyDown(KeyCode.I))
        {
            // this.uiHandler.goToPage(this.notebook.pages[1]);
        }
        // CONVERSATIONS
        else if (Input.GetKeyDown(KeyCode.O))
        {
            // this.uiHandler.goToPage(this.notebook.pages[2]);
        }
        // BIO
        else if (Input.GetKeyDown(KeyCode.P))
        {
            // this.uiHandler.goToPage(this.notebook.pages[3]);
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // this.toggleNotebook();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            // charactersPanel.populateButton(GameObject.Find("averageBob").GetComponent<Character>());
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            // charactersPanel.populateButton(GameObject.Find("shortKathy").GetComponent<Character>());
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            // charactersPanel.populateButton(GameObject.Find("oldReginald").GetComponent<Character>());
        }
    }
}
