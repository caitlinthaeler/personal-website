using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data_api.Models;

public class JwtOptions
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpirationTimeInMinutes { get; set; }
}