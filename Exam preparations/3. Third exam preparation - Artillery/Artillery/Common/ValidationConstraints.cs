﻿namespace Artillery.Common;

public static class ValidationConstraints
{
	//Country
	public const int CountryNameMinLength = 4;
	public const int CountryNameMaxLength = 60;
	public const int CountryArmyMinSize = 50000;
	public const int CountryArmyMaxSize = 10000000;

	//Manufacturer
	public const int ManufacturerNameMinLength = 4;
	public const int ManufacturerNameMaxLength = 40;
	public const int ManufacturerFoundedMinLength = 10;
	public const int ManufacturerFoundedMaxLength = 100;

	//Shell
	public const int ShellWeightMinValue = 2;
	public const int ShellWeightMaxValue = 1680;
	public const int ShellCaliberMinLength = 4;
	public const int ShellCaliberMaxLength = 30;

	//Gun
	public const int GunWeightMinValue = 100;
	public const int GunWeightMaxValue = 1350000;
	public const double GunBarrelLengthMinValue = 2.00;
	public const double GunBarrelLengthMaxValue = 35.00;
	public const int GunRangeMinValue = 1;
	public const int GunRangeMaxValue = 100000;
	public const int GunTypeMinValue = 0;
	public const int GunTypeMaxValue = 5;
}

