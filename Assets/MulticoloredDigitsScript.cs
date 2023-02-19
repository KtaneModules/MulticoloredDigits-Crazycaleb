using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Rnd = UnityEngine.Random;
using KModkit;

public class MulticoloredDigitsScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;
    public TextMesh Display;

    int[] Digits = new int[4];
    string[] Colors = new string[4];
    char[] Mad = {'R','G','Y','B','W','M','C','O','P','A'};

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;

        Digits[0] = Rnd.Range(0,1000);
        Display.text = Digits[0].ToString("000");


    }
}
