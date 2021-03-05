namespace DataHandler.Model
{
    public class Team
    {
        public long Id { get; set; }
        public string Country { get; set; }
        public string AlternateName { get; set; }
        public string FifaCode { get; set; }
        public long GroupId { get; set; }
        public string GroupLetter { get; set; }

        public override string ToString() => $"Id: {Id}, Country: {Country}, AlternateName: {AlternateName}, FifaCode: {FifaCode}, GroupId: {GroupId}, GroupLetter: {GroupLetter}";
    }
}
