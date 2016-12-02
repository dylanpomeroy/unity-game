using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using AssemblyCSharp;
using System.Linq;

public class DataAccessUtils : MonoBehaviour {
	private JsonData jsonData;
	void Start () {
		var urls = new Dictionary<string, string> {
			{ "animal", "http://70.32.108.82:3000/animal/findByList" },
			{ "instruction", "http://70.32.108.82:3000/instruction/findByList" },
			{ "theme", "http://70.32.108.82:3000/theme/findByList" },
		};

		foreach (var url in urls) {
			StartCoroutine(WaitForRequest(url.Key, new WWW(url.Value)));
		}
	}

	IEnumerator WaitForRequest(string item, WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			jsonData = JsonMapper.ToObject(www.text);

			if (item.Equals ("animal"))
				loadAnimalsData (jsonData);
			else if (item.Equals ("instruction"))
				loadInstructionData (jsonData);
			else if (item.Equals ("theme"))
				loadThemesData (jsonData);

		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}

	public void loadAnimalsData(JsonData data){
		Repo.animalCache = new Dictionary<int,Animal> ();
		for (int i = 0; i < data.Count; i++) {
			Animal animal = new Animal ();
			animal.id = (int)data[i]["id"];
			animal.color = getData(data[i]["color"]["name"]);
			animal.description = getCommaSeparatedStringAsList(getData(data[i]["description"]),"\n");
			animal.images = getCommaSeparatedStringAsList(getData(data [i] ["image"]),",");
			animal.imageGeo = getData(data[i]["imageGeo"]);
			animal.name = getData(data[i]["name"]);
			animal.nationalStatus = getData(data[i]["nationalStatus"]["name"]);
			animal.provincialStatus = getData(data[i]["provincialStatus"]["name"]);
			animal.scientificName = getData(data[i]["scientificName"]);
			animal.species = getData(data[i]["specie"]["name"]);

//			Debug.Log (animal.id + ","
//				+ animal.color + ","
//				+ animal.description + ","
//				+ string.Join (",", animal.images.ToArray()) + ","
//				+ animal.imageGeo + ","
//				+ animal.name + ","
//				+ animal.nationalStatus + ","
//				+ animal.provincialStatus + ","
//				+ animal.scientificName + ","
//				+ animal.species);
			
			Repo.animalCache.Add(animal.id, animal);
		}

	}

	private void loadInstructionData(JsonData data){
		Repo.instruction = getData (data[0]["instruction"]);
		Repo.instructionImages = getCommaSeparatedStringAsList(getData (data [0] ["image"]),",");
	}

	private void loadThemesData(JsonData data){
		Repo.themeColor = getData (data [0] ["color"]);
		Repo.themeImages = getCommaSeparatedStringAsList(getData (data [0] ["image"]),",");
		Debug.Log ("Test: "+Repo.themeColor);
		Debug.Log (Repo.themeImages.First ());
	}


	private string getData(JsonData data){
		return null != data ? (string)data : "";
	}

	public List<string> getCommaSeparatedStringAsList(string aString, string regex){
		if (aString.Contains(regex)) {
			return aString.Split(regex).ToList();
		}
		return new string[] { aString }.ToList();
	}
}