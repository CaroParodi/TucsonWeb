﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tucson.Models.DTO
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
