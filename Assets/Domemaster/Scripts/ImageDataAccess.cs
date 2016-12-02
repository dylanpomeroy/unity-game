using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using AssemblyCSharp;
using System.Linq;

namespace AssemblyCSharp
{
	public class ImageDataAccess : MonoBehaviour
	{
		public static UnityEngine.UI.Image img;

		public ImageDataAccess ()
		{
		}

//		public UnityEngine.UI.Image getImage(string imageName) {
//			WWW www = new WWW("https://web.cs.dal.ca/~dpomeroy/dome/images/" + imageName);
//			yield return www;
//			img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
//			return img;
//		}

		public UnityEngine.UI.Image getImage(string imageName){
			WWW www = new WWW("https://web.cs.dal.ca/~dpomeroy/dome/images/" + imageName);
			StartCoroutine(WaitForRequest(www));
			return img;
		}

		 IEnumerator WaitForRequest(WWW www){
			yield return www;

			// check for errors
			if (www.error == null) {
				img.sprite = Sprite.Create (
					www.texture,
					new Rect (0, 0, www.texture.width, www.texture.height),
					new Vector2 (0, 0));
			} else {
				Debug.Log("WWW Error: "+ www.error);
			}
		}
	}
}

