using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAnswer : MonoBehaviour
{
    Camera _mainCam = null;

    // 마우스의 상태.
    private bool _mouseState;
    // 마우스가 다운된 오브젝트. 퀴즈의 정답으로 설정.
    private GameObject Answer;
    // 마우스의 좌표.
    private Vector3 MousePos;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭했을 때.
        if (Input.GetMouseButtonDown(0))
        {

            // 오브젝트를 받아 옴.
            Answer = GetClickedObject();

            // 정답이 해당 오브젝트인지 확인.
            if (true == Answer.Equals(gameObject))
            {
                // 맞으면 마우스 정보 바꿈.
                _mouseState = true;
            }
        }
        else if (true == Input.GetMouseButtonUp(0))
        {
            // 마우스 버튼이 올라가면 마우스 정보 바꿈.
            _mouseState = false;
        }


            // 마우스가 눌렸으면 답, 퀴즈, 퀴즈가 있는 벽 전부 파괴.
            if (true == _mouseState)
            {
                Destroy(Answer);
                Destroy(this.transform.parent.gameObject);
            }
        }

        // 마우스가 내려간 오브젝트를 가져옴.
        private GameObject GetClickedObject()
        {
            // 충돌이 감지된 영역.
            RaycastHit hit;
            // 찾은 오브젝트.
            GameObject Answer = null;

            // 마우스 포인트 근처 좌표를 만든다.
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

            // 마우스 근처에 오브젝트가 있는지 확인.
            if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
            {
                // 있으면 오브젝트를 저장.
                Answer = hit.collider.gameObject;
            }
            return Answer;
        }
    }

