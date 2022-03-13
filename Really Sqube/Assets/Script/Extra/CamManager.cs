using UnityEngine;
using System.Collections;

public class CamManager : MonoBehaviour
{
    [Header("Refrence")]
    public Camera cam;
    public Rigidbody2D player;

    [Header("Follow")]
    [SerializeField] float followSmoothness;
    [SerializeField] Vector3 offset;
    private Vector3 refFollowVel = Vector3.zero;

    [Header("Zoom")]
    [SerializeField] float zoomSmoothness;
    private float refZommVel = 0;

    public static CamManager instance { get; set; }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        FollowCamera();

        if (Mathf.Abs(player.velocity.x) <= 0.3f)
        {
            StopCoroutine(IEZoomCamera(11));
            if (cam.orthographicSize != 7)
            {
                StartCoroutine(IEZoomCamera(7));
            }
        }
        else
        {
            if (cam.orthographicSize != 11)
            {
                // StopCoroutine(IEZoomCamera(7));
                StartCoroutine(IEZoomCamera(11));
            }
        }
    }

    private void FollowCamera()
    {
        Vector3 newPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref refFollowVel, followSmoothness);
    }

    private IEnumerator IEZoomCamera(float zoom)
    {
        yield return new WaitForSeconds(2);

        if (Mathf.Abs(player.velocity.x) == 0)
        {
            // StopAllCoroutines();
            yield break;
        }

        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref refZommVel, zoomSmoothness);
    }
}
