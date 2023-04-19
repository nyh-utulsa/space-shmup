using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public enum eType { center, inset, outset }; // a

    [System.Flags]
 public enum eScreenLocs { // a
 onScreen = 0, // 0000 in binary (zero)
 offRight = 1, // 0001 in binary
 offLeft = 2, // 0010 in binary
 offUp = 4, // 0100 in binary
 offDown = 8 // 1000 in binary
 }

 [Header("Inscribed")]
 public eType boundsType = eType.center; // a
 public float radius = 1f;
 public bool keepOnScreen = true;

[Header("Dynamic")]
 //public bool isOnScreen = true;
 public float camWidth;
 public float camHeight;
 public eScreenLocs screenLocs = eScreenLocs.onScreen;

 void Awake() {
 camHeight = Camera.main.orthographicSize; // b
 camWidth = camHeight * Camera.main.aspect; // c
 }

 void LateUpdate () { // d
 float checkRadius = 0;
if (boundsType == eType.inset) checkRadius = -radius;
 if (boundsType == eType.outset) checkRadius = radius;
 Vector3 pos = transform.position;
 //isOnScreen = true;
 screenLocs = eScreenLocs.onScreen; 

 // Restrict the X position to camWidth
if (pos.x > camWidth + checkRadius) { // c
 pos.x = camWidth + checkRadius; // c
 //isOnScreen = false; 
 screenLocs |= eScreenLocs.offRight;
 }
 if (pos.x < -camWidth - checkRadius) { // c
 pos.x = -camWidth - checkRadius; // c
 //isOnScreen = false; 
 screenLocs |= eScreenLocs.offLeft; 
 }

 // Restrict the Y position to camHeight
 if (pos.y > camHeight + checkRadius) { // c
 pos.y = camHeight + checkRadius; // c
 //isOnScreen = false; 
 screenLocs |= eScreenLocs.offUp; 
 }
 if (pos.y < -camHeight - checkRadius) { // c
 pos.y = -camHeight - checkRadius; // c
 //isOnScreen = false; 
 screenLocs |= eScreenLocs.offDown; 
 }

if ( keepOnScreen && !isOnScreen ) { // f
 transform.position = pos; // g
 //isOnScreen = true;
 screenLocs = eScreenLocs.onScreen;
}

 }

 public bool isOnScreen { // e
 get { return ( screenLocs == eScreenLocs.onScreen ); }
 }

public bool LocIs( eScreenLocs checkLoc ) {
 if ( checkLoc == eScreenLocs.onScreen ) return isOnScreen; // a
 return ( (screenLocs & checkLoc) == checkLoc ); // b
 }

 }