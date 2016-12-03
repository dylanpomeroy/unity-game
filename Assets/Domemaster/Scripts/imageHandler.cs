using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class imageHandler : MonoBehaviour {
	public static string imageBasePath = "https://cs.dal.ca/~dpomeroy/dome/images/";
	public string Img = "animalPicture3.jpg";
		
	// Use this for initialization
//	static void loadImage (string imageName) {
//		var url = imageBasePath + imageName; 
//		WWW www = new WWW(url);
//		yield return www;
//		img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
//	}


	// Update is called once per frame
	public void Update (string t) {
		Test (t);
	}

	IEnumerator  Test(string t){
		
		var tex = new Texture2D (4, 4, TextureFormat.DXT1, false);
		Debug.Log ("IMG: " + t);
		var www = new WWW (imageBasePath + t);
		yield return www;
		www.LoadImageIntoTexture (tex);
		transform.GetComponent<Renderer>().material.mainTexture = tex;

	}
}
