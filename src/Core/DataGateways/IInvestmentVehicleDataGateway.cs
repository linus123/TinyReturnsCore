namespace TinyReturns.SharedKernel.DataGateways
{
    public interface IInvestmentVehicleDataGateway
    {
        InvestmentVehicleDto[] GetAllEntities();
    }
}