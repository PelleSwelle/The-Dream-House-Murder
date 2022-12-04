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
    private MessengerTileCharacter messengerTileCharacter;
    private MessengerTilePlayer messengerTilePlayer;

    public void populate(Character character)
    {
        setCharacterName(character);
        setPhoto(character);

        foreach (Question q in character.questionsAsked)
        {
            print("populated messenger page with" + q.sentence);
            Question question = q;
            Answer answer = q.answer;

            GameObject playerTile = Instantiate(playerTilePrefab, new Vector3(0, 0, 0), Quaternion.identity, messagesContainer.transform);
            messengerTilePlayer = playerTile.GetComponent<MessengerTilePlayer>();
            messengerTilePlayer.setText(q);

            GameObject characterTile = Instantiate(characterTilePrefab, new Vector3(0, 0, 0), Quaternion.identity, messagesContainer.transform);
            messengerTileCharacter = characterTile.GetComponent<MessengerTileCharacter>();
            messengerTileCharacter.setImage(character);
            messengerTileCharacter.setText(answer);
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
