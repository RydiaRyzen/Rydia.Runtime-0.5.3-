using System;
using System.Diagnostics;
using Rydia;

namespace Rydia.Physics.Dynamics.Joints
{
	/// <summary>
	/// Maintains a fixed angle between two bodies
	/// </summary>
	public class AngleJoint : Joint
	{
		public float BiasFactor;
		public float MaxImpulse;
		public float Softness;
		private float _bias;
		private float _jointError;
		private float _massFactor;
		private float _targetAngle;

		internal AngleJoint()
		{
			JointType = JointType.Angle;
		}

		public AngleJoint(Body bodyA, Body bodyB)
			: base(bodyA, bodyB)
		{
			JointType = JointType.Angle;
			TargetAngle = 0;
			this.BiasFactor = .2f;
			this.Softness = 0f;
			this.MaxImpulse = float.MaxValue;
		}

		public float TargetAngle
		{
			get { return this._targetAngle; }
			set
			{
				if (value != this._targetAngle)
				{
					this._targetAngle = value;
					WakeBodies();
				}
			}
		}

		public override Vector2 WorldAnchorA
		{
			get { return BodyA.Position; }
		}

		public override Vector2 WorldAnchorB
		{
			get { return BodyB.Position; }
			set { Debug.Assert(false, "You can't set the world anchor on this joint type."); }
		}

		public override Vector2 GetReactionForce(float inv_dt)
		{
			//TODO
			//return _inv_dt * _impulse;
			return Vector2.Zero;
		}

		public override float GetReactionTorque(float inv_dt)
		{
			return 0;
		}

		internal override void InitVelocityConstraints(ref TimeStep step)
		{
			this._jointError = (BodyB.Sweep.A - BodyA.Sweep.A - TargetAngle);

			this._bias = -this.BiasFactor * step.inv_dt * this._jointError;

			this._massFactor = (1 - this.Softness) / (BodyA.InvI + BodyB.InvI);
		}

		internal override void SolveVelocityConstraints(ref TimeStep step)
		{
			float p = (this._bias - BodyB.AngularVelocity + BodyA.AngularVelocity) * this._massFactor;
			BodyA.AngularVelocity -= BodyA.InvI * Math.Sign(p) * Math.Min(Math.Abs(p), this.MaxImpulse);
			BodyB.AngularVelocity += BodyB.InvI * Math.Sign(p) * Math.Min(Math.Abs(p), this.MaxImpulse);
		}

		internal override bool SolvePositionConstraints()
		{
			//no position solving for this joint
			return true;
		}
	}
}