using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialog : MonoBehaviour{
	GameObject actualDialog;

	void Start(){
		actualDialog = gameObject.GetComponentsInChildren<Transform>(true)[1].gameObject;
		Debug.Log(actualDialog.name);
	}
	public void showDialog(string text){
		actualDialog.GetComponentsInChildren<Text>()[0].text = text;
		actualDialog.SetActive(true);
	}
	public void closeOnOK(){
		actualDialog.SetActive(false);
	}
}
