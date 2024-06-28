using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region OldCode
    //public TextAsset inkFile;
    //public GameObject textBox;
    //public GameObject customeButton;
    //public GameObject optionPanel;
    //public bool isTalking = false;

    //static Story story;
    //TextMeshProUGUI nameTag;
    //TextMeshProUGUI message;
    //List<string> tags;
    //static Choice choiceSelected;

    ////Start is call before the first frame update
    //void Start()
    //{
    //    story = new Story(inkFile.text);
    //    nameTag = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    //    message = textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    //    tags = new List<string>();
    //    choiceSelected = null;
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //Is there more to the story?
    //        if (story.canContinue)
    //        {
    //            nameTag.text = "test";
    //            AdvanceDialogue();

    //            //Are there any choices?
    //            if (story.currentChoices.Count != 0)
    //            {
    //                StartCoroutine(ShowChoices());
    //            }
    //        }
    //        else
    //        {
    //            FinishDialogue();
    //        }
    //    }
    //}

    ////Create then show the choices on the screen until one got selected
    //IEnumerator ShowChoices()
    //{
    //    Debug.Log("There are choices need to make here!");
    //    List<Choice> choices = story.currentChoices;

    //    for (int i = 0; i < choices.Count; i++)
    //    {
    //        GameObject temp = Instantiate(customeButton, optionPanel.transform);
    //        temp.transform.GetChild(0).GetComponent<Text>().text = choices[i].text;
    //        temp.AddComponent<Selectable>();
    //        temp.GetComponent<Selectable>().element = choices[i];
    //        temp.GetComponent<Button>().onClick.AddListener(() =>
    //        {
    //            temp.GetComponent<Selectable>().Decide();
    //        });

    //        optionPanel.SetActive(true);

    //        yield return new WaitUntil(() =>
    //        {
    //            if (choiceSelected != null)
    //            {
    //                AdvanceFromDecision();
    //            }
    //            return choiceSelected != null;
    //        });

    //        AdvanceFromDecision();
    //    }
    //}

    //// Tells the story which branch to go to
    //public static void SetDecision(object element)
    //{
    //    choiceSelected = (Choice)element;
    //    story.ChooseChoiceIndex(choiceSelected.index);
    //}

    //// After a choice was made, turn off the panel and advance from that choice
    //void AdvanceFromDecision()
    //{
    //    optionPanel.SetActive(false);
    //    for (int i = 0; i < optionPanel.transform.childCount; i++)
    //    {
    //        Destroy(optionPanel.transform.GetChild(i).gameObject);
    //    }
    //    choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
    //    AdvanceDialogue();
    //}

    ////Finish the Story (Dialogue)
    //private void FinishDialogue()
    //{
    //    optionPanel.SetActive(false);
    //    for (int i = 0; i < optionPanel.transform.childCount; i++)
    //    {
    //        Destroy(optionPanel.transform.GetChild(i).gameObject);
    //    }
    //    Debug.Log("End dialogue");
    //}

    ////Advance through the story
    //void AdvanceDialogue()
    //{
    //    string currentSentence = story.Continue();
    //    //ParseTags();
    //    StopAllCoroutines();
    //    StartCoroutine(TypeSentence(currentSentence));
    //}

    ////type out sentence letter by letter and make character idle if they were talking
    //IEnumerator TypeSentence(string sentence)
    //{
    //    //Show message 
    //    message.text = "";
    //    foreach (char letter in sentence.ToCharArray())
    //    {
    //        message.text += letter;
    //        yield return null;
    //    }

    //    //Idle character
    //}


    ////====== Tag Parser ======
    ////Description: 
    ////In inky, you can use tags which can be used to cue stuff in a game.
    ////This is just one way of doing it. Not the only method on how to trigger eventss.
    //void ParseTags()
    //{
    //    tags = story.currentTags;
    //    foreach (string t in tags)
    //    {
    //        string prefix = t.Split(' ')[0];
    //        string param = t.Split(" ")[1];

    //        switch (prefix.ToLower())
    //        {
    //            case "anim":
    //                //Set animation
    //                break;
    //            case "color":
    //                //Set color of text
    //                break;
    //        }
    //    }
    //}
    #endregion

    #region NewCode
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("ChoicesUI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool isDialoguePlaying { get; private set; }

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
        isDialoguePlaying = false;

        //Get choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        //Return right away if dialogue is not playing
        if (!isDialoguePlaying)
        {
            return;
        }

        //Handle continuing to the next dialogue's line when player's continuing input catch
        if (Input.GetKeyDown(KeyCode.Space) && currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        isDialoguePlaying = true;
        dialoguePanel.SetActive(true);

        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            //Exit dialogue
            ExitDialogueMode();
        }
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = string.Empty;
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //Set current dialogue line
            dialogueText.text = currentStory.Continue();

            //Set current choices
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogAssertion("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        //Enable and initialize the choices 
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        //Go through the remaining choices the UI support and hide them.
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        //Event system requires we clear it first, and then wait
        // for at least 1 frame before we can set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
    #endregion
}
