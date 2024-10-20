using UnityEngine;

public class GunPhysics : MonoBehaviour
{
    public Animation shoot;
    public Animation reload;
    static GunPhysics instance;
    public float intensity;
    public float smooth;
    Quaternion origin_Rotation;
    Quaternion targer_Rotation;

    public static GunPhysics GetInstance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        origin_Rotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSway();
    }
    public void PlayShootingAnimation()
    {
        shoot.Play();
        Invoke("PlayReloadingAnimation", 0.417f);
    }
    public void PlayReloadingAnimation()
    {
        reload.Play();
    }
    void UpdateSway()
    {
        float t_y_mouse = Input.GetAxis("Mouse Y");
        float t_x_mouse = Input.GetAxis("Mouse X");

        Quaternion t_x_Adj  = Quaternion.AngleAxis(-intensity * t_x_mouse, Vector3.up);
        Quaternion t_y_Adj  = Quaternion.AngleAxis(-intensity * t_y_mouse, Vector3.right);
        targer_Rotation = origin_Rotation * t_x_Adj* t_y_Adj;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targer_Rotation, Time.deltaTime * smooth);
    }
}
