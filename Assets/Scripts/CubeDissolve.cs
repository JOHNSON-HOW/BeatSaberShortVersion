﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDissolve : MonoBehaviour {

    // Use this for initialization

    private float minValue = 4f;
    private float maxValue = 7f;
    private float fill = 0f;
    private float second = 0.7f;
    private float delay = 0.3f;
    private Material[] mats;


    void Start () {
        mats = gameObject.GetComponent<Renderer>().materials;
        fill = maxValue;
		setValue();
	}

    // Update is called once per frame
    void Update()
    {
        if (delay > 0) delay -= Time.deltaTime;
        else
        {
            fill -= (maxValue - minValue) / (60 * second);
            setValue();
        }
        if (fill < minValue) Destroy(gameObject,0.3f);
    }

    private void setValue() {
		for (int i = 0; i < mats.Length; i++) {
            mats[i].SetFloat("_Fill", fill);
		}
	}
}
