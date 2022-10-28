using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BioPage : MonoBehaviour
{
    public GameObject image, nameText, nickNameText, dateOfBirthText, occupationText, favouriteFoodText, religionText, descriptionText;

    public void populateBio(Character character)
    {
        image.GetComponent<Image>().sprite = character.photo;
        nameText.GetComponent<Text>().text = $"{character.firstName} {character.middleName} {character.lastName}";
        nickNameText.GetComponent<Text>().text = character.nickName;
        dateOfBirthText.GetComponent<Text>().text = formatDateOfBirth(character.dateOfBirth);
        occupationText.GetComponent<Text>().text = character.careerPath;
        favouriteFoodText.GetComponent<Text>().text = character.favoriteFoods[0];
        religionText.GetComponent<Text>().text = character.religion;
        descriptionText.GetComponent<Text>().text = character.longDescription;
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
