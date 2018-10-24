using System;
using System.Collections.Generic;
using Matlab.Logger;

namespace Matlab.DataModel
{
    public static class ModelExtension
    {
        public static void AddScore(this ApplicationUser user, ScoreReason reason)
        {
            int scoreValue;
            switch (reason)
            {
                case ScoreReason.CorrectAnswer:
                    scoreValue = Defaults.CorrectAnswerScore;
                    break;
                case ScoreReason.WrongAnswer:
                    scoreValue = Defaults.WrongAnswerScore;
                    break;
                case ScoreReason.ArticleRead:
                    scoreValue = Defaults.ArticleReadScore;
                    break;
                case ScoreReason.Login:
                    scoreValue = Defaults.LoginScore;
                    break;
                case ScoreReason.Register:
                    scoreValue = Defaults.RegisterScore;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reason), reason, null);
            }

            user.ScoreLogs.Add(new ScoreLog() { DateTime = DateTime.Now, Reason = reason, Value = scoreValue });
            user.Score += scoreValue;
        }
    }
}