﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class bl_BackGround : MonoBehaviour {

    public Image BackGround_1 = null;
    public Image BackGround_2 = null;
    public Sprite[] Images;
    [Space(5)]
    [HideInInspector]
    public bool Firts = true;
    [Range(0.01f,10f)]
    public float m_Delay = 2.0f;
    [Space(7)]
    public Image PlayerGallery;
    public Sprite PlaySprite;
    public Sprite PauseSprite;
    //Privates
    private int NextImage = 0;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        StartCoroutine(FirtsFade());
        FileStream fileStream = new FileStream("config.txt", FileMode.Open);
        StreamReader streamReader = new StreamReader(fileStream);
        streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
        string strLine = streamReader.ReadLine();

        GLOBAL_PARA.Game.PlayerID = int.Parse(strLine.Substring(strLine.Length - 2, 1));
        Debug.Log("ID: " + GLOBAL_PARA.Game.PlayerID.ToString());
        streamReader.Close();
        streamReader.Dispose();
        fileStream.Close();
        fileStream.Dispose();
    }
    /// <summary>
    /// 
    /// </summary>
    void Change()
    {
        NextImage = (NextImage + 1) % Images.Length;
        if (Firts)
        {
            BackGround_2.sprite = Images[NextImage];
            StartCoroutine(FirtsFade());
        }
        else
        {
            BackGround_1.sprite = Images[NextImage];
            StartCoroutine(SecondFade());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator FirtsFade()
    {
        Color FadeIn = BackGround_1.color;
        Color FadeOut = BackGround_2.color;
        float t = 0.0f;
        while (FadeIn.a > 0.0f)
        {
            t += Time.deltaTime / 25;
            FadeIn.a = Mathf.Lerp(FadeIn.a, 0.0f,t);
            FadeOut.a = Mathf.Lerp(FadeOut.a, 1.0f,t);
            BackGround_1.color = FadeIn;
            BackGround_2.color = FadeOut;
            yield return null;
        }
        Firts = !Firts;
        StartCoroutine(Delay());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator SecondFade()
    {
        Color FadeIn = BackGround_2.color;
        Color FadeOut = BackGround_1.color;
        float t = 0.0f;
        while (FadeIn.a > 0.0f)
        {
            t += Time.deltaTime / 25;
            FadeIn.a = Mathf.Lerp(FadeIn.a, 0.0f, t);
            FadeOut.a = Mathf.Lerp(FadeOut.a, 1.0f, t);
            BackGround_2.color = FadeIn;
            BackGround_1.color = FadeOut;
            yield return null;
        }
        Firts = !Firts;
        StartCoroutine(Delay());
    }

    private bool m_state = true;
    public void GalleryState()
    {
        m_state = !m_state;
        Animation aa = BackGround_1.GetComponent<Animation>();
        Animation ab = BackGround_2.GetComponent<Animation>();

        aa.enabled = m_state;
        ab.enabled = m_state;

        PlayerGallery.sprite = (m_state) ? PlaySprite : PauseSprite;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(m_Delay);
        Change();
    }
}