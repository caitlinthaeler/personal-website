using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data_api.Exceptions;
public class TokenInvalidException(string message) : Exception(message);