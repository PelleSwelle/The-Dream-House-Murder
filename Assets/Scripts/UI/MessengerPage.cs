using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessengerPage : MonoBehaviour
{
    public Sprite characterPhoto;
    public string characterName;
    public GameObject characterTilePrefab, playerTilePrefab;
    public Transform messagesContainer;


    public void populate(Character character)
    {
        characterPhoto = character.photo;
        characterName = character.firstName;
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
}
