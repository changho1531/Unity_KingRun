using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIManager : MonoBehaviour
{
    public GameObject heartPrefab; // 하트 이미지 프리팹
    public Transform heartParent; // 하트 이미지들을 담을 부모 Transform

    public List<Image> hearts = new List<Image>(); // 생성된 하트 이미지들을 저장할 리스트

    public void Initialize(int maxHP)
    {
        // 이미 생성된 이미지들을 모두 제거합니다.
        foreach (Image heartImage in hearts)
        {
            Destroy(heartImage.gameObject);
        }
        hearts.Clear();

        // 초기화 시에 maxHP에 맞춰 하트 이미지를 생성합니다.
        for (int i = 0; i < maxHP; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, heartParent);
            Image heartImage = heartObj.GetComponent<Image>();
            hearts.Add(heartImage);

            // 이미지 위치를 우측으로 이동시킵니다.
            RectTransform rectTransform = heartObj.GetComponent<RectTransform>();
            Vector3 newPosition = rectTransform.localPosition;
            newPosition.x = heartImage.rectTransform.rect.width * i;
            rectTransform.localPosition = newPosition;
        }
    }

    public void TakeDamage(int amount)
    {
        int currentHeartIndex = hearts.Count - 1;

        for (int i = 0; i < amount; i++)
        {
            if (currentHeartIndex >= 0)
            {
                Image heartImage = hearts[currentHeartIndex];
                hearts.RemoveAt(currentHeartIndex);
                Destroy(heartImage.gameObject);
                currentHeartIndex--;
            }
            else
            {
                break; // 하트가 더 이상 남아있지 않으면 루프를 종료합니다.
            }
        }
    }
}