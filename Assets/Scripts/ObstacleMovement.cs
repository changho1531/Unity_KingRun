using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // 이동 속도

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        // 이동 방향 설정
        Vector3 moveDirection = Vector3.back; // 뒤로 이동하도록 설정

        // Rigidbody를 사용하여 오브젝트 이동
        rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }
}
