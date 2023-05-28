namespace Trucks.Utilities;

public static class ValidationConstraints
{
    //Truck
    public const int TruckVinNumberRequiredLength = 17;
    public const int TruckRegistrationNumberRequiredLength = 8;
    public const int TruckTankCapacityMinValue = 950;
    public const int TruckTankCapacityMaxValue = 1420;
    public const int TruckCargoCapacityMinValue = 5000;
    public const int TruckCargoCapacityMaxValue = 29000;

    //Client
    public const int ClientNameMinLength = 3;
    public const int ClientNameMaxLength = 40;
    public const int ClientNationalityMinLength = 2;
    public const int ClientNationalityMaxLength = 40;

    //Dispatcher
    public const int DespatcherNameMinLength = 2;
    public const int DespatcherNameMaxLength = 40;
}

