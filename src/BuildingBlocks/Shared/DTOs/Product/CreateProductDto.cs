﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Product
{
  public class CreateProductDto
  {
    [Required]
    public string No { get; set; }
  }
}

