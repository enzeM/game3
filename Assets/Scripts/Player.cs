using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_jumpForce;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

	[SerializeField] public static bool autoRun = true;
	[SerializeField] public static bool m_canDoubleJump = true;//check double jump

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
	}

	void Update () {
		HandleJump ();
	}

	private void AutoMove () {
		if (autoRun) {
			m_animator.SetFloat ("hSpeed", m_moveSpeed);
			m_rigidBody.velocity = new Vector3 (m_moveSpeed, m_rigidBody.velocity.y, 0f);
		}
	}

	//player can make a jump and double jump
	private void HandleJump () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (m_canDoubleJump && !m_isGrounded) {//double jump
				m_rigidBody.velocity = new Vector3 (m_rigidBody.velocity.x, 0f, m_rigidBody.velocity.z); 
				m_rigidBody.AddForce (new Vector3 (m_rigidBody.velocity.x, m_jumpForce, m_rigidBody.velocity.z));
				m_canDoubleJump = false;
			} else if (m_isGrounded) { //first jump
				m_rigidBody.velocity = new Vector3 (m_rigidBody.velocity.x, 0f, m_rigidBody.velocity.z); 
				m_rigidBody.AddForce (new Vector3 (m_rigidBody.velocity.x, m_jumpForce, m_rigidBody.velocity.z));
			} 
		}
		if (m_isGrounded) {//reset double jump when player is grounded
			m_canDoubleJump = true;
		}
		m_animator.SetFloat ("vSpeed", m_rigidBody.velocity.y);
	}
}
