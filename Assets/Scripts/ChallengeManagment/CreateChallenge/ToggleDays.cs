using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BME;
using UnityEngine.EventSystems;

public class ToggleDays : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject addToggle; //Add toggle Prefab
    [SerializeField] GameObject addRow; //Add row Prefab
    GameObject daysField; //Field of all Toggles
    CreateChallenge createChallengeScript;//Main script of Scene
    // Start is called before the first frame update
    void Start()
    {
        Visuals.SelectToggle();
        createChallengeScript = GameObject.Find("Canvas").GetComponent<CreateChallenge>();
        daysField = GameObject.Find("DaysToggle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Visuals.SelectToggle(); //Colors the selected Toggle
        GameObject selectedToggle = Visuals.selectedToggle;
        if (selectedToggle.name == "Add" || selectedToggle.name == "Add(Clone)")
        {
            createChallengeScript.totalDays++;
            selectedToggle.name = "Day" + createChallengeScript.totalDays.ToString();
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
        selectedToggle.GetComponent<Toggle>().isOn = true;
    }
}
