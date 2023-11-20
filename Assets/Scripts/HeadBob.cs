using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeadBob : MonoBehaviour
{
    [Header("HeadBob Effect")]
    [SerializeField] bool Enabled = true;
    [Space, Header("Main")]
    [SerializeField, Range(0.001f, 0.01f)] float Amount = 0.00484f;
    [SerializeField, Range(10f, 30f)] float Frequency = 16.0f;
    [SerializeField, Range(100f, 10f)] float Smooth = 44.7f;
    float ToggleSpeed = 3.0f;
    Vector3 StartPos;
    CharacterController player;
    private void Awake()
    {
        player = GetComponentInParent<CharacterController>();
        StartPos = transform.localPosition;
    }

    private void Update()
    {
        if (!Enabled) return;
        CheckMotion();
        ResetPos();
    }

    private void CheckMotion()
    {
        float speed = new Vector3(player.velocity.x, 0, player.velocity.z).magnitude;
        if (speed < ToggleSpeed) return;
        if (!player.isGrounded) return;
        PlayMotion(HeadBobMotion());
    }

    private void PlayMotion(Vector3 Movement)
    {
        transform.localPosition += Movement;
    }
    private Vector3 HeadBobMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * Frequency) * Amount, Smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * Frequency / 2f) * Amount * 2f, Smooth * Time.deltaTime);
        return pos;
    }

    private void ResetPos()
    {
        if (transform.localPosition == StartPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
    }
}
