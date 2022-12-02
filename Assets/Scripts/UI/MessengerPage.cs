using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessengerPage : MonoBehaviour
{
    Sprite characterPhoto;
    public GameObject characterTilePrefab, playerTilePrefab;
    public Transform messagesContainer;
    public Image characterImage;
    public Text characterName;


    public void populate(Character character)
    {
        setCharacterName(character);
        setPhoto(character);

        foreach (Question q in character.questionsAsked)
        {
            Question question = q;
            Answer answer = q.answer;

            GameObject playerTile = Instantiate(playerTilePrefab, new Vector3(0, 0, 0), Quaternion.identity, messagesContainer.transform);
            GameObject characterTile = Instantiate(characterTilePrefab, new Vector3(0, 0, 0), Quaternion.identity, messagesContainer.transform);

            characterTile.transform.GetChild(0).GetComponent<Image>().sprite = character.photo;
            characterTile.transform.GetChild(1).GetComponent<Text>().text = answer.sentence;

            playerTile.transform.GetChild(0).GetComponent<Text>().text = q.sentence;
        }
    }

    void setCharacterName(Character character)
    {
        characterName.text = character.firstName;
    }
    void setPhoto(Character character)
    {
        this.characterImage.sprite = character.photo;
    }
}
