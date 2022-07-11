

namespace CustomerWebApplication.Models.Data
{
    public class genericJsonResponse
    {
        public bool success { get; set; } = false;
        public string message { get; set; } = "";
        public object data { get; set; } = null;

    }
}
