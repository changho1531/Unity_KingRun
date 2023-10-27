using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // �̵� �ӵ�

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
        // �̵� ���� ����
        Vector3 moveDirection = Vector3.back; // �ڷ� �̵��ϵ��� ����

        // Rigidbody�� ����Ͽ� ������Ʈ �̵�
        rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }
}