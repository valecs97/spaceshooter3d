using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pressToStart;

    private GameObject pressToStartObject;
    private bool canStart = false;

    void Start()
    {
        pressToStartObject = Instantiate(pressToStart, pressToStart.transform.position, pressToStart.transform.rotation);
        StartCoroutine(BlinkPressToStartText());
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android && AndroidInputs.getAnyTouchBeginInput())
            canStart = true;
        else if (Application.platform == RuntimePlatform.WindowsEditor && Input.GetKeyDown(KeyCode.G))
            canStart = true;
    }

    IEnumerator BlinkPressToStartText()
    {
        player.GetComponent<PlayerController>().BlockControlls();
        while (!canStart)
        {
            pressToStartObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            pressToStartObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(pressToStartObject);
        player.GetComponent<PlayerController>().UnblockControlls();
        GetComponent<GameController>().StartGame();
        
    }
}
