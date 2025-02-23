﻿using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
        Status Status { get; set; }
    }
}
