using UnityEngine;
using UnityEngine.UI;

public class HearthUI : MonoBehaviour
{
    public GameObject heartContainer;
    private float fillValue;//������������

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)play.Health;//������������� �������� ������ ������
        fillValue= fillValue/ play.MaxHealth;//����� ��������� ������� �������� �� 1 �� 0
        heartContainer.GetComponent<Image>().fillAmount= fillValue;//�������� �������� ������� � ������������� ������ �������� �������
    }
}
