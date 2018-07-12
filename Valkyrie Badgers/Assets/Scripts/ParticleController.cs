using UnityEngine;
public class ParticleController : MonoBehaviour
{

  [SerializeField]
  private ParticleSystem particleObject;

  void Start()
  {
    particleObject = GetComponent<ParticleSystem>();
    particleObject.Stop();
  }
  public void Play()
  {
    Debug.Log("Play:");
    particleObject.Play();
  }

}