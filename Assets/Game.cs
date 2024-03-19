using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public InputField firstInputNum;
    public InputField secondInputNum;
    public InputField lastInputNum;

    Button selectButton;

    public Text resultText;

    //public Canvas infoUI;

    bool isGameOver = true;

    static int firstNum;
    static int secondNum;
    static int lastNum;

    int ballCount = 0;
    int strikeCount = 0;
    int outCount = 0;

    int turnCount = 0;

    int rewardPoint = 300;

    int resetGold = 1000;

    private void Start()
    {
        selectButton = GameObject.Find("SelectButton").GetComponent<Button>();
    }

    private void SetNumber()
    {
        firstNum = Random.Range(1, 9);
        while (true)
        {
            secondNum = Random.Range(1, 9);
            if (firstNum != secondNum)
            {
                break;
            }
        }
        while (true)
        {
            lastNum = Random.Range(1, 9);
            if (lastNum != firstNum)
            {
                if (lastNum != secondNum)
                {
                    break;
                }
            }
        }
    }

    public void OnClickGameStart()
    {
        if (isGameOver)
        {
            selectButton.interactable = true;
            SetNumber();
            resultText.text += "���ӽ���\n";
            isGameOver = false;
            turnCount = 0;
        }
        else
        {
            resultText.text += "�̹� ������ ���� ���Դϴ�.\n";
        }
    }

    public void OnClickCheck()
    {
        if (firstInputNum.text != secondInputNum.text && secondInputNum.text != lastInputNum.text && firstInputNum.text != lastInputNum.text)
        {
            if (int.Parse(firstInputNum.text) == firstNum)
            {
                strikeCount++;
            }
            else if (int.Parse(firstInputNum.text) == secondNum || int.Parse(firstInputNum.text) == lastNum)
            {
                ballCount++;
            }
            else
            {
                outCount++;
            }

            if (int.Parse(secondInputNum.text) == secondNum)
            {
                strikeCount++;
            }
            else if (int.Parse(secondInputNum.text) == firstNum || int.Parse(secondInputNum.text) == lastNum)
            {
                ballCount++;
            }
            else
            {
                outCount++;
            }

            if (int.Parse(lastInputNum.text) == lastNum)
            {
                strikeCount++;
            }
            else if (int.Parse(lastInputNum.text) == firstNum || int.Parse(lastInputNum.text) == secondNum)
            {
                ballCount++;
            }
            else
            {
                outCount++;
            }

            turnCount++;

            if (strikeCount == 3)
            {
                GameOver();
            }
            else
            {
                resultText.text += $"{ballCount}B {strikeCount}S {outCount}O \n";
                ballCount = 0;
                strikeCount = 0;
                outCount = 0;

            }
        }
        else
        {
            resultText.text += "�ߺ��� ���� ������ �մϴ�.\n";
        }
    }

    void GameOver()
    {
        resultText.text += $"���� �¸�. �õ� : {turnCount} - ���� {rewardPoint - (turnCount * 10)} ����Ʈ \n";
        isGameOver = true;

        InfoManager.instance.PlusPoint(rewardPoint - (turnCount * 10));
        resetGold = 1000;

        selectButton.interactable = false;
    }

    public void OnClickReset()
    {
        int gold = InfoManager.instance.GetGold();
        if (isGameOver == false)
        {
            if (gold >= resetGold)
            {
                InfoManager.instance.UseGold(resetGold);
                resetGold += 1000;

                resultText.text += "������ �ٽ� �����մϴ�. \n";
                isGameOver = true;
                OnClickGameStart();
            }
            else
            {
                Debug.Log("��尡 �����մϴ�.");
            }

        }
        else
        {
            resultText.text += "�������� ������ �����ϴ� \n";
        }
    }
}

