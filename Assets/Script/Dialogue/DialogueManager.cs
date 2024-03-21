using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public CanvasGroup dialogueCanvas;
	public CanvasGroup blackCanvas;
	//public Text nameText;
	public TMPro.TextMeshProUGUI dialogueText;
	public TMPro.TextMeshProUGUI dialogueName;

	public Image dialogueIcon;
	public RectTransform UITransform;
	float fadeTime;
	UnityEvent unityEvent;

	PlayerGameInput gameInput;
	float defaultWidth;

	public bool talking { get; private set;}
	bool typing = false;

	//public Animator animator;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		dialogueIcon.gameObject.SetActive(false);
		defaultWidth = UITransform.rect.width;
		gameInput = FindObjectOfType<PlayerGameInput>();
		sentences = new Queue<string>();
		talking = false;
	}

	public void StartDialogue (Dialogue dialogue)
	{
		unityEvent = dialogue.eventAfterDialogue;
		if (talking == true)
			return;
		talking = true;
		gameInput.EnableUIInputs();
		if (dialogue.name != "")
			dialogueName.text = dialogue.name + ':';
		else
			dialogueName.text = "";
		if(dialogue.sprite != null)
        {
			dialogueIcon.gameObject.SetActive(true);
			UITransform.sizeDelta =new Vector2(defaultWidth + 300f, UITransform.rect.height);
			dialogueIcon.sprite = dialogue.sprite;
        }
		else
        {
			UITransform.sizeDelta = new Vector2(defaultWidth, UITransform.rect.height);
			dialogueIcon.gameObject.SetActive(false);

		}
		fadeTime = dialogue.fadeTime;
		//animator.SetBool("IsOpen", true);
		LeanTween.alphaCanvas(dialogueCanvas, 1, fadeTime);
		if(dialogue.isCutscene)
        {
			LeanTween.alphaCanvas(blackCanvas, 1, fadeTime);
		}
		

		//nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

    private void Update()
    {
        if(gameInput.UIOKAction.WasPressedThisFrame())
        {
			DisplayNextSentence();
        }
    }

    public void DisplayNextSentence ()
	{
		if (typing)
			return;
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		typing = true;
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.03f);
		}
		typing = false;
	}

	void EndDialogue()
	{
		talking = false;
		gameInput.EnableGameplayInputs();
		TriggerEvents();
		//animator.SetBool("IsOpen", false);
		LeanTween.alphaCanvas(dialogueCanvas, 0, fadeTime);
		LeanTween.alphaCanvas(blackCanvas, 0, fadeTime);
	}
	
	void TriggerEvents()
    {
		unityEvent.Invoke();
    }

}
