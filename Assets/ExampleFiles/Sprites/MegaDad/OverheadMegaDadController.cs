using System.Collections;
using System.Linq;
using UnityEngine;

namespace MegaDad
{
    public class OverheadMegaDadController : MonoBehaviour
    {
        private const float MovingSpeedPPS = 0.64f;

        private Vector2 m_Facing = Vector2.down;
        private Vector2 m_Velocity = Vector2.zero;

        private Animator m_Animator;

        private void Awake()
        {
            m_Animator = gameObject.GetComponentInChildren<Animator>();
            m_Animator.SetFloat("Dir_x", m_Facing.x);
            m_Animator.SetFloat("Dir_y", m_Facing.y);
        }

        private void Update()
        {
            m_Velocity = Vector2.zero;

            m_Velocity.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * MovingSpeedPPS;
            m_Velocity.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * MovingSpeedPPS;

            // Only move one axis at a time, prefering horizontal movement
            if (m_Velocity.x != 0)
            {
                m_Velocity.y = 0;
            }

            // Remember which way we were facing when we moved
            if (m_Velocity.SqrMagnitude() != 0)
            {
                m_Facing = m_Velocity;
            }

            gameObject.transform.Translate(m_Velocity);
        }

        private void LateUpdate()
        {
            m_Animator.SetFloat("Dir_x", m_Facing.x);
            m_Animator.SetFloat("Dir_y", m_Facing.y);
            m_Animator.SetBool("Moving", m_Velocity.SqrMagnitude() != 0);
        }
    }
}