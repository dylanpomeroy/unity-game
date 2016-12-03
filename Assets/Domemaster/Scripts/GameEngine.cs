using UnityEngine;
using System.Collections;
using System.Linq;

public class GameEngine{
	private imageHandler handler;

	public void Start(){
		Debug.Log ("Game Engine started");
		displayAnimal (3);

		Debug.Log ("Game Engine ended");
	}

	public void test(){
		Debug.Log ("Test");
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

	private void setTextComponent(string textObjectId, string text){
		var objectComponent = GameObject.Find(textObjectId);
		var objectTextMesh = (TextMesh)objectComponent.transform.GetComponent (typeof(TextMesh));
		objectTextMesh.text = text;
	}
}
