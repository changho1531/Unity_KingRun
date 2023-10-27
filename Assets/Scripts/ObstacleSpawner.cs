using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // ��ֹ� �����յ�
    public Transform[] spawnPoints; // ��ֹ��� ������ ��ġ��

    public float spawnInterval = 0.6f; // ��ֹ� ���� ����
    public float moveSpeed = 30f; // ��ֹ� �̵� �ӵ�
    public float maxMoveSpeed = 180f; // �ִ� �̵� �ӵ�
    public float despawnDistance = 150; // ��ֹ��� ������� �Ÿ�

    public GameObject rangeIndicatorPrefab; // �ǰ� ������ �̵� ������ ǥ���� �ӽ� ������Ʈ�� ������
    private GameObject rangeIndicator; // ������ �ӽ� ������Ʈ�� ���� ����

    public float speedIncreaseInterval = 1f; // �ӵ��� ������ų ����
    public float speedIncreaseAmount = 3f; // �ӵ��� ������ų ��

    private class SpawnedObstacle
    {
        public GameObject obstacle;
        public float traveledDistance;
    }

    private List<SpawnedObstacle> spawnedObstacles = new List<SpawnedObstacle>(); // ������ ��ֹ� ������Ʈ��

    private void Start()
    {
        StartCoroutine(SpawnObstacles());
        StartCoroutine(IncreaseSpeed());
    }

    private IEnumerator IncreaseSpeed()
    {
        Debug.Log("IncreaseSpeed�Լ� ���� ����");
        yield return new WaitForSeconds(6f); // 6�� ���
        Debug.Log("��� ��");

        while (true)
        {
            if (moveSpeed < maxMoveSpeed) // ���� �ӵ��� �ִ� �ӵ� �̸��� ��쿡�� �ӵ��� ������Ŵ
            {
                // �ӵ��� ������ŵ�ϴ�.
                moveSpeed += speedIncreaseAmount;
            }

            yield return new WaitForSeconds(speedIncreaseInterval);
        }
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // ������ ��ġ���� ��ֹ� ����
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);

            // �ӽ� ������Ʈ ���� �� ��ġ ����
            rangeIndicator = Instantiate(rangeIndicatorPrefab);
            rangeIndicator.transform.position = new Vector3(spawnPoints[spawnIndex].position.x, 0.51f, spawnPoints[spawnIndex].position.z);
            rangeIndicator.transform.rotation = Quaternion.LookRotation(spawnPoints[spawnIndex].forward); // ��ֹ� �̵� ������ ǥ���ϱ� ���� ȸ���� ����

            ChangeColorOfIndicator(rangeIndicator, Color.red); // �̵� ���� ǥ�ô� �������� ǥ��

            // 1�� �Ŀ� ���� ��ֹ� ����
            yield return new WaitForSeconds(0.8f);
            GameObject obstacle = Instantiate(obstaclePrefabs[obstacleIndex], spawnPoints[spawnIndex].position, Quaternion.identity); // ȸ�� �ʱ�ȭ
            //ObstacleRotation rotationScript = obstacle.AddComponent<ObstacleRotation>();

            spawnedObstacles.Add(new SpawnedObstacle { obstacle = obstacle, traveledDistance = 0f });

            // ������ �Ϸ�Ǹ� �ӽ� ������Ʈ ����
            Destroy(rangeIndicator);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void FixedUpdate()
    {
        // ��ֹ� �̵� �� ����� ó��
        for (int i = spawnedObstacles.Count - 1; i >= 0; i--)
        {
            SpawnedObstacle spawnedObstacle = spawnedObstacles[i];
            GameObject obstacle = spawnedObstacle.obstacle;

            float obstacleMoveDelta = moveSpeed * Time.deltaTime;
            spawnedObstacle.traveledDistance += obstacleMoveDelta;
            //obstacle.transform.Translate(Vector3.back * obstacleMoveDelta);
            obstacle.transform.Translate(Vector3.back * obstacleMoveDelta, Space.World); // Space.World�� ����

            if (spawnedObstacle.traveledDistance >= despawnDistance)
            {
                // ��ֹ��� ���� �Ÿ��� �̵��� �Ŀ� ����
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
            
        // �ڽ� ������Ʈ�� �����Ͽ� �ӽ� ������Ʈ�� ���� ����
        Renderer[] childRenderers = indicator.GetComponentsInChildren<Renderer>();
        foreach (Renderer childRenderer in childRenderers)
        {
            childRenderer.material.color = color;
        }
    }
}