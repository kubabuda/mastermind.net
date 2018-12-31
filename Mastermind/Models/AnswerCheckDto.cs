namespace Mastermind.Models
{
    public class AnswerCheckDto: IAnswerCheckDto
    {
        public int WhitePoints { get; private set; }
        public int BlackPoints { get; private set; }
        
        public AnswerCheckDto(int white, int black)
        {
            WhitePoints = white;
            BlackPoints = black;
        }
    }
}
