using Newtonsoft.Json;

namespace WPFInterface
{
    internal class TeamCodes
    {
        public string HomeTeamCodeFemale { get; set; }
        public string GuestTeamCodeFemale { get; set; }
        public string HomeTeamCodeMale { get; set; }
        public string GuestTeamCodeMale { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}