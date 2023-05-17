﻿using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

[XmlType("Product")]
public class ExportSoldProductsDTO
{
	[XmlElement("name")]
	public string Name { get; set; } = null!;

	[XmlElement("price")]
	public decimal Price { get; set; }
}

