using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccusePanel : MonoBehaviour
{
    public Button maryButton, harryButton, jamesButton;
    public GameManager gameManager;

    void Start()
    {
        maryButton.onClick.AddListener(() => accuse(gameManager.mary));
        harryButton.onClick.AddListener(() => accuse(gameManager.harry));
        jamesButton.onClick.AddListener(() => accuse(gameManager.james));
    }

    void accuse(Character character)
    {
        gameManager.endGame(character);
    }
}
