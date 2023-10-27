using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIManager : MonoBehaviour
{
    public GameObject heartPrefab; // ��Ʈ �̹��� ������
    public Transform heartParent; // ��Ʈ �̹������� ���� �θ� Transform

    public List<Image> hearts = new List<Image>(); // ������ ��Ʈ �̹������� ������ ����Ʈ

    public void Initialize(int maxHP)
    {
        // �̹� ������ �̹������� ��� �����մϴ�.
        foreach (Image heartImage in hearts)
        {
            Destroy(heartImage.gameObject);
        }
        hearts.Clear();

        // �ʱ�ȭ �ÿ� maxHP�� ���� ��Ʈ �̹����� �����մϴ�.
        for (int i = 0; i < maxHP; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, heartParent);
            Image heartImage = heartObj.GetComponent<Image>();
            hearts.Add(heartImage);

            // �̹��� ��ġ�� �������� �̵���ŵ�ϴ�.
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
                break; // ��Ʈ�� �� �̻� �������� ������ ������ �����մϴ�.
            }
        }
    }
}