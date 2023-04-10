﻿using System.ComponentModel.DataAnnotations;

namespace Basket.API.Entities
{
  public class CartItem
  {
    [Required]
    [Range(1, double.PositiveInfinity, ErrorMessage = "The field {0} must be >= {1}.")]
    public int Quantity { get; set; }

    [Range(0.1, double.PositiveInfinity, ErrorMessage = "The field {0} must be >= {1}.")]
    public decimal ItemPrice { get; set; }
    public string ItemNo { get; set; }

    public string ItemName { get; set; }
    public int AvailableQuantity { get; set; }

    public void SetAvailableQuantity(int quantity)
    {
      AvailableQuantity = quantity;
    }

  }
}
