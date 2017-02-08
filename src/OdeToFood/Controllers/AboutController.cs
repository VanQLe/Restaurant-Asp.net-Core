using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    [Route ("company/[controller]/[action]")]
    public class AboutController
    {
        public string Phone()
        {
            return "1-585-281-9778";
        }
        
        public string Address()
        {
            return "USA";
        }
    }
}
