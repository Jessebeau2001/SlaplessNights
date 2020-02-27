﻿using System;

namespace GXPEngine
{
	internal class Debree : EasyDraw
	{
		public int healthTimer = 0;
		private float _xSpeed, _ySpeed, _distance, _beatMs;
		private int _hitAnimFrame = 0;

		Sprite note = new Sprite("assets/note_red.png", false);

		public Debree(float x, float y, float _xTarg, float _yTarg, float beatMs) : base(100, 100)
		{
			note.SetOrigin(note.width / 2, note.height / 2);
			note.scale = .2f;
			AddChild(note);
			this.x = x;
			this.y = y;
			this._beatMs = beatMs;

			SetOrigin(width / 2, height / 2);
			Ellipse(width / 2, height / 2, 20, 20);

			_distance = Extensions.GetDistance(_xTarg, _yTarg, x, y);
			_xSpeed = (_xTarg - x) / _distance; //Its more like a speed multiplier, this one should be 1 and
			_ySpeed = (_yTarg - y) / _distance;	//the y one should be a multiplier of some sorts
		}

		public void Update()
		{
			x += ((_distance / _beatMs) * Time.deltaTime * _xSpeed) / 2;
			y += ((_distance / _beatMs) * Time.deltaTime * _ySpeed) / 2;

			if (x > game.width + 30 || x < -30)
			{
				if (y > game.height || y < -30)
				{
					LateDestroy();
				}
			}
		}

		public void HitAnim()
		{

			_hitAnimFrame++;
		}

		void OnCollision(GameObject other)
		{
			if (other is Intercept && (other as Intercept).isActive(x)) LateDestroy();
			if (other is Scene01 && (other as Scene01).getDamage()) LateDestroy();
		}
	}

	static class Extensions
	{
		///<summary> Re-maps a number from one range to another.</summary>wel
		public static float Map(this float value, float from1, float to1, float from2, float to2)
		{
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}
		///<summary> Pythagoras equation.</summary>
		public static float GetDistance(float x1, float y1, float x2, float y2)
		{
			return (float)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
		}
	}
}
