using UnityEngine;

// This script allows Hinge Joints to function correctly with our Sprite Tilt Hinge shader
//which allows hinge rotatation even while a sprite is tilted (rotated on the X axis)
public class TiltedSpriteHingeJoint : MonoBehaviour
{
  #region Constants

  private static readonly int ANGLE = Shader.PropertyToID("_Angle");

  #endregion


  #region Inspector

  [SerializeField]
  private HingeJoint hinge = null;

  [SerializeField]
  private SpriteRenderer spriteRenderer = null;

  #endregion


  #region MonoBehaviour

  private void Update ()
  {
    float angle = -hinge.angle;
    spriteRenderer.material.SetFloat(ANGLE, angle);
  }

  #endregion
}