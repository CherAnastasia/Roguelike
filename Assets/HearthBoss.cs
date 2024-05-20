using UnityEngine;
using UnityEngine.UI;

public class HearthBoss : MonoBehaviour
{
    public GameObject heartContainer;
    private float fillValue;//заполенность

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)Enemy.Health;//устанавливаем значение жизней игрока
        fillValue = fillValue / Enemy.MaxHealth;//будет уменьшать красную картинку от 1 до 0
        heartContainer.GetComponent<Image>().fillAmount = fillValue;//получаем величину заливки и устанавливаем нашему значению заливки
    }
}

