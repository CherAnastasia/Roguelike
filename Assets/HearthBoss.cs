using UnityEngine;
using UnityEngine.UI;

public class HearthBoss : MonoBehaviour
{
    public GameObject heartContainer;
    private float fillValue;//������������

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)Enemy.Health;//������������� �������� ������ ������
        fillValue = fillValue / Enemy.MaxHealth;//����� ��������� ������� �������� �� 1 �� 0
        heartContainer.GetComponent<Image>().fillAmount = fillValue;//�������� �������� ������� � ������������� ������ �������� �������
    }
}

