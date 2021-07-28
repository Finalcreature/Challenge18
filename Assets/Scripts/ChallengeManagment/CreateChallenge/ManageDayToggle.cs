using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BME;
using UnityEngine.EventSystems;

public class ManageDayToggle : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject addToggle; //Add toggle Prefab
    [SerializeField] GameObject addRow; //Add row Prefab
    GameObject daysField; //Field of all Toggles
    CreateChallenge createChallengeScript;//Main script of Scene
    // Start is called before the first frame update
    void Start()
    {
        daysField = GameObject.Find("DaysToggle");
        createChallengeScript = GameObject.Find("Canvas").GetComponent<CreateChallenge>();
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        GameObject selectedToggle = Visuals.SelectToggle(createChallengeScript.dayToggles); //Colors the selected Toggle
        createChallengeScript.currentToggle = selectedToggle.GetComponent<Toggle>();
        StartCoroutine(createChallengeScript.SetUpDayTitle());
        if (selectedToggle.name == "Add" || selectedToggle.name == "Add(Clone)")
        {
            createChallengeScript.totalDays++;
            selectedToggle.name = "Day " + createChallengeScript.totalDays.ToString();
            selectedToggle.GetComponentInChildren<Text>().text = "Day " + createChallengeScript.totalDays.ToString();
            if (selectedToggle.transform.parent.childCount < 4)
            {
                AddNewToggle(selectedToggle, selectedToggle.transform.parent);
            }
            else
            {
                GameObject newRow = AddNewRow();
                AddNewToggle(selectedToggle, newRow.transform);
            }

        }
    }
    /// <summary>
    /// adds another row to the toggle field area
    /// </summary>
    /// <returns>new Row</returns>
    private GameObject AddNewRow()
    {
        GameObject newRow = Instantiate(addRow, daysField.transform);
        newRow.name = "Row" + (daysField.transform.childCount - 1);
        return newRow;
    }
    /// <summary>
    /// adds a new toggle button and changes his name and texe
    /// </summary>
    /// <param name="selectedToggle">previus toggle selected</param>
    /// <param name="row">row which the toggle will be added to</param>
    private void AddNewToggle(GameObject selectedToggle, Transform row)
    {
        GameObject newToggle = Instantiate(addToggle, row);
        newToggle.name = "Add";
        newToggle.GetComponentInChildren<Text>().text = "Add";
        newToggle.GetComponent<Toggle>().isOn = false;
        newToggle.GetComponent<Toggle>().targetGraphic.color = newToggle.GetComponent<Toggle>().colors.normalColor; 
        selectedToggle.GetComponent<Toggle>().isOn = true;
        createChallengeScript.dayToggles.Add(newToggle.GetComponent<Toggle>());
        createChallengeScript.challengeOptions.daysArr.Add(new Day(createChallengeScript.totalDays, ""));
        StartCoroutine(createChallengeScript.SetUpDayTitle());
    }
}
