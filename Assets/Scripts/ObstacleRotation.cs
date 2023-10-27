using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotation : MonoBehaviour
{
    //장애물 오브젝트 회전 스크립트 
    public float rotationSpeed = 300f; // 회전 속도 (원하는 값으로 조절)
    public Vector3 rotationAxis = Vector3.up; // 회전 축

    private void Update()
    {
        // 지정된 축 주위로 회전 (로컬 좌표 공간에서 회전)
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
