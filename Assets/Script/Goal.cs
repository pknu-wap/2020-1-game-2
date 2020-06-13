using System.Collections;

using System.Collections.Generic;

using UnityEngine;







public class Goal : MonoBehaviour
{


    private int BallCount = 0; // 공의 총 갯수

    private int counter = 0; // 골에 닿은 공 카운터

    private bool cleared = false; // 클리어 여부

    private IEnumerator coroutine;



    // Use this for initialization

    void Start()
    {

        BallCount = 1; //공의 수 입력

    }



    //클리어

    void OnTriggerEnter(Collider other)

    {

        if (other.gameObject.tag == "Ball") // 공이 골에 닿았다

        {

            counter++;

            if (cleared == false && counter == BallCount) // 클리어 판정

            {

                cleared = true;

                coroutine = Wait(2.0f); //코루틴 작동

                StartCoroutine(coroutine);

                Debug.Log("Cleared!"); //클리어 찍기

            }

        }

    }

    private IEnumerator Wait(float waitTime) //WaitForSeconds 작성하기 위한 함수 생성

    {

        while (true)

        {

            yield return new WaitForSeconds(waitTime); //waitTime 동안 기다리기

            Application.LoadLevel("practice Title"); // 타이틀로 가기

        }

    }

    void OnGUI()

    {

        int sw = Screen.width;

        int sh = Screen.height;

        if (cleared == true)
        {

            GUI.Label(new Rect(sw / 6, sh / 3, sw * 2 / 3, sh / 3), "CLEARED!!");

        }

    }

    // Update is called once per frame

    void Update()
    {



    }

}

