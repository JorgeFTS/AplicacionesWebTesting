using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using Xunit;

namespace ChambaPro.Platform.API.Tests.Service.Domain.Model.Aggregates;

public class ServicesTests
{
    private Services CreateSampleService()
    {
        return new Services(
            clientId: 1,
            technicianId: 2,
            date: DateTime.Today,
            time: "10:00",
            description: "Reparación de tomacorriente",
            category: "Electricidad",
            cost: 80.00m,
            duration: "2 horas",
            address: "Av. Principal 123"
        );
    }

    [Fact]
    public void Constructor_WithValidData_SetsStatusToPending()
    {
        var service = CreateSampleService();

        Assert.Equal(ServiceStatus.Pending, service.Status);
        Assert.Equal(1, service.ClientId);
        Assert.Equal(2, service.TechnicianId);
        Assert.Equal(80.00m, service.Cost);
    }

    [Fact]
    public void EmptyConstructor_SetsDefaultValues()
    {
        var service = new Services();

        Assert.Equal(ServiceStatus.Pending, service.Status);
        Assert.Equal(string.Empty, service.Time);
        Assert.Equal(string.Empty, service.Description);
        Assert.Equal(string.Empty, service.Category);
        Assert.Equal(string.Empty, service.Duration);
        Assert.Equal(string.Empty, service.Address);
    }

    [Fact]
    public void Confirm_FromPending_ChangesStatusToConfirmed()
    {
        var service = CreateSampleService();

        service.Confirm();

        Assert.Equal(ServiceStatus.Confirmed, service.Status);
    }

    [Fact]
    public void Complete_FromConfirmed_ChangesStatusToCompleted()
    {
        var service = CreateSampleService();
        service.Confirm();

        service.Complete();

        Assert.Equal(ServiceStatus.Completed, service.Status);
    }

    [Fact]
    public void Cancel_FromPending_ChangesStatusToCancelled()
    {
        var service = CreateSampleService();

        service.Cancel();

        Assert.Equal(ServiceStatus.Cancelled, service.Status);
    }

    [Fact]
    public void Complete_WhenAlreadyCancelled_ThrowsInvalidOperationException()
    {
        var service = CreateSampleService();
        service.Cancel();

        var exception = Assert.Throws<InvalidOperationException>(() => service.Complete());
        Assert.Equal("Cannot complete a cancelled service", exception.Message);
    }

    [Fact]
    public void Confirm_WhenAlreadyCancelled_ThrowsInvalidOperationException()
    {
        var service = CreateSampleService();
        service.Cancel();

        var exception = Assert.Throws<InvalidOperationException>(() => service.Confirm());
        Assert.Equal("Cannot confirm a cancelled service", exception.Message);
    }

    [Fact]
    public void Cancel_WhenAlreadyCompleted_ThrowsInvalidOperationException()
    {
        var service = CreateSampleService();
        service.Confirm();
        service.Complete();

        var exception = Assert.Throws<InvalidOperationException>(() => service.Cancel());
        Assert.Equal("Cannot cancel a completed service", exception.Message);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(5, 10)]
    [InlineData(100, 200)]
    public void Constructor_WithDifferentClientAndTechnicianIds_SetsCorrectly(int clientId, int technicianId)
    {
        var service = new Services(clientId, technicianId, DateTime.Today, "09:00", 
            "desc", "cat", 50m, "1 hora", "dirección");

        Assert.Equal(clientId, service.ClientId);
        Assert.Equal(technicianId, service.TechnicianId);
    }
}
