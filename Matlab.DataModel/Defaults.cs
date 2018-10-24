using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matlab.DataModel
{
    public static class Defaults
    {
        public const int BoxMaxArticles = 5;
        public const string BaseUrl = "http://mohsenmeshkini.ir/";
        public const string BaseImageUrl = BaseUrl+"Images/Articles/";
        public const string BasePackageImageUrl = BaseUrl + "Images/Packages/";
        public const string BaseChallengeImageUrl = BaseUrl + "Images/Challenges/";
        public const int CorrectAnswerScore = 5;
        public const int WrongAnswerScore = 1;
        public const int ArticleReadScore = 1;
        public const int LoginScore = 1;
        public const int RegisterScore = 10;

    }
}
