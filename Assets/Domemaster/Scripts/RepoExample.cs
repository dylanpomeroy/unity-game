using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RepoExample : MonoBehaviour
{
	void Start()
	{
		Repo.instructionImages = new List<string> ()
		{
			"asdf",
			"fdsa"
		};
		Debug.Log ("instructionImages IS: " + Repo.instructionImages.First());
	}
}
