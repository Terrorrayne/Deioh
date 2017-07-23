using UnityEngine;

namespace PigeonCoopToolkit.Effects.Trails
{
	[AddComponentMenu("Pigeon Coop Toolkit/Effects/Smoke Trail")]
	public class SmokeTrail : TrailRenderer_Base
	{
		public float MinVertexDistance = 0.1f;
		public int MaxNumberOfPoints = 50;
		private Vector3 _lastPosition;
		private float _distanceMoved;
		public float RandomForceScale = 1;

		// DEIOH custom
		public bool usePerlinNoise = false;
		public float noiseScale = 0.5f;
		public float noiseSpeed = 1f;
		public bool useEjectionForce = false; // should smoke get pushed out even if we arn't moving?
		public float ejectionForceValue = 1f; // pushed with force along +z axis

		protected override void Start()
		{
			base.Start();
			_lastPosition = _t.position;
		}

		protected override void Update()
		{
			if (_emit)
			{
				_distanceMoved += Vector3.Distance(_t.position, _lastPosition);

				if (_distanceMoved != 0 && _distanceMoved >= MinVertexDistance || useEjectionForce)
				{
					AddPoint(new SmokeTrailPoint(), _t.position);
					_distanceMoved = 0;
				}
				_lastPosition = _t.position;

			}

			base.Update();
		}

		protected override void OnStartEmit()
		{
			_lastPosition = _t.position;
			_distanceMoved = 0;
		}

		protected override void Reset()
		{
			base.Reset();
			MinVertexDistance = 0.1f;
			RandomForceScale = 1;
		}


		protected override void InitialiseNewPoint(PCTrailPoint newPoint)
		{
			Vector3 moveVec;

			if (usePerlinNoise)
			{

				float noiseScroll = Time.time * noiseSpeed;
				moveVec = new Vector3(Mathf.PerlinNoise(_t.position.x * noiseScale, noiseScroll) - 0.5f, Mathf.PerlinNoise(_t.position.y * noiseScale, noiseScroll) - 0.5f, Mathf.PerlinNoise(_t.position.z * noiseScale, noiseScroll) - 0.5f).normalized * 2f;
			}
			else
			{
				moveVec = Random.onUnitSphere;
			}

			// todo: get ejection force stuff to actually work...
			if (useEjectionForce)
			{
				moveVec += _t.forward * ejectionForceValue * Time.deltaTime;
			}

			((SmokeTrailPoint)newPoint).RandomVec = moveVec * RandomForceScale;

		}
		protected override void OnTranslate(Vector3 t)
		{
			_lastPosition += t;
		}

		protected override int GetMaxNumberOfPoints()
		{
			return MaxNumberOfPoints;
		}
	}

	public class SmokeTrailPoint : PCTrailPoint
	{
		public Vector3 RandomVec;

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
			Position += RandomVec * deltaTime;
		}
	}
}
