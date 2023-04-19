using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
 [Header("Inscribed")]
 public float speed = 10f; // The movement speed is 10m/s
 public float fireRate = 0.3f; // Seconds/shot (Unused)
 public float health = 10; // Damage needed to destroy this enemy
 public int score = 100; // Points earned for destroying this
private BoundsCheck bndCheck;

 // This is a Property: A method that acts like a field
 public Vector3 pos { // a
 get {
 return this.transform.position;
 }
 set {
 this.transform.position = value;
 }
 }

void Awake() { // c
 bndCheck = GetComponent<BoundsCheck>();
 }

 void Update() {
 Move(); // b
if ( bndCheck.LocIs( BoundsCheck.eScreenLocs.offDown ) ) { // a
 Destroy( gameObject );
 }
 // if ( !bndCheck.isOnScreen ) { // b
 // if ( pos.y < bndCheck.camHeight - bndCheck.radius ) {
 // // We’re off the bottom, so destroy this GameObject
 // Destroy( gameObject );
 // }
 // }
 }

 public virtual void Move() { // c
 Vector3 tempPos = pos;
 tempPos.y -= speed * Time.deltaTime;
 pos = tempPos;
 }

 void OnCollisionEnter( Collision coll ) {
 GameObject otherGO = coll.gameObject; // a
 if ( otherGO.GetComponent<ProjectileHero>() != null ) { // b
 Destroy( otherGO ); // Destroy the Projectile
 Destroy( gameObject ); // Destroy this Enemy GameObject
 } else {
 Debug.Log( "Enemy hit by non-ProjectileHero: " + otherGO.name ); // c
 }
 }

 // void Start() {…} // Please delete the unused Start() method
 }
