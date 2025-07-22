using Godot;
using System;

namespace FlappyClone.Core {
	public class ScoreHandler
	{
		public int score { get; private set; }
		
		public ScoreHandler() {
			score = 0;
		}
		
		public void Reset() {
			score = 0;
		}

		public int IncrementScore() {
			score += 1;
			return score;
		}
	}
}
