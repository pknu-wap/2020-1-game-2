using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

// Main Camera를 Player에 드래그 앤 드롭 한다.

public class PlayerMove : MonoBehaviour
{
    public float mouseSensi = 50f;

    private float HorizonRot;
    private float VerticalRot;
    public float PlayerXRotValue;
    public float PlayerYRotValue;

    // public Camera PlayerCam;
    public Transform Axis;
    public Vector3 axis;

    // Start is called before the first frame update
    void Start()
    {
        // 마우스 커서 가운데 고정.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CamRotate();
        MovePlayer();
    }

    // 키보드 이동.
    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * 3.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * 3.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * 3.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * 3.0f * Time.deltaTime);
        }
    }

    // 카메라 회전.
    void CamRotate()
    {
        // 회전수치값. ** 현재 오브젝트의 회전값 아님.
        HorizonRot = Input.GetAxis("Mouse X") * mouseSensi * Time.deltaTime;
        VerticalRot = Input.GetAxis("Mouse Y") * -mouseSensi * Time.deltaTime;
        // 회전 값 만들기. 회전 수치를 더하거나 빼 주어 누적시켜 오브젝트의 회전 값을 형성.
        PlayerXRotValue += VerticalRot;
        PlayerYRotValue += HorizonRot;
        // 최대 회전 값 제한. 위아래 +-80으로 제한.
        PlayerXRotValue = Mathf.Clamp(PlayerXRotValue, -80, 80);
        // Z값을 0으로 고정시키는 함수
        Zvalue(0.0f);

        transform.rotation = Quaternion.Euler(PlayerXRotValue, PlayerYRotValue, 0);
    }

    private void Zvalue(float value)
    {
        Vector3 ZeulerRot = transform.eulerAngles;
        ZeulerRot.z = value;
        transform.eulerAngles = ZeulerRot;
    }
}

