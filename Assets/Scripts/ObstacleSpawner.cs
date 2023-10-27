using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // 장애물 프리팹들
    public Transform[] spawnPoints; // 장애물이 생성될 위치들

    public float spawnInterval = 0.6f; // 장애물 생성 간격
    public float moveSpeed = 30f; // 장애물 이동 속도
    public float maxMoveSpeed = 180f; // 최대 이동 속도
    public float despawnDistance = 150; // 장애물이 사라지는 거리

    public GameObject rangeIndicatorPrefab; // 피격 범위와 이동 범위를 표시할 임시 오브젝트의 프리팹
    private GameObject rangeIndicator; // 생성된 임시 오브젝트를 담을 변수

    public float speedIncreaseInterval = 1f; // 속도를 증가시킬 간격
    public float speedIncreaseAmount = 3f; // 속도를 증가시킬 양

    private class SpawnedObstacle
    {
        public GameObject obstacle;
        public float traveledDistance;
    }

    private List<SpawnedObstacle> spawnedObstacles = new List<SpawnedObstacle>(); // 생성된 장애물 오브젝트들

    private void Start()
    {
        StartCoroutine(SpawnObstacles());
        StartCoroutine(IncreaseSpeed());
    }

    private IEnumerator IncreaseSpeed()
    {
        Debug.Log("IncreaseSpeed함수 내부 들어옴");
        yield return new WaitForSeconds(6f); // 6초 대기
        Debug.Log("대기 끝");

        while (true)
        {
            if (moveSpeed < maxMoveSpeed) // 현재 속도가 최대 속도 미만인 경우에만 속도를 증가시킴
            {
                // 속도를 증가시킵니다.
                moveSpeed += speedIncreaseAmount;
            }

            yield return new WaitForSeconds(speedIncreaseInterval);
        }
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // 랜덤한 위치에서 장애물 생성
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);

            // 임시 오브젝트 생성 및 위치 설정
            rangeIndicator = Instantiate(rangeIndicatorPrefab);
            rangeIndicator.transform.position = new Vector3(spawnPoints[spawnIndex].position.x, 0.51f, spawnPoints[spawnIndex].position.z);
            rangeIndicator.transform.rotation = Quaternion.LookRotation(spawnPoints[spawnIndex].forward); // 장애물 이동 방향을 표시하기 위해 회전값 설정

            ChangeColorOfIndicator(rangeIndicator, Color.red); // 이동 방향 표시는 빨강으로 표시

            // 1초 후에 실제 장애물 생성
            yield return new WaitForSeconds(0.8f);
            GameObject obstacle = Instantiate(obstaclePrefabs[obstacleIndex], spawnPoints[spawnIndex].position, Quaternion.identity); // 회전 초기화
            //ObstacleRotation rotationScript = obstacle.AddComponent<ObstacleRotation>();

            spawnedObstacles.Add(new SpawnedObstacle { obstacle = obstacle, traveledDistance = 0f });

            // 생성이 완료되면 임시 오브젝트 삭제
            Destroy(rangeIndicator);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void FixedUpdate()
    {
        // 장애물 이동 및 사라짐 처리
        for (int i = spawnedObstacles.Count - 1; i >= 0; i--)
        {
            SpawnedObstacle spawnedObstacle = spawnedObstacles[i];
            GameObject obstacle = spawnedObstacle.obstacle;

            float obstacleMoveDelta = moveSpeed * Time.deltaTime;
            spawnedObstacle.traveledDistance += obstacleMoveDelta;
            //obstacle.transform.Translate(Vector3.back * obstacleMoveDelta);
            obstacle.transform.Translate(Vector3.back * obstacleMoveDelta, Space.World); // Space.World로 변경

            if (spawnedObstacle.traveledDistance >= despawnDistance)
            {
                // 장애물이 일정 거리를 이동한 후에 제거
                Destroy(obstacle);
                spawnedObstacles.RemoveAt(i);
            }
        }
            
    }

    private void ChangeColorOfIndicator(GameObject indicator, Color color)
    {
        Renderer renderer = indicator.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
            
        // 자식 오브젝트도 포함하여 임시 오브젝트의 색상 변경
        Renderer[] childRenderers = indicator.GetComponentsInChildren<Renderer>();
        foreach (Renderer childRenderer in childRenderers)
        {
            childRenderer.material.color = color;
        }
    }
}
