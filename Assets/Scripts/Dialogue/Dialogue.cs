using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue {

	public string name;
	public Sprite sprite;
	[TextArea(3, 10)]
	public string[] sentences;
	public bool isCutscene;
	[Range(0f, 5f)]
	public float fadeTime = 0.1f;
	public UnityEvent eventAfterDialogue;

}
