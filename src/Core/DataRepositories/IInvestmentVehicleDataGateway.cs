namespace TinyReturns.Core.DataRepositories
{
    public interface IInvestmentVehicleDataGateway
    {
        InvestmentVehicleDto[] GetAllEntities();
    }
}