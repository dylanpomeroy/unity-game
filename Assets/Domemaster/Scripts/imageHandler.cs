using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class imageHandler : MonoBehaviour {
	public static string imageBasePath = "https://cs.dal.ca/~dpomeroy/dome/images/";

	public void Update () {
	}

	public void loadImage(string imageName){
		StartCoroutine (loadAssert (imageName));
	}
	IEnumerator  loadAssert(string img){
		Debug.Log (img);
		var tex = new Texture2D (4, 4, TextureFormat.DXT1, false);
		Debug.Log ("IMG: " + img);
		var www = new WWW (imageBasePath + img);
		yield return www;
		www.LoadImageIntoTexture (tex);
		GameObject.Find ("animalPicture0").transform.GetComponent<Renderer> ().material.mainTexture = tex;
		GameObject.Find ("animalPicture1").transform.GetComponent<Renderer> ().material.mainTexture = tex;
		GameObject.Find ("animalPicture2").transform.GetComponent<Renderer> ().material.mainTexture = tex;
		GameObject.Find ("animalPicture3").transform.GetComponent<Renderer> ().material.mainTexture = tex;
	}
}
