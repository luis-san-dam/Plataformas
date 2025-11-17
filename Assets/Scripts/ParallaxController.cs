using UnityEngine;
public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private float[] parallaxScales;
    [SerializeField] private float smoothing = 1f;
    private Transform cam;
    private Vector3 previousCamPos;
    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }
    void LateUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) *
            parallaxScales[i];
            Vector3 targetPos = new Vector3(
            backgrounds[i].position.x + parallax,
            backgrounds[i].position.y,
            backgrounds[i].position.z
            );
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position,
            targetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;
    }
}