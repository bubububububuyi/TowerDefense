using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPlayer :MonoBehaviour
{

    public static int Money;
    public int StartMoney = 500;

    void Start()
    {
        Money = StartMoney;
    }

}
