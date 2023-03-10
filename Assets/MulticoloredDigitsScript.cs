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
    public TextMesh[] DigitDisplay;
    public TextMesh[] DigitDisplay2;
    public TextMesh[] DigitDisplay3;
    public TextMesh[] DigitDisplay4;
    public TextMesh Display;
    public TextMesh StageCounter;

    public Color[] Drugs;
    public KMSelectable[] buttonage;
    public KMSelectable clear;
    public KMSelectable submit;

    int[] Digits = new int[4];
    string[] Colors = new string[4];
    char[] Mad = { 'R', 'G', 'Y', 'B', 'W', 'M', 'C', 'O', 'P', 'A' };
    int Stage = 1;
    string answer = "";
    string after = "";

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;

        foreach (KMSelectable button in buttonage)
        {
            button.OnInteract += delegate () { inputPress(button); return false; };
        }

        submit.OnInteract += delegate () { submitPress(submit); return false; };

        clear.OnInteract += delegate () { clearPress(clear); return false; };
        
        Digits[0] = Rnd.Range(0, 1000);
        string Num = Digits[0].ToString("000");
        
        for (int i = 0; i < 3; i++)
        {
            int the = Rnd.Range(0, 10);
            Colors[0] += Mad[the];
            DigitDisplay[i].text = Num[i].ToString();
            DigitDisplay[i].color = Drugs[the];
        }



        Debug.LogFormat("[Multicolored Digits #{0}] Stage 1's colors are {1}", _moduleId, Colors[0]);

        // Stage 1 above 

        Digits[1] = Rnd.Range(0, 10000);
        string Num2 = Digits[1].ToString("0000");
        for (int i = 0; i < 4; i++)
        {
            int the = Rnd.Range(0, 10);
            Colors[1] += Mad[the];
            DigitDisplay2[i].text = Num2[i].ToString();
            DigitDisplay2[i].color = Drugs[the];
            DigitDisplay2[i].gameObject.SetActive(false);
        }

        Debug.LogFormat("[Multicolored Digits #{0}] Stage 2's colors are {1}", _moduleId, Colors[1]);

        // Stage 2 above

        Digits[2] = Rnd.Range(0, 100000);
        
        string Num3 = Digits[2].ToString("00000");

        for (int i = 0; i < 5; i++)
        {
            int the = Rnd.Range(0, 10);
            Colors[2] += Mad[the];
            DigitDisplay3[i].text = Num3[i].ToString();
            DigitDisplay3[i].color = Drugs[the];
            DigitDisplay3[i].gameObject.SetActive(false);
        }

        Debug.LogFormat("[Multicolored Digits #{0}] Stage 3's colors are {1}", _moduleId, Colors[2]);

        // Stage 3 above 

        Digits[3] = Rnd.Range(0, 1000000);

        string Num4 = Digits[3].ToString("000000");

        int question = Rnd.Range(0, 6);
        Num4 = Num4.Remove(question, 1);
        Num4 = Num4.Insert(question, "?");

        for (int i = 0; i < 6; i++)
        {
            int the = Rnd.Range(0, 10);
            
            Colors[3] += Mad[the];
            DigitDisplay4[i].text = Num4[i].ToString();
            DigitDisplay4[i].color = Drugs[the];
            DigitDisplay4[i].gameObject.SetActive(false);
        }

        Debug.LogFormat("[Multicolored Digits #{0}] Stage 4's colors are {1}", _moduleId, Colors[3]);

        after = tableone(Num4, Colors[3]);



        answer = tableone(Num, Colors[0]);
        Debug.LogFormat("[Multicolored Digits #{0}] The answer for stage 1 is {1}.", _moduleId, answer);


    }
    string QuestionMark(string before)
    {
        int p = BombInfo.GetPortCount();
        int i = ((2 * BombInfo.GetOnIndicators().Count()) + BombInfo.GetOffIndicators().Count());
        int b = BombInfo.GetBatteryCount();
        int[][] TheTable = new int[][]
        {
            new int[] {0 + 0, 1 + i, 0 - b, 0 * 0, p + p, 0 - b, i * i, i + p, b - i, p * b},
            new int[] {i + 0, i + b, 2 - 1, b * i, i + i, i - 5, i * p, b + 7, 7 - p, b * p},
            new int[] {0 - b, p - 1, 2 * p, 3 + 3, p - 4, i * 5, i + 2, 2 - i, b * 8, i + 9},
            new int[] {i * i, 1 * b, 2 + i, p - b, p * b, 5 + b, p - b, 7 * i, i + 8, b - 3},
            new int[] {b + 4, i + i, p - i, i * b, b + p, b - p, p * b, 4 + 7, p - 8, i * 9},
            new int[] {b - p, p - b, 5 * i, b + 3, 5 - i, i * b, 5 + p, i - 5, i * 5, b + 5},
            new int[] {b * 6, b * 6, 2 + p, 3 - i, 6 * b, i + 5, 6 - b, i * i, i + b, p - 9},
            new int[] {7 + i, b + i, p - 2, i * 3, 7 + 7, 7 - 5, 7 * 7, p + i, 8 - p, 9 * 9},
            new int[] {i - b, b - p, 2 * i, i + 8, 8 - i, b * 5, i + i, b - 8, 8 * b, p + i},
            new int[] {0 * i, p * p, 2 + p, p - 3, p * b, 5 + b, 6 - i, 7 * i, b + i, 9 - p},
        };

        int beforesum = 0;
        int aftersum = 0;
        int position = -1;
        for (int j = 0; j < 6; j++)
        {
            if (before[j] == '?')
            {
                position = j;
                continue;
            }
            beforesum += Int32.Parse(before[j].ToString());

        }

            for (int j = 0; j < 6; j++)
            {
                if (after[j] == '?')
                {
                    continue;
                }
            aftersum += Int32.Parse(after[j].ToString());
            }
            
            beforesum = Math.Abs(beforesum) % 10;
            aftersum = Math.Abs(aftersum) % 10;
        
            int replaceddigit = Math.Abs(TheTable[beforesum][aftersum] % 10);
            
            Colors[3] = Colors[3].Remove(position, 1);
            Colors[3] = Colors[3].Insert(position, "ARGYBWMCOP"[beforesum].ToString());
            before = before.Remove(position, 1);
            before = before.Insert(position, replaceddigit.ToString());

        Debug.LogFormat("[Multicolored Digits #{0}] The ? number is {1}.", _moduleId, replaceddigit);

        Debug.LogFormat("[Multicolored Digits #{0}] The color of the question mark is {1}", _moduleId, Colors[3][position]);

            return tableone(before, Colors[3]);
        
    }
       

    void inputPress(KMSelectable button)
    {
        button.AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, button.transform);
        if (!_moduleSolved && Display.text.Length < 6)
        {
            int num = Array.IndexOf(buttonage, button);
            Display.text += num.ToString();
        }
    }
    void clearPress(KMSelectable clear)
    {
        clear.AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, clear.transform);
        if (!_moduleSolved)
        {
            Display.text = "";
        }
    }
    void submitPress(KMSelectable submit)
    {
        submit.AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, clear.transform);
        if (!_moduleSolved)
        {
            if (Display.text == answer)
            {
                if (Stage == 1)
                {
                Stage++;
                StageCounter.text = "2";
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                Display.text = "";
                for (int i = 0; i < 4; i++)
                    {
                DigitDisplay2[i].gameObject.SetActive(true);
                        if (i != 3)
                        {
                DigitDisplay[i].gameObject.SetActive(false);
                        }

                    }
                    answer = tableone(Digits[1].ToString("0000"), Colors[1]);
                    Debug.LogFormat("[Multicolored Digits #{0}] The answer for stage 2 is {1}.", _moduleId, answer);
                }
                else if (Stage == 2)
                {
                    Stage++;
                    StageCounter.text = "3";
                    Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                    Display.text = "";
                    for (int i = 0; i < 5; i++)
                    {
                    DigitDisplay3[i].gameObject.SetActive(true);
                        if (i != 4)
                        {
                    DigitDisplay2[i].gameObject.SetActive(false);
                        }

                    }
                    answer = tableone(Digits[2].ToString("00000"), Colors[2]);
                    Debug.LogFormat("[Multicolored Digits #{0}] The answer for stage 3 is {1}.", _moduleId, answer);

                }
                else if (Stage == 3)
                {
                    Stage++;
                    StageCounter.text = "4";
                    Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                    Display.text = "";
                    for (int i = 0; i < 6; i++)
                    {
                    DigitDisplay4[i].gameObject.SetActive(true);
                        if (i != 5)
                        {
                    DigitDisplay3[i].gameObject.SetActive(false);
                        }

                    }
                    answer = QuestionMark(DigitDisplay4[0].text + DigitDisplay4[1].text + DigitDisplay4[2].text + DigitDisplay4[3].text + DigitDisplay4[4].text + DigitDisplay4[5].text);
                    Debug.LogFormat("[Multicolored Digits #{0}] The answer for stage 4 is {1}.", _moduleId, answer);
                }
                else if (Stage == 4)
                {
                    Module.HandlePass();
                    Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                    Display.text = "";
                    for (int i = 0; i < 6; i++)
                    {
                       DigitDisplay4[i].gameObject.SetActive(false);
                    }
 
                    Debug.LogFormat("[Multicolored Digits #{0}] Module Solved!", _moduleId);
                    
                }

            }
            else
            {
                Module.HandleStrike();
                Debug.LogFormat("[Multicolored Digits #{0}] Incorrect answer: {1}", _moduleId, Display.text);
                Display.text = "";

            }
            
        }
    }


    string tableone(string n, string c)
    {
        string answer = n;
        for (int i = 0; i < n.Length; i++)
        {

            int full = Int32.Parse(n.Replace('?', '0'));
            if (n[i] == '?')
            {
                continue;
            }
            int number = Int32.Parse(n[i].ToString());
            char col = c[i];

            if (col == 'G')
            {
                number = (number + 1) % 10;
            }

            else if (col == 'Y')
            {
                number = (number + 9) % 10;
            }

            else if (col == 'B')
            {
                number = (number * 2) % 10;
            }

            else if (col == 'W')
            {
                number = (number + 5) % 10;
            }

            else if (col == 'M')
            {
                number = (number + 2) % 10;
            }

            else if (col == 'C')
            {
                number = (number + 8) % 10;
            }

            else if (col == 'O')
            {
                number = (number * BombInfo.GetBatteryCount()) % 10;
            }

            else if (col == 'P')
            {
                number = (9 - number);
            }

            else if (col == 'A')
            {
                int sum = 0;
                while (full != 0)
                {
                    sum = (full % 10) + sum;
                    full = (full / 10);
                    
                }
                

                number = (number * sum) % 10;
            }

            answer = answer.Remove(i, 1);
            answer = answer.Insert(i, number.ToString());

        }



        return answer;
        
    }


}
