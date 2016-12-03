using UnityEngine;
using System.Collections;
using System.Linq;

public static class GameEngine {
	public static void Start(){
		Debug.Log ("Game Engine started");


		displayAnimal (2);

		Debug.Log ("Game Engine ended");
	}

	private static void displayAnimal(int animalId){
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
			//new imageHandler(animal.images.First());
			//GameObject.
			new imageHandler().Update("animalPicture3.jpg");

			// we have to add the component imageHandler
		}



	}

	private static void setTextComponent(string textObjectId, string text){
		var objectComponent = GameObject.Find(textObjectId);
		var objectTextMesh = (TextMesh)objectComponent.transform.GetComponent (typeof(TextMesh));
		objectTextMesh.text = text;
	}
}
