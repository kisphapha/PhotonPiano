
namespace PhotonPiano.Models.Enums
{
    public enum StudentStatus
    {
        Unregistered, //Not enroll yet
        PendingRegistration, //Registered, waiting for acceptance of entrance test
        Accepted, //Accepted to participate in entrance tests
        WaitingForClassPlacement, //Waiting for class placement, apply for student who just complete entrance test or just complete a class
        InClass, //The student is currently studying a class
        Dropped, //This student has dropped out for some reason.
    }
}
