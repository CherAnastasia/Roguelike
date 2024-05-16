using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Room currRoom;//наша комната
    public float moveSpeedWhenRoomChange;//скорость пермещения камеры
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        UpdatePosition();//обновляем положение нашей камеры
    }
    void UpdatePosition()//обновляем положение нашей камеры
    {
        if(currRoom==null)//равна ли наша комп=ната нулю
        {
            return;//мы не хотим обновлять положение камеры
        }
        Vector3 targetPos = GetCameraTargetPosition();//обновляем позицию
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedWhenRoomChange);
    }
    Vector3 GetCameraTargetPosition()//обновляем позицию
    {
        if (currRoom == null)//равна ли наша комп=ната нулю
        {
            return Vector3.zero;//оставляем положение камеры в ее исхдной позиции
        }
        Vector3 targetPos = currRoom.GetRoomCentre();//позиция равна текущей комнате
        targetPos.z=transform.position.z;
        return targetPos;
    }
    public bool IsSwitchingScrene()//проверяем переключение сцены
    {
        return transform.position.Equals(GetCameraTargetPosition())==false;
    }
}
