namespace XO.Dto
{
    public class SetWinnerRequest
    {
        public string WinnerSymbol { get; set; }
        public string[,] Board { get; set; }
        public string CurrentPlayer { get; set; }
    }

}