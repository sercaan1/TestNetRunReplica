﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Abstracts
{
    public interface IResult
    {
        bool IsSuccess { get; }
        string Message { get; }
    }
}
