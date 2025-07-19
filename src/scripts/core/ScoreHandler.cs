using Godot;
using System;

namespace FlappyClone.Core {
	public class ScoreHandler
	{
		public int score { get; }
		
		public ScoreHandler() {
			score = 0;
		}
		
		public int incrementScore() {
			score += 1;
			return score;
		}
	}
}
