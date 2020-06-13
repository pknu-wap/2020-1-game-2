using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 에디터 안 Inspector에서 편집 간으한 속도 조정 변수 생성.
    public float speed;
    // Rigidbody 형태의 변수 생성.
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // 현재 오브젝트의 Rigidbody를 참조.
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 물리 효과 계산을 수행하기 전에 호출.
    void FixedUpdate()
    {
        // 키보드의 키로 컨트롤하는 수평 축의 입력(left, right).
        float moveHorizontal = Input.GetAxis("Horizontal");
        // 키보드의 키로 컨트롤하는 수직 축의 입력 (up, down).
        float moveVertical = Input.GetAxis("Vertical");

        // 플레이어의 이동량을 선언(x축, y축, z축).
        Vector3 movemnet = new Vector3(moveHorizontal, 0, moveVertical);
        // 플레이어의 Rigidbody에서 movement 값만큼 힘을 가해서 이동시킴.
        rb.AddForce(movemnet * speed);
    }
}
