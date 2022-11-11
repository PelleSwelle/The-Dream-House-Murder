using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BioPage : MonoBehaviour
{
    public GameObject image, nameText, nickNameText, dateOfBirthText, descriptionText;

    public void populateBio(Character character)
    {
        image.GetComponent<Image>().sprite = character.photo;
        nameText.GetComponent<Text>().text = $"{character.firstName} {character.middleName} {character.lastName}";
        nickNameText.GetComponent<Text>().text = character.firstName;
        dateOfBirthText.GetComponent<Text>().text = formatDateOfBirth(character.dateOfBirth);
        descriptionText.GetComponent<Text>().text = character.description;
    }
    string formatDateOfBirth(int dateOfBirth)
    {
        // day, month, year
        int date = dateOfBirth.ToString()[0] + dateOfBirth.ToString()[1];
        int month = dateOfBirth.ToString()[2] + dateOfBirth.ToString()[3];
        int year = dateOfBirth.ToString()[4] + dateOfBirth.ToString()[5] + dateOfBirth.ToString()[6] + dateOfBirth.ToString()[7];


        return $"{date} / {month} - {year}";
    }
}
