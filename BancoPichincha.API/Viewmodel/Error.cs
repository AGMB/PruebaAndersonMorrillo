using Newtonsoft.Json;

namespace BancoPichincha.API.Viewmodel
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string MensajeError { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
