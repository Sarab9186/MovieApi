using Newtonsoft.Json;

namespace Movie.Data.HelperClasses
{
    public class GeneralError
    {
        [JsonProperty("Error")]
        public string Error { get; set; }
       
        public GeneralError(string error)
        {
            Error = error;
        }
    }
}
