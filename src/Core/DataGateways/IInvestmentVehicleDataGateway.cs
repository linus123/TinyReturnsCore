namespace TinyReturns.Core.DataGateways
{
    public interface IInvestmentVehicleDataGateway
    {
        InvestmentVehicleDto[] GetAllEntities();
    }
}