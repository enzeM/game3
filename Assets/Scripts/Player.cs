using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_jumpForce;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private bool autoRun = false;

	public static int RED = 1;
	public static int BLUE = 2;
	public static int DEFAULT = 3;

	public static int PlayerColour {
		get;
		set;
	}

    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();

	private void Start() {
		m_animator = GetComponent<Animator> ();
		m_rigidBody = GetComponent<Rigidbody> ();
	}

    private void OnCollisionEnter(Collision collision) {
        ContactPoint[] contactPoints = collision.contacts;
        for(int i = 0; i < contactPoints.Length; i++) {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f) {
                if (!m_collisions.Contains(collision.collider)) {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision) {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++) {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f) {
                validSurfaceNormal = true; break;
            }
        }
        if(validSurfaceNormal) {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider)) {
                m_collisions.Add(collision.collider);
            }
        } else {
            if (m_collisions.Contains(collision.collider)) {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { 
				m_isGrounded = false; 
			}
        }
    }

    private void OnCollisionExit(Collision collision) {
        if(m_collisions.Contains(collision.collider)) {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { 
			m_isGrounded = false; 
		}
    }

	void FixedUpdate () {
		m_animator.SetBool ("isGrounded", m_isGrounded);
		AutoMove ();
		HandleJump ();
	}

	private void AutoMove () {
		if (autoRun) {
			m_animator.SetFloat ("hSpeed", m_moveSpeed);
			m_rigidBody.velocity = new Vector3 (m_moveSpeed, m_rigidBody.velocity.y, 0f);
		}
	}

	private int m_jumpCount = 0;//double jump

	private void HandleJump () {
		if (m_jumpCount < 2 && !m_isGrounded && Input.GetKeyDown (KeyCode.Space)) {//double jump
			m_rigidBody.AddForce (new Vector3 (m_rigidBody.velocity.x, m_jumpForce, m_rigidBody.velocity.z));
			m_jumpCount++;
		} else if (m_isGrounded && Input.GetKeyDown (KeyCode.Space)) {
			m_rigidBody.AddForce (new Vector3 (m_rigidBody.velocity.x, m_jumpForce, m_rigidBody.velocity.z));
			m_jumpCount++;
		} else if (m_jumpCount >= 2 && m_isGrounded) {//reset jump count
			m_jumpCount = 0;
		}
		m_animator.SetFloat ("vSpeed", m_rigidBody.velocity.y);
	}
}
