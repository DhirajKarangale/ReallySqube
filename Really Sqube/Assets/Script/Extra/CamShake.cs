using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour
{
    public static CamShake instance;
    [SerializeField] CinemachineImpulseSource impulse;

    private void Awake()
    {
        instance = this;
    }
   
    public void Shake(float amplitude = 6, float time = 0.1f)
    {
        impulse.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = time;
        impulse.GenerateImpulse(amplitude);
    }
}
