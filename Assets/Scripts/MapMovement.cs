using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 맵의 이동 속도

    private void Update()
    {
        // 맵을 Z 축 방향으로 이동시킵니다
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
}
