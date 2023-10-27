using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotation : MonoBehaviour
{
    //��ֹ� ������Ʈ ȸ�� ��ũ��Ʈ 
    public float rotationSpeed = 300f; // ȸ�� �ӵ� (���ϴ� ������ ����)
    public Vector3 rotationAxis = Vector3.up; // ȸ�� ��

    private void Update()
    {
        // ������ �� ������ ȸ�� (���� ��ǥ �������� ȸ��)
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }
}