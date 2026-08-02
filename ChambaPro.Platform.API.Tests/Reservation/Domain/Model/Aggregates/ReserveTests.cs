using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Reservation.Domain.Model.Commands;
using Xunit;

namespace ChambaPro.Platform.Test.Reservation.Domain.Model.Aggregates;
public class ReserveTests
{
    private Reserve CreateSampleReserve()
    {
        return new Reserve(
            
            date: DateTime.Today,
            time: TimeSpan.Parse("10:00"),
            description : "Lorem ipsum" ,
            clientId : 3,
            categoryId: "4"
        );



    }

    [Fact]
    public void UpdateFromCommand_WhenChangingStatusToCancelled_DoesNotValidateBusinessRules()
    {
        var reserve = CreateSampleReserve();
        reserve.AssignTechnician(5);

        var command = new UpdateReserveCommand(
            ReserveId: 1,
            ClientId: 3,
            Date: DateTime.Today,
            Time: TimeSpan.Parse("10:00"),
            Description: "Lorem ipsum",
            CategoryId: "lorem ipsum",
            TechnicianId: 5,
            Status: ReservationStatus.Cancelled

        );
        
        reserve.UpdateFromCommand(command);
        Assert.Equal(ReservationStatus.Cancelled, reserve.Status);
        
        
    }
}