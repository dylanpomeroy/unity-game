using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using System.Linq;

public class GameEngine : MonoBehaviour{
	private imageHandler handler;
	private string url = "http://70.32.108.82:3000/qrcode/findByList";
	private int lastQRCodeLength = 0;
	private int cursor = 1;
	private JsonData jsonData;
	// Use this for initialization
	public void Start () {
		pullRecentQRCode (); 
	}

	// Update is called once per frame
	public void Update () {

	}

	public void pullRecentQRCode(){
		StartCoroutine(WaitForRequest(new WWW(url)));
		Debug.Log ("Latest QRCode");
	}

	public void Awake(){
		InvokeRepeating("pullRecentQRCode", 0, 5);
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			jsonData = JsonMapper.ToObject(www.text);
			if (jsonData.Count > 0) {
				lastQRCodeLength = jsonData.Count;
				cursor++;
				int id = (int)jsonData [jsonData.Count - 1] ["animalId"]; 
				Debug.Log ("Current Animal ID:" + id);
				if ((lastQRCodeLength > jsonData.Count) && (cursor > 1)) {
					displayAnimal (id);

				} else {
					displayInstruction();
				}

			} else {
				Debug.Log ("no data available");
			}
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}


	public void displayAnimal(int animalId){

		handler = (imageHandler)GameObject.FindObjectOfType<imageHandler> ();
		if (!Repo.animalCache.ContainsKey (animalId)) {
			Debug.Log ("An animal ID was requested that does not exist in the animalCache.");
		} else {
			var animal = Repo.animalCache [animalId];

			// set the text values
			setTextComponent ("nameText0", animal.name);
			setTextComponent ("scientificNameText0", animal.scientificName);
			setTextComponent ("nationalStatusText0", "National Status: " + animal.nationalStatus);
			setTextComponent ("provincialStatusText0", "Provincial Status: " + animal.provincialStatus);
			setTextComponent ("descriptionText0", animal.description);
		
			// set the images
			Debug.Log("Image: " + animal.images.First());
			handler.loadImage (animal.images.First());
		}
	}

	public void displayInstruction(){
		handler = (imageHandler)GameObject.FindObjectOfType<imageHandler> ();
		setTextComponent ("nameText0", "Instruction");
		setTextComponent ("scientificNameText0", "");
		setTextComponent ("nationalStatusText0", "");
		setTextComponent ("provincialStatusText0", "");
		setTextComponent ("descriptionText0", Repo.instruction);
		handler.loadImage ("animalPicture7.jpg");

	}
	private void setTextComponent(string textObjectId, string text){
		var objectComponent = GameObject.Find(textObjectId);
		var objectTextMesh = (TextMesh)objectComponent.transform.GetComponent (typeof(TextMesh));
		objectTextMesh.text = text;
	}
}
