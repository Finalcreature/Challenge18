using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TasksManager : MonoBehaviour, IPointerClickHandler
{
    CreateChallenge createChallengeScript;
    public void Start()
    {
        createChallengeScript = GameObject.Find("Canvas").GetComponent<CreateChallenge>();
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        createChallengeScript.taskPanel.SetActive(true);
    }
}
