using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Text Sentence;
    public Lsystem lSystem;


    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {

        Sentence.text = lSystem.sentence;

    }


}
